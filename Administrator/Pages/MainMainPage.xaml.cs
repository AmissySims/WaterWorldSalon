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

namespace Administrator.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainMainPage.xaml
    /// </summary>
    public partial class MainMainPage : Page
    {
        public MainMainPage()
        {
            InitializeComponent();
        }

        private void FishBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new FishPage());
        }

        private void AquariumBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AquariumsPage());
        }

        private void InventoryBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new InventoryPage());
        }

        private void OrdersBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new OrdersPage());
        }

        private void UsersBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UsersPage());
        }

        private void AccountBtn_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new AccountPage());
        }

        private void PointsBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DeliveryPointsPage());
        }

        private void TypesBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TypesMainPage());
        }
    }
}
