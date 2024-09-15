
using _3DEUX1.IMMOBILIER.TG.Models;
using CommunityToolkit.Mvvm.Input;

namespace _3DEUX1.IMMOBILIER.TG.Views.PopupPersonaliser;

public partial class PopupContact : CommunityToolkit.Maui.Views.Popup
{
    private Post post;
    string message = string.Empty;

    public PopupContact(Post post)
	{
		InitializeComponent();
		BindingContext = this;
        this.post = post;
        message = $"Bonjour, je suis intéressé par votre annonce immobilière situer a {post.Localisation} au nom de \"{post.Name}\" que vous aver mise sur l'application 3deux1imobilier.";

    }

    [RelayCommand]
    private async Task ContactForWhatsapp()
    {
        if (!string.IsNullOrEmpty(post.UserNum))
        {

            try
            {
                var url = $"https://wa.me/{post.UserNum}?text={Uri.EscapeDataString(message)}";
                await Launcher.OpenAsync(url);
            }
            catch
            {
                // Gérer l'exception, par exemple afficher une alerte
                await Shell.Current.DisplayAlert("Erreur", "Impossible d'ouvrir WhatsApp.", "OK");
            }
        }
        else
        {
            await Shell.Current.DisplayAlert("Erreur", "Aucun numéro de téléphone disponible.", "OK");
        }
    }

    [RelayCommand]
    public async Task ContactForSMS()
    {
        if (!string.IsNullOrEmpty(post.UserNum))
        {
            try
            {
                var smsUri = new Uri($"sms:{post.UserNum}?body={Uri.EscapeDataString(message)}");
                await Launcher.OpenAsync(smsUri);
            }
            catch
            {
                await Shell.Current.DisplayAlert("Erreur", "Impossible d'ouvrir l'application SMS.", "OK");
            }
        }
        else
        {
            await Shell.Current.DisplayAlert("Erreur", "Aucun numéro de téléphone disponible.", "OK");
        }
    }

    [RelayCommand]
    public async Task ContactForCall()
    {
        if (!string.IsNullOrEmpty(post.UserNum))
        {
            try
            {
                PhoneDialer.Open(post.UserNum);
            }
            catch
            {
                await Shell.Current.DisplayAlert("Erreur", "Impossible de passer l'appel.", "OK");
            }
        }
        else
        {
            await Shell.Current.DisplayAlert("Erreur", "Aucun numéro de téléphone disponible.", "OK");
        }
    }
}