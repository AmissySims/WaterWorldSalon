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

        }
    }
}
