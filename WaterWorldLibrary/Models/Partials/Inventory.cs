using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WaterWorldLibrary.Models
{
    public partial class Inventory
    {
        public Visibility Visibility
        {
            get
            {

                if (CountInvent == 0)
                {
                    return Visibility.Collapsed;
                }
                else { return Visibility.Visible; }
            }
        }
        public string IsAvalible
        {
            get
            {


                if (CountInvent == 0)
                {
                    return $"Нет в наличии";
                }
                else { return $"В наличии"; }
            }
        }

        public SolidColorBrush ColorAv
        {
            get
            {

                if (CountInvent == 0)
                {
                    return Brushes.Red;
                }
                else { return Brushes.LightGreen; }
            }
        }
    }
    public class BuscketItemInvent
    {
        public Inventory Inventory { get; set; }

        public int Count { get; set; }
    }

}
