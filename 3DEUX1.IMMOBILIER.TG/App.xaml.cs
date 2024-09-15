using _3DEUX1.IMMOBILIER.TG.Models;
using _3DEUX1.IMMOBILIER.TG.Services;
using _3DEUX1.IMMOBILIER.TG.Views;
using Newtonsoft.Json;

namespace _3DEUX1.IMMOBILIER.TG
{
    public partial class App : Application
    {
        // Propriété statique pour stocker l'utilisateur actuel
        public static User? AppUser { get; set; }

        public App()
        {
            InitializeComponent();
            // Définit la page principale en fonction de la vérification de l'utilisateur
            MainPage = IsUserVerified() ? new AppShell() : new LoginPage(new ViewModels.LoginPageViewModel());
        }

        // Vérifie si l'utilisateur est authentifié
        private bool IsUserVerified()
        {
            return new UserService(new HttpClient()).UserVerifier().Result;
        }

        // Méthode de vérification asynchrone de l'utilisateur (actuellement commentée)
        //private async Task VerifyUserAsync()
        //{
        //    UserService service = new();
        //    if (App.AppUser != null)
        //    {
        //        var res = await service.RefreshLogin(App.AppUser!.Email);
        //        Preferences.Default.Set(nameof(App.AppUser), JsonConvert.SerializeObject(res));
        //        App.AppUser = res;
        //        if (res == null) App.Current!.MainPage = new LoginPage(new ViewModels.LoginPageViewModel());
        //    }
        //}
    }
}
