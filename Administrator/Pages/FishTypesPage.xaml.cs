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
    /// Логика взаимодействия для FishTypesPage.xaml
    /// </summary>
    public partial class FishTypesPage : Page
    {
        public FishTypesPage()
        {
            InitializeComponent();
            Refresh();
        }

        public void Refresh()
        {

            var cat = App.db.TypeFish.ToList();

            CatList.ItemsSource = cat;
        }
        private void AddBt_Click(object sender, RoutedEventArgs e)
        {
            AddEditFishTypeWin editCategory = new AddEditFishTypeWin(new TypeFish());
            editCategory.ShowDialog();
            Refresh();
        }

        private void DeleteBt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selCat = (sender as Button).DataContext as TypeFish;
                var selProd = App.db.Fish.Where(x => x.TypeFishId == selCat.Id);
                App.db.Fish.RemoveRange(selProd);
                App.db.TypeFish.Remove(selCat);
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
            AddEditFishTypeWin editCategory = new AddEditFishTypeWin((sender as Button).DataContext as TypeFish);
            editCategory.ShowDialog();
            Refresh();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
    }
}
