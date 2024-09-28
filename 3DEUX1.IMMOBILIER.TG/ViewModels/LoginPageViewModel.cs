using _3DEUX1.IMMOBILIER.TG.Models;
using _3DEUX1.IMMOBILIER.TG.Services;
using _3DEUX1.IMMOBILIER.TG.Views;
using _3DEUX1.IMMOBILIER.TG.Views.PopupPersonaliser;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;

namespace _3DEUX1.IMMOBILIER.TG.ViewModels
{
    public partial class LoginPageViewModel : ObservableObject
    {
        [ObservableProperty]
        public LoginModel _loginModel;

        [ObservableProperty]
        public bool _passwordIsVisible;
        [ObservableProperty]
        public string _passImg;

        readonly IUserService _userService = new UserService(new HttpClient());
        public LoginPageViewModel()
        {
            LoginModel = new LoginModel();
            PasswordIsVisible = true;
            PassImg = "eye.png";
            //OnLogin();

        }

        [RelayCommand]
        public async Task OnLogin()
        {
            if (!string.IsNullOrWhiteSpace(LoginModel.Email) && !string.IsNullOrWhiteSpace(LoginModel.Password))
            {
                var popup = new ChargementPopup();
                //Shell.Current.ShowPopup(popup);
                App.Current!.MainPage!.ShowPopup(popup);

                User? user = await _userService.Login(LoginModel.Email, LoginModel.Password);

                popup.Close();

                if (user is not null)
                {
                    App.AppUser = user;
                    Preferences.Default.Set(nameof(App.AppUser), JsonConvert.SerializeObject(user));

                    LoginModel.Email = string.Empty;
                    LoginModel.Password = string.Empty;
                    //await Shell.Current.GoToAsync(nameof(Views.AccueilPage), true);
                    App.Current!.MainPage = new AppShell();
                }
                return;
            }
            //await Shell.Current.DisplayAlert("Erreur","L'email ou le password ne peuvent etre vide", "reeseyer");
            await CreatSnackBar.SnackBarShow("veuillez mettre un email et mot de passe !");
        }
        [RelayCommand]
        public void GoToRegisterPage(RegisterPage registerPage)
        {
            //await Shell.Current.GoToAsync(nameof(Views.RegisterPage), true);
            App.Current!.MainPage = registerPage;
        }
        [RelayCommand]
        public void Skipe()
        {
            //await Shell.Current.GoToAsync(nameof(Views.AccueilPage), true);
            App.Current!.MainPage = new AppShell();
        }
        [RelayCommand]
        public void PassVisibility()
        {
            PasswordIsVisible = !PasswordIsVisible;
            if (PasswordIsVisible) PassImg = "eye.png";
            else PassImg = "feye.png";
        }
    }
}
