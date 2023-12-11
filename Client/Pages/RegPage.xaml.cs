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
using System.Xml.Linq;
using WaterWorldLibrary.Models;

namespace Client.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegPage.xaml
    /// </summary>
    public partial class RegPage : Page
    {
        public RegPage()
        {
            InitializeComponent();
        }
        private void CElAuthBtn(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new AuthPage());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void CElRegBtn(object sender, RoutedEventArgs e)
        {
            try
            {
                var check = App.db.User.FirstOrDefault(x => x.Login == LoginTb.Text);
                
                if (string.IsNullOrEmpty(FNameTb.Text))
                {
                    MessageBox.Show("Заполните поле фамилии", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (string.IsNullOrEmpty(NameTb.Text))
                {
                    MessageBox.Show("Заполните поле имени", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (string.IsNullOrEmpty(LoginTb.Text))
                {
                    MessageBox.Show("Заполните поле логина", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (string.IsNullOrEmpty(PasswordPb.Password))
                {
                    MessageBox.Show("Заполните поле пароля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (check == null)
                {
                    User user = new User
                    {


                        FName = FNameTb.Text,
                        Name = NameTb.Text,
                        RoleId = 3,
                        Login = LoginTb.Text,
                        Password = PasswordPb.Password
                    };
                    App.db.User.Add(user);
                    App.db.SaveChanges();
                    MessageBox.Show("Зарегистрировано", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.Navigate(new AuthPage());


                }
                else
                {
                    MessageBox.Show("Пользователь с таким логином уже существует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
