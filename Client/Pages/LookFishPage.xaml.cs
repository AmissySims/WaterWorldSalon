using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
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
    /// Логика взаимодействия для LookFishPage.xaml
    /// </summary>
    public partial class LookFishPage : Page
    {
        Fish contextFish;
        DbPropertyValues oldValues;
        public LookFishPage(Fish fish)
        {
            InitializeComponent();
           

            contextFish = fish;
            DataContext = contextFish;
            if (contextFish.Id != 0)
            {
                oldValues = App.db.Entry(contextFish).CurrentValues.Clone();
            }
        }

        private void CancelBt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (oldValues != null)
                {
                    App.db.Entry(contextFish).CurrentValues.SetValues(oldValues);

                }
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void BusketBt_Click(object sender, RoutedEventArgs e)
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

                var prodInBucket = App.db.BusketFish.Where(b => b.FishId == bucket.FishId).FirstOrDefault();
                if (prodInBucket != null) { MessageBox.Show("Данный товар уже присутствует в корзине", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information); return; };

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
