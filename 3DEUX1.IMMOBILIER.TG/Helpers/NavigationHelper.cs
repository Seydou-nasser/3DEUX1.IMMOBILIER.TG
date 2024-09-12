using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DEUX1.IMMOBILIER.TG.Helpers
{
    public class NavigationHelper
    {
        public static Task NavigateAsync(string route, Dictionary<string, object> parammeter = null!)
        {
            if (parammeter == null)
                return Shell.Current.GoToAsync(route, null);
            else
                return Shell.Current.GoToAsync(route, parammeter);
        }
    }
}
