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
    /// Логика взаимодействия для AquariumsPage.xaml
    /// </summary>
    public partial class AquariumsPage : Page
    {
        public AquariumsPage()
        {
            InitializeComponent();
            Refresh();
        }
        public void Refresh()
        {
            var aqua = App.db.Aquarium.ToList();
            AquaList.ItemsSource = aqua;
        }
        private void EatBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var corm = App.db.Inventory.FirstOrDefault(x => x.Id == 14);

                if(corm.CountInvent > 0)
                {
                    corm.CountInvent -= 1;
                    App.db.SaveChanges();
                    MessageBox.Show("Рыбки покормлены", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    Refresh();
                }
                else
                {
                    MessageBox.Show("Недостаточно корма", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
    }
}
