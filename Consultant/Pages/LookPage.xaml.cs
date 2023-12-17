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
    /// Логика взаимодействия для LookPage.xaml
    /// </summary>
    public partial class LookPage : Page
    {
        Inventory contextInvent;
        DbPropertyValues oldValues;
        public LookPage(Inventory inventory)
        {
            InitializeComponent();
            contextInvent = inventory;
            DataContext = contextInvent;
            if (contextInvent.Id != 0)
            {
                oldValues = App.db.Entry(contextInvent).CurrentValues.Clone();
            }

            if (contextInvent.CountInvent == 0)
            {
                BuscketBt.Visibility = Visibility.Collapsed;
            }
            else { BuscketBt.Visibility = Visibility.Visible; }
        }

        private void CancelBt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (oldValues != null)
                {
                    App.db.Entry(contextInvent).CurrentValues.SetValues(oldValues);

                }
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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

                var prodInBucket = App.db.BusketInventory.Where(b => b.InventoryId == bucket.InventoryId).FirstOrDefault();
                if (prodInBucket != null) { MessageBox.Show("Данный товар уже присутствует в корзине", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information); return; };

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
