using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterWorldLibrary.Models;

namespace Administrator.Partials
{
    public class CurrentUser
    {
        public static User AuthUser = null;
        public static bool isAuth = false;
    }
}
