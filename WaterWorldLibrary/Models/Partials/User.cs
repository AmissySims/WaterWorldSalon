using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WaterWorldLibrary.Models
{
    public partial class User
    {
        public string FullName
        {
            get
            {
                return $"{FName} {Name} ";
            }
        }

        public Visibility AccVisible
        {
            get
            {
                if (Id == CurrentUser.AuthUser.Id)
                    return Visibility.Collapsed;
                else
                    return Visibility.Visible;
            }
        }
    }
}
