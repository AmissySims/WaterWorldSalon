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
using System.Windows.Shapes;
using WaterWorldLibrary.Models;

namespace Administrator.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddEditInventTypePage.xaml
    /// </summary>
    public partial class AddEditInventTypeWin : Window
    {
        TypeInventory contextTypein;
        DbPropertyValues oldValues;
        public AddEditInventTypeWin(TypeInventory typeInventory)
        {
            InitializeComponent();
            contextTypein = typeInventory;
            DataContext = typeInventory;
            if (contextTypein.Id != 0)
            {
                oldValues = App.db.Entry(contextTypein).CurrentValues.Clone();
            }

        }

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DialogResult = true;
        }
        private void TitleTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetter(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void SaveBt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(contextTypein.Title))
                {
                    MessageBox.Show("Заполните поле названия", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    if (contextTypein.Id == 0)
                    {
                        App.db.TypeInventory.Add(contextTypein);
                    }
                    App.db.SaveChanges();
                    MessageBox.Show("Сохранено", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    DialogResult = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelBt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (oldValues != null)
                {
                    App.db.Entry(contextTypein).CurrentValues.SetValues(oldValues);
                }
                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
