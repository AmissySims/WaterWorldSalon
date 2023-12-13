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
    /// Логика взаимодействия для AddEditFishPage.xaml
    /// </summary>
    public partial class AddEditFishPage : Page
    {
        Fish contextFish;
        public AddEditFishPage(Fish fish)
        {
            InitializeComponent();
            contextFish = fish;
            DataContext = contextFish;
        }
    }
}
