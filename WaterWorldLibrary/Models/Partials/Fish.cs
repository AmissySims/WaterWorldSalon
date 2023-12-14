using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace WaterWorldLibrary.Models
{
    public partial class Fish
    {
        public Visibility Visibility
        {
            get
            {

                if (CountFish == 0)
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


                if (CountFish == 0)
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

                if (CountFish == 0)
                {
                    return Brushes.Red;
                }
                else { return Brushes.LightGreen; }
            }
        }

        public SolidColorBrush ColorBl
        {
            get
            {

                if (CountFish == 0)
                {
                    return Brushes.LightGray;
                }
                else { return Brushes.Transparent; }
            }

        }


    }

    public class BuscketItem
    {
        public Fish Fish { get; set; }
        public int Count { get; set; }
    }
}

