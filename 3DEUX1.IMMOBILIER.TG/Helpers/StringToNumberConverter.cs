using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _3DEUX1.IMMOBILIER.TG.Helpers
{
    public class StringToNumberConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is string inputString)
            {
                string pattern = @"\d+";
                Match match = Regex.Match(inputString, pattern);
                if (match.Success)
                {
                    int number = int.Parse(match.Value);

                    if (inputString.Contains("superficie", StringComparison.OrdinalIgnoreCase))
                    {
                        return $"{number}m2";
                    }
                    return $"{number}";
                }
                else
                {
                    return "";
                }
            }

            return string.Empty;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
