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

        private void SaveBt_Click(object sender, RoutedEventArgs e)
        {
            try
            {


              
                

                if (contextInvent.CountInvent < 0 || CountInvTb.Text.Length <= 0)
                {
                    MessageBox.Show("Заполните поле количество", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (contextInvent.CountInvent < 0)
                {
                    MessageBox.Show("Значение количества не может быть меньше 0", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    if (contextInvent.Id == 0)
                    {
                        App.db.Inventory.Add(contextInvent);
                    }
                    App.db.SaveChanges();
                    MessageBox.Show("Сохранено", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.GoBack();

                }




            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }
    }
}
