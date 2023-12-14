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
    /// Логика взаимодействия для UsersPage.xaml
    /// </summary>
    public partial class UsersPage : Page
    {
        public UsersPage()
        {
            InitializeComponent();
            var role = App.db.Role.ToList();
            role.Insert(0, new Role() { Title = "Все" });
            SortCb.ItemsSource = role;



        }
        public void Refresh()
        {
            var selRole = SortCb.SelectedItem as Role;
            var users = App.db.User.ToList();
            if (selRole != null && selRole.Id != 0)
            {
                users = users.Where(x => x.RoleId == selRole.Id).ToList();
            }
            UsersList.ItemsSource = users;
        }
        private void SortCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        private void AddBt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditUserPage(new User()));
        }

        private void DeleteBt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selUser = (sender as Button).DataContext as User;
                var selOrder = selUser.Order;
           

                App.db.Order.RemoveRange(selOrder);
                App.db.User.Remove(selUser);
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
            try
            {
                var selUser = (sender as Button).DataContext as User;
                NavigationService.Navigate(new AddEditUserPage(selUser));
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
