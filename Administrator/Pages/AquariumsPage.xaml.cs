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
        private void AddFAquariumBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Aquarium aqua = new Aquarium
                {
                    
                };
                App.db.Aquarium.Add(aqua);
                
                App.db.SaveChanges();
                aqua.NumberAquarium = aqua.Id;
                MessageBox.Show("Добавлен аквариум", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                Refresh();


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
