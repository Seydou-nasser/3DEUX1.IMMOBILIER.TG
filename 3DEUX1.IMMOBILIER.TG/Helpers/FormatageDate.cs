using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DEUX1.IMMOBILIER.TG.Helpers
{
    public class FormatageDate : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            DateTime currentDate = DateTime.Now; // Date actuelle

            if (value is DateTime time)
            {
                int monthsElapsed = ((currentDate.Year - time.Year) * 12) + currentDate.Month - time.Month;
                //string var = $"Mise en ligne :{time.Day}/0{time.Month}/{time.Year}";

                string formattedDate;

                if (monthsElapsed == 0)
                {
                    int daysElapsed = (currentDate - time).Days;
                    if (daysElapsed == 0)
                    {
                        return formattedDate = $"Aujourd'hui";
                    }
                    return formattedDate = $"il y a {daysElapsed} jours";
                }
                else if (monthsElapsed < 12)
                {
                    return formattedDate = $"il y a {monthsElapsed} mois";
                }
                else
                {
                    return formattedDate = currentDate.ToString("dd MMMM yyyy"); // Format de date normal
                }
            }

            return null;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
