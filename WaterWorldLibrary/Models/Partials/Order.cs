using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WaterWorldLibrary.Models
{
    public partial class Order
    {
        public Visibility AdressVisible
        {
            get
            {
                if (DeliveryTypeId == 1)
                {
                    return Visibility.Collapsed;

                }
                else
                { return Visibility.Visible; }

            }
        }
        public Visibility AdressVisible1
        {
            get
            {
                if (DeliveryTypeId == 2)
                {
                    return Visibility.Collapsed;

                }
                else
                { return Visibility.Visible; }

            }
        }
        public Visibility DateVisible
        {
            get
            {
                if (Date == null)
                {
                    return Visibility.Collapsed;

                }
                else
                { return Visibility.Visible; }

            }
        }
        public Visibility EditVisible
        {
            get
            {
                if (StatusOrderId == 6 || StatusOrderId == 7)
                {
                    return Visibility.Collapsed;

                }
                else
                { return Visibility.Visible; }

            }
        }

        public Visibility PointVisible
        {
            get
            {
                if (DeliveryTypeId == 1 && StatusOrderId == 5)
                {
                    return Visibility.Visible;

                }
                else
                { return Visibility.Collapsed; }

            }
        }
        
    }
}
