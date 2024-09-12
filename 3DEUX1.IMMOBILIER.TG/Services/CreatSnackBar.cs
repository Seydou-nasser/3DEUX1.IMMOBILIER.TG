using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Converters;
using CommunityToolkit.Maui.Core;
using Font = Microsoft.Maui.Font;

namespace _3DEUX1.IMMOBILIER.TG.Services
{
    public class CreatSnackBar
    {
        public static async Task SnackBarShow(string message)
        {
            CancellationTokenSource cancellationTokenSource = new
            CancellationTokenSource();
            var snackbarOptions = new SnackbarOptions
            {
                BackgroundColor = Colors.LightGray,
                TextColor = Colors.Black,
                ActionButtonTextColor = Colors.Yellow,
                CornerRadius = new CornerRadius(10),
                Font = Font.SystemFontOfSize(25),
                ActionButtonFont = Font.SystemFontOfSize(10),
                CharacterSpacing = 0
            };
            string text = message;
            string actionButtonText = string.Empty;
            Action? action = null;
            TimeSpan duration = TimeSpan.FromSeconds(5);
            var snackbar = Snackbar.Make(text, action, actionButtonText, duration, snackbarOptions);
            await snackbar.Show(cancellationTokenSource.Token);
        }
    }
}
