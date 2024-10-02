using System.Globalization;

namespace _3DEUX1.IMMOBILIER.TG.Helpers
{
    public class KeywordToCustomTextConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is string inputString)
            {
                if (inputString.Contains("superficie", StringComparison.OrdinalIgnoreCase))
                    return "Superficie";
                else if (inputString.Contains("chambre", StringComparison.OrdinalIgnoreCase))
                    return "Nombre de chambres";
                else if (inputString.Contains("Marque", StringComparison.OrdinalIgnoreCase))
                    return "Marque du véhicule";
                else if (inputString.Contains("Modèle", StringComparison.OrdinalIgnoreCase))
                    return "Modèle du véhicule";
                else if (inputString.Contains("Année", StringComparison.OrdinalIgnoreCase))
                    return "Année de fabrication";
                else if (inputString.Contains("Capacité", StringComparison.OrdinalIgnoreCase))
                    return "Capacité d'accueil";
            }

            return string.Empty;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
