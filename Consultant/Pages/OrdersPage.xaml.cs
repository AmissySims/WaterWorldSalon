using Consultant.Windows;
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
    /// Логика взаимодействия для OrdersPage.xaml
    /// </summary>
    public partial class OrdersPage : Page
    {
        public OrdersPage()
        {
            InitializeComponent();
            Refresh();
        }
        private void Refresh()
        {


            var ord = App.db.Order.Where(z => z.DeliveryTypeId == 1).ToList();




            OrdersList.ItemsSource = ord;
        }
        
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void EditOrdBt_Click(object sender, RoutedEventArgs e)
        {
            EditStatusWin editOrder = new EditStatusWin((sender as Button).DataContext as Order);
            editOrder.ShowDialog();
            Refresh();
        }
    }
}
