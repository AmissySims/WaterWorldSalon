using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.IO;
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
    /// Логика взаимодействия для EditUserPage.xaml
    /// </summary>
    public partial class EditUserPage : Page
    {
        User contextUser;
        DbPropertyValues oldValues;
        public EditUserPage(User user)
        {
            InitializeComponent();
            contextUser = user;
            DataContext = contextUser;
            if (contextUser.Id != 0)
            {
                oldValues = App.db.Entry(contextUser).CurrentValues.Clone();
            }
        }

        private void SaveBt_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (string.IsNullOrEmpty(contextUser.FName))
                {
                    MessageBox.Show("Заполните поле фамилии", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (string.IsNullOrEmpty(contextUser.Name))
                {
                    MessageBox.Show("Заполните поле имени", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (string.IsNullOrEmpty(contextUser.Login))
                {
                    MessageBox.Show("Заполните поле логина", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (string.IsNullOrEmpty(contextUser.Password))
                {
                    MessageBox.Show("Заполните поле пароля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                else
                {

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

        private void AddImageBt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dialog = new OpenFileDialog();
                if (dialog.ShowDialog().GetValueOrDefault())
                {
                    contextUser.PhotoUser = File.ReadAllBytes(dialog.FileName);
                    DataContext = null;
                    DataContext = contextUser;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetter(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void CancelBt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (oldValues != null)
                {
                    App.db.Entry(contextUser).CurrentValues.SetValues(oldValues);

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
