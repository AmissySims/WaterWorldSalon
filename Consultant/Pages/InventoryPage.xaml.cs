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

namespace Consultant.Pages
{
    /// <summary>
    /// Логика взаимодействия для InventoryPage.xaml
    /// </summary>
    public partial class InventoryPage : Page
    {
        public InventoryPage()
        {
            InitializeComponent();
            var type = App.db.TypeInventory.ToList();
            type.Insert(0, new TypeInventory() { Title = "Все" });
            FiltrCb.ItemsSource = type;
            SortCb.Items.Add("Все");
            SortCb.Items.Add("от А до Я");
            SortCb.Items.Add("от Я до А");
            SortCb.Items.Add("По цене мин.");
            SortCb.Items.Add("По цене макс.");
            Refresh();
        }
        private void Refresh()
        {
            var found = FoundTb.Text.ToLower();
            var type = FiltrCb.SelectedItem as TypeInventory;
            var invent = App.db.Inventory.ToList();
            if (!string.IsNullOrEmpty(found))
            {
                invent = App.db.Inventory.Where(x => x.Title.ToLower().Contains(found)).ToList();
            }

            if (type != null && type.Id != 0)
            {
                invent = invent.Where(x => x.TypeInventoryId == type.Id).ToList();
            }

            if (SortCb.SelectedIndex == 1)
            {
                invent = invent.OrderBy(x => x.Title).ToList();
            }
            if (SortCb.SelectedIndex == 2)
            {
                invent = invent.OrderByDescending(x => x.Title).ToList();
            }
            if (SortCb.SelectedIndex == 3)
            {
                invent = invent.OrderBy(x => x.CostInvent).ToList();
            }
            if (SortCb.SelectedIndex == 4)
            {
                invent = invent.OrderByDescending(x => x.CostInvent).ToList();
            }

            InventList.ItemsSource = invent;
        }
        private void FiltrCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        private void SortCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        private void FoundTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void LookBt_Click(object sender, RoutedEventArgs e)
        {
            var selInvent = (sender as Button).DataContext as Inventory;
            NavigationService.Navigate(new LookPage(selInvent));
        }

        private void BuscketBt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedProduct = (sender as Button).DataContext as Inventory;

                BusketInventory bucket = new BusketInventory
                {
                    CountI = 1,
                    UserId = CurrentUser.AuthUser.Id,
                    InventoryId = selectedProduct.Id
                };

                var prodInBucket = App.db.BusketInventory.Where(b => b.InventoryId == bucket.InventoryId && b.UserId == CurrentUser.AuthUser.Id).FirstOrDefault();
                if (prodInBucket != null)
                {
                    MessageBox.Show("Данный товар уже присутствует в корзине", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                App.db.BusketInventory.Add(bucket);
                App.db.SaveChanges();
                MessageBoxResult result = MessageBox.Show("Товар добавлен в корзину. Хотите перейти в корзину сейчас?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    NavigationService.Navigate(new BusketinventPage());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении в корзину: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }
    }
}
