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

namespace Consultant.Pages
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

        private void AccountBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AccountPage());
        }

        private void BucketBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new BuscketPage());
        }

        private void AquariumBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AquariumsPage());
        }

        private void OrdersBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new OrdersPage());
        }

        private void InventoryBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new InventoryPage());
        }
    }
}
