using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WaterWorldLibrary.Models
{
    public partial class Aquarium
    {
       
        public int? CountFishes
        {

            get
            {
                

                if (Fish != null)
                {
                    return Fish.Sum(x => x.CountFish);
                }
                else
                    return 0;
                
            }
        }

        public Visibility Visibility
        {
            get
            {

                if (CountFishes == 0)
                {
                    return Visibility.Collapsed;
                }
                else { return Visibility.Visible; }
            }
        }
    }
}
