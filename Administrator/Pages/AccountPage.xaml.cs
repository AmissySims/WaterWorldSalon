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
    /// Логика взаимодействия для AccountPage.xaml
    /// </summary>
    public partial class AccountPage : Page
    {
        public static List<User> Users { get; set; }
        public AccountPage()
        {
            InitializeComponent();
        }

        public void Refresh()
        {
            Users = App.db.User.Where(x => x.Id == CurrentUser.AuthUser.Id).ToList();
            Uselist.ItemsSource = Users;
        }

        private void EditBt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selUser = (sender as Button).DataContext as User;
               // NavigationService.Navigate(new AddEditUserPage(selUser));
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
