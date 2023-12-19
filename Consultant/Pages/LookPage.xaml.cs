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

       
    }
}
