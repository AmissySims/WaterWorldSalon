using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WaterWorldLibrary.Models;

namespace Client.Pages
{
    /// <summary>
    /// Логика взаимодействия для BuscketPage.xaml
    /// </summary>
    public partial class BuscketPage : Page
    {
        public static List<BuscketItemFish> bucketList { get; set; }
        public BuscketPage()
        {
            InitializeComponent();
            bucketList = App.db.BusketFish
                   .Where(b => (b.FishId == b.Fish.Id ) && b.UserId == CurrentUser.AuthUser.Id)
                   .Select(b => new BuscketItemFish
                   {
                       Fish = b.Fish,
                       
                       Count = (int)b.CountF
                   })
                   .ToList();

            LIstBucket.ItemsSource = bucketList;
        }
        private void OrderBt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (var buc in bucketList)
                {

                    int countProd = App.db.Fish.Where(p => p.Id == buc.Fish.Id).Select(p => p.CountFish).FirstOrDefault() ?? -1 ;
                  
                    if (countProd < buc.Fish.CountFish )
                    {
                        MessageBox.Show($"Остаток на складе {countProd}, укажите верное количество", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                   else  if (countProd == 0)
                    {
                        MessageBox.Show($"Нельзя выбрать количство товара 0", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    else if (countProd == -1)
                    {
                        MessageBox.Show("Ошибка товара на складе", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                   
                }

                NavigationService.Navigate(new AddOrderPage(bucketList));

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при оформлении заказа: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteCommand(object sender, RoutedEventArgs e)
        {
            var selectedProduct = (sender as Button).DataContext as BuscketItemFish;
            if (selectedProduct != null)
            {
                try
                {
                    var rm = App.db.BusketFish.Where(b => b.FishId == selectedProduct.Fish.Id ).FirstOrDefault();
                    App.db.BusketFish.Remove(rm);
                    App.db.SaveChanges();
                    bucketList.Remove(selectedProduct);
                    LIstBucket.ItemsSource = null;
                    LIstBucket.ItemsSource = bucketList;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Не удалось удалить товар из корзины. Ошибка: {ex}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void CountTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox currentTextBox = (TextBox)sender;
            TextBlock totalPriceTextBlock = FindTotalPriceTextBlock(currentTextBox);

            if (totalPriceTextBlock.DataContext is BuscketItemFish product)
            {
                if (Int32.TryParse(currentTextBox.Text, out Int32 currentCount))
                {
                    var productInBucket = App.db.BusketFish.FirstOrDefault(b => b.FishId == product.Fish.Id);
                    if (productInBucket != null)
                    {
                        productInBucket.CountF = currentCount;
                        App.db.SaveChanges();
                    }
                    else
                    {
                        return;
                    }

                    totalPriceTextBlock.Text = (product.Fish.Cost * currentCount).ToString();
                }
                else
                {
                    totalPriceTextBlock.Text = 0.ToString();
                }
            }
        }


        private TextBlock FindTotalPriceTextBlock(TextBox currentTextBox)
        {
            FrameworkElement parent = currentTextBox;
            while (parent != null)
            {
                if (parent.FindName("TotalPriceTb") is TextBlock totalPriceTextBlock)
                {
                    return totalPriceTextBlock;
                }
                parent = parent.Parent as FrameworkElement;
            }
            return null;
        }


        private T FindVisualChild<T>(DependencyObject parent, string name) where T : FrameworkElement
        {
            int childCount = VisualTreeHelper.GetChildrenCount(parent);

            for (int i = 0; i < childCount; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child is T element && element.Name == name)
                {
                    return element;
                }
                else
                {
                    T foundElement = FindVisualChild<T>(child, name);
                    if (foundElement != null)
                        return foundElement;
                }
            }
            return null;
        }

        private void LIstBucket_Loaded(object sender, RoutedEventArgs e)
        {
            ListView listView = (ListView)sender;
            foreach (var item in listView.Items)
            {
                ListViewItem listViewItem = (ListViewItem)listView.ItemContainerGenerator.ContainerFromItem(item);
                if (listViewItem != null)
                {
                    TextBox countTextBox = FindVisualChild<TextBox>(listViewItem, "CountTb");
                    TextBlock totalPriceTextBlock = FindVisualChild<TextBlock>(listViewItem, "TotalPriceTb");

                    if (countTextBox != null && totalPriceTextBlock != null)
                    {
                        if (countTextBox.DataContext is BuscketItemFish product)
                        {
                            if (int.TryParse(countTextBox.Text, out int currentCount))
                            {
                                totalPriceTextBlock.Text = (product.Fish.Cost * currentCount).ToString();
                            }
                            else
                            {
                                totalPriceTextBlock.Text = "0";
                            }
                        }
                    }
                }
            }
        }

        private void CountTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }
    }
}
