using Administrator.Windows;
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
    /// Логика взаимодействия для InventTypesPage.xaml
    /// </summary>
    public partial class InventTypesPage : Page
    {
        public InventTypesPage()
        {
            InitializeComponent();
            Refresh();
        }
        public void Refresh()
        {
           
            var cat = App.db.TypeInventory.ToList();
          
            CatList.ItemsSource = cat;
        }
        private void AddBt_Click(object sender, RoutedEventArgs e)
        {
            AddEditInventTypeWin editCategory = new AddEditInventTypeWin(new TypeInventory());
            editCategory.ShowDialog();
            Refresh();
        }

        private void DeleteBt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selCat = (sender as Button).DataContext as TypeInventory;
                var selProd = App.db.Inventory.Where(x => x.TypeInventoryId == selCat.Id);
                App.db.Inventory.RemoveRange(selProd);
                App.db.TypeInventory.Remove(selCat);
                App.db.SaveChanges();
                MessageBox.Show("Удалено", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void EditBt_Click(object sender, RoutedEventArgs e)
        {
            AddEditInventTypeWin editCategory = new AddEditInventTypeWin((sender as Button).DataContext as TypeInventory);
            editCategory.ShowDialog();
            Refresh();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
    }
}
