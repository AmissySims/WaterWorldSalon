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
    /// Логика взаимодействия для FishPage.xaml
    /// </summary>
    public partial class FishPage : Page
    {
        TypeFish selType;
        public FishPage()
        {
            InitializeComponent();
            selType = null;
            SortCb.Items.Add("Все");
            SortCb.Items.Add("от А до Я");
            SortCb.Items.Add("от Я до А");
            SortCb.Items.Add("По цене мин.");
            SortCb.Items.Add("По цене макс.");
        }

        private void Refresh()
        {
            var found = FoundTb.Text.ToLower();
            var fish = App.db.Fish.ToList();
            if (!string.IsNullOrEmpty(found))
            {
                fish = App.db.Fish.Where(x => x.Title.ToLower().Contains(found)).ToList();
            }
            if (selType != null)
            {
                fish = fish.Where(x => x.TypeFishId == selType.Id).ToList();
            }


            if (SortCb.SelectedIndex == 1)
            {
                fish = fish.OrderBy(x => x.Title).ToList();
            }
            if (SortCb.SelectedIndex == 2)
            {
                fish = fish.OrderByDescending(x => x.Title).ToList();
            }
            if (SortCb.SelectedIndex == 3)
            {
                fish = fish.OrderBy(x => x.Cost).ToList();
            }
            if (SortCb.SelectedIndex == 4)
            {
                fish = fish.OrderByDescending(x => x.Cost).ToList();
            }

            FishList.ItemsSource = fish;
        }


        private void FreshwaterFishBtn_Click(object sender, RoutedEventArgs e)
        {
            selType = App.db.TypeFish.FirstOrDefault(x => x.Id == 1);
            Refresh();
        }

       

        private void SeaFishBtn_Click(object sender, RoutedEventArgs e)
        {
            selType = App.db.TypeFish.FirstOrDefault(x => x.Id == 2);
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

      

        private void AllFishBtn_Click(object sender, RoutedEventArgs e)
        {
            selType = null;
            Refresh();
        }

        private void LookBt_Click(object sender, RoutedEventArgs e)
        {
            var selInvent = (sender as Button).DataContext as Fish;
            NavigationService.Navigate(new LookFishPage(selInvent));
        }

        private void BuscketBt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedProduct = (sender as Button).DataContext as Fish;

                BusketFish bucket = new BusketFish
                {
                    CountF = 1,
                    UserId = CurrentUser.AuthUser.Id,
                    FishId = selectedProduct.Id
                };

                var prodInBucket = App.db.BusketFish.Where(b => b.FishId == bucket.FishId && b.UserId == CurrentUser.AuthUser.Id).FirstOrDefault();
                if (prodInBucket != null)
                {
                    MessageBox.Show("Данный товар уже присутствует в корзине", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                App.db.BusketFish.Add(bucket);
                App.db.SaveChanges();
                MessageBoxResult result = MessageBox.Show("Товар добавлен в корзину. Хотите перейти в корзину сейчас?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    NavigationService.Navigate(new BuscketPage());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении в корзину: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }
    }
}
