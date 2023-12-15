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
using System.Windows.Shapes;
using WaterWorldLibrary.Models;

namespace Consultant.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditStatusWin.xaml
    /// </summary>
    public partial class EditStatusWin : Window
    {
        Order contextOrder;
        public EditStatusWin(Order order)
        {
            InitializeComponent();
            StatusCb.ItemsSource = App.db.StatusOrder.ToList();
            contextOrder = order;
            DataContext = contextOrder;
        }
        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DialogResult = true;
        }
        private void SaveBt_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (contextOrder.StatusOrder == null)
                {
                    MessageBox.Show("Выберите статус", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    App.db.SaveChanges();
                    

                    MessageBox.Show("Сохранено", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    DialogResult = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
