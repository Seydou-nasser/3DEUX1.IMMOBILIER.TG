using System.Globalization;

namespace _3DEUX1.IMMOBILIER.TG.Helpers
{
    public class KeywordToIconPathConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is string inputString)
            {
                if (inputString.Contains("superficie", StringComparison.OrdinalIgnoreCase))
                    return "surface.png";
                else if (inputString.Contains("chambre", StringComparison.OrdinalIgnoreCase))
                    return "chambre.png";
                else if (inputString.Contains("Marque", StringComparison.OrdinalIgnoreCase))
                    return "marque.png";
                else if (inputString.Contains("Modèle", StringComparison.OrdinalIgnoreCase))
                    return "modele.png";
                else if (inputString.Contains("Année", StringComparison.OrdinalIgnoreCase))
                    return "annee.png";
                else if (inputString.Contains("Capacité", StringComparison.OrdinalIgnoreCase))
                    return "capacite.png";
            }

            return string.Empty;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
