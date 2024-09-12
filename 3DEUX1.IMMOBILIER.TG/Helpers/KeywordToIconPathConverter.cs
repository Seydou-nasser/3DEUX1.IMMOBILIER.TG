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
                {
                    return "surface.png";
                }
                else if (inputString.Contains("chambre", StringComparison.OrdinalIgnoreCase))
                {
                    return "chambre.png";
                }
                else if (inputString.Contains("salle de bain", StringComparison.OrdinalIgnoreCase))
                {
                    return "Texte personnalisé pour salle de bain";
                }
                // Ajoute d'autres conditions selon tes besoins
                else
                {
                    return "Texte par défaut";
                }
            }

            return string.Empty;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
