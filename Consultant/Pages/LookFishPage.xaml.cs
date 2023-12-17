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

namespace Consultant.Pages
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
            var aqua = App.db.Aquarium.ToList();
            AquaCb.ItemsSource = aqua;
            
            contextFish = fish;
            DataContext = contextFish;
            if (contextFish.Id != 0)
            {
                oldValues = App.db.Entry(contextFish).CurrentValues.Clone();
            }

            if (contextFish.CountFish == 0)
            {

                BusketBt.Visibility = Visibility.Collapsed;
            }
            else { BusketBt.Visibility = Visibility.Visible; }
        }
        private void SaveBt_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                
                if (contextFish.Aquarium == null)
                {
                    MessageBox.Show("Выберите аквариум", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
             
                else
                {
                    if (contextFish.Id == 0)
                    {
                        App.db.Fish.Add(contextFish);
                    }
                    App.db.SaveChanges();
                    MessageBox.Show("Сохранено", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.GoBack();

                }




            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
