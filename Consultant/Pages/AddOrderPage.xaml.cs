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

namespace Consultant.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddOrderPage.xaml
    /// </summary>
    public partial class AddOrderPage : Page
    {
        public List<BuscketItemFish> Busket { get; set; }
        public AddOrderPage(List<BuscketItemFish> bucketList)
        {

            InitializeComponent();
            Busket = bucketList;
            var points = App.db.DeliveryPoint.ToList();
            DeliveryPointCb.ItemsSource = points;
            var custs = App.db.User.Where(x => x.RoleId == 3).ToList();
            CustomerCb.ItemsSource = custs;
            DateTb.Text = DateTime.Now.ToString("dd.MM.yyyy");

          
            Adress.Visibility = Visibility.Hidden;
            IfPickup.Visibility = Visibility.Hidden;

            PriceTb.Text = $"{Convert.ToString(Busket.Sum(b => b.Count * b.Fish.Cost)) + " ₽"}";
        }
        private void Pickup_Checked(object sender, RoutedEventArgs e)
        {
            IfPickup.Visibility = Visibility.Visible;
            Adress.Visibility = Visibility.Hidden;
        }

        private void Pickup_Unchecked(object sender, RoutedEventArgs e)
        {
            IfPickup.Visibility = Visibility.Visible;
            Adress.Visibility = Visibility.Visible;
        }

        private void Courier_Checked(object sender, RoutedEventArgs e)
        {
            IfPickup.Visibility = Visibility.Hidden;
            Adress.Visibility = Visibility.Visible;
        }

        private void Courier_Unchecked(object sender, RoutedEventArgs e)
        {
            IfPickup.Visibility = Visibility.Visible;
        }




        private void DeliveryPointCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void OrderBt_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (App.db.BusketFish.ToList().Count <= 0)
                {
                    MessageBox.Show("Ваша корзина пуста", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                Order ord = new Order();
                {
                    ord.UserId = (CustomerCb.SelectedItem as User).Id;
                    ord.StatusOrderId = 1;

                    if (Courier.IsChecked == true)
                    {
                        ord.DeliveryTypeId = 2;
                        ord.DeliveryPointId = null;
                        ord.AdressToDelivery = AdressTb.Text;
                    }
                    else if (Pickup.IsChecked == true)
                    {
                        ord.DeliveryTypeId = 1;
                        ord.DeliveryPointId = (DeliveryPointCb.SelectedItem as DeliveryPoint).Id;
                        ord.AdressToDelivery = null;

                    }
                    else
                    {
                        MessageBox.Show("Выберите тип доставки", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    ord.Date = DateTime.Now;
                    ord.Price = Busket.Sum(b => b.Count * b.Fish.Cost);
                }
                //Добавление в Order
                App.db.Order.Add(ord);

                //Удаление корзины
                var bucketsToRemove = App.db.BusketFish.Where(b => b.UserId == CurrentUser.AuthUser.Id).ToList();
                App.db.BusketFish.RemoveRange(bucketsToRemove);

                //Добавление в OrderProduct
                foreach (var b in Busket)
                {
                    var orderProduct = new OrderFish
                    {
                        FishId = b.Fish.Id,
                        CountFishOrder = b.Count,
                        OrderId = ord.Id
                    };


                    //Минус товар на складе
                    BuscketItemFish selectedProd = App.db.Fish
                            .Where(p => p.Id == orderProduct.FishId)
                            .Select(p => new BuscketItemFish
                            {
                                Fish = p,
                                Count = (int)p.CountFish
                            })
                            .FirstOrDefault();
                    selectedProd.Fish.CountFish -= b.Count;


                    App.db.OrderFish.Add(orderProduct);


                }

                //Сохранение

                App.db.SaveChanges();
                MessageBox.Show("Заказ успешно добавлен", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.Navigate(new MainMainPage());



            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при оформлении заказа: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
