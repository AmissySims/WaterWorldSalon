using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Security.RightsManagement;
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
    /// Логика взаимодействия для AddEditFishPage.xaml
    /// </summary>
    public partial class AddEditFishPage : Page
    {
        Fish contextFish;
        DbPropertyValues oldValues;
        public AddEditFishPage(Fish fish)
        {
            InitializeComponent();
            var aqua = App.db.Aquarium.ToList();
            AquaCb.ItemsSource = aqua;
            var typeF = App.db.TypeFish.ToList();
            TypeCb.ItemsSource = typeF;
            contextFish = fish;
            DataContext = contextFish;
            if (contextFish.Id != 0)
            {
                oldValues = App.db.Entry(contextFish).CurrentValues.Clone();
            }
        }
        private void SaveBt_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (string.IsNullOrEmpty(contextFish.Title))
                {
                    MessageBox.Show("Заполните поле названия", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (string.IsNullOrEmpty(contextFish.Description))
                {
                    MessageBox.Show("Заполните поле описания", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }


                if (contextFish.Cost < 0 || CostFTb.Text.Length <= 0)
                {
                    MessageBox.Show("Заполните поле цены", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (contextFish.CountFish < 0 || CountFTb.Text.Length <= 0)
                {
                    MessageBox.Show("Заполните поле количества", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (contextFish.Aquarium == null)
                {
                    MessageBox.Show("Выберите аквариум", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (contextFish.TypeFish == null)
                {
                    MessageBox.Show("Выберите тип", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;

                }
                if (contextFish.CountFish < 0)
                {
                    MessageBox.Show("Значение количества не может быть меньше 0", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (contextFish.Cost < 0)
                {
                    MessageBox.Show("Значение цены не может быть меньше 0", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    if (contextFish.Id == 0)
                    {
                        App.db.Fish.Add(contextFish);
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
                    contextFish.PhotoFish = File.ReadAllBytes(dialog.FileName);
                    DataContext = null;
                    DataContext = contextFish;
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

        private void TextBox_PreviewTextInput_1(object sender, TextCompositionEventArgs e)
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
                    App.db.Entry(contextFish).CurrentValues.SetValues(oldValues);

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
