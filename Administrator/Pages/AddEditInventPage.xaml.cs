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

namespace Administrator.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditInventPage.xaml
    /// </summary>
    public partial class AddEditInventPage : Page
    {
        Inventory contextInvent;
        DbPropertyValues oldValues;
        public AddEditInventPage(Inventory invent)
        {
            InitializeComponent();
            var typeInvent = App.db.TypeInventory.ToList();
            TypeCb.ItemsSource = typeInvent;
            contextInvent = invent;
            DataContext = contextInvent;
           
           
            if (contextInvent.Id != 0)
            {
                oldValues = App.db.Entry(contextInvent).CurrentValues.Clone();
            }

        }

        private void SaveBt_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (string.IsNullOrEmpty(contextInvent.Title))
                {
                    MessageBox.Show("Заполните поле названия", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (string.IsNullOrEmpty(contextInvent.Description))
                {
                    MessageBox.Show("Заполните поле описания", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (contextInvent.CostInvent == null)
                {
                    MessageBox.Show("Заполните поле цены", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (contextInvent.CountInvent == null)
                {
                    MessageBox.Show("Заполните поле количества", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
               
                if (contextInvent.TypeInventory == null)
                {
                    MessageBox.Show("Выберите тип", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if(contextInvent.CountInvent < 0)
                {
                    MessageBox.Show("Значение количества не может быть меньше 0", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (contextInvent.CostInvent < 0)
                {
                    MessageBox.Show("Значение цены не может быть меньше 0", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
        private void AddImageBt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dialog = new OpenFileDialog();
                if (dialog.ShowDialog().GetValueOrDefault())
                {
                    contextInvent.PhotoInvent = File.ReadAllBytes(dialog.FileName);
                    DataContext = null;
                    DataContext = contextInvent;
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
                    App.db.Entry(contextInvent).CurrentValues.SetValues(oldValues);

                }
                NavigationService.GoBack();
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

        private void TextBox_PreviewTextInput_1(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetter(e.Text, 0))
            {
                e.Handled = true;
            }
        }
    }
}
