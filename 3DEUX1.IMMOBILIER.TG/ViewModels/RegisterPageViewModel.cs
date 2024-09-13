using _3DEUX1.IMMOBILIER.TG.Models;
using _3DEUX1.IMMOBILIER.TG.Services;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace _3DEUX1.IMMOBILIER.TG.ViewModels
{
    public partial class RegisterPageViewModel : ObservableObject
    {
        readonly IUserService _userService = new UserService(new HttpClient());

        [ObservableProperty]
        public RegisterModel _registerModel;
        [ObservableProperty]
        public bool _noValideEmail = false;
        [ObservableProperty]
        public bool _noValidePasword = false;
        [ObservableProperty]
        public bool _noValideConfirmPass = false;
        [ObservableProperty]
        public bool _noValideUsername = false;
        public RegisterPageViewModel()
        {
            RegisterModel = new RegisterModel();
        }
        [RelayCommand]
        public async Task Register()
        {
            NoValideUsername = false; NoValideEmail = false; NoValidePasword = false; NoValideConfirmPass = false;
            if (!string.IsNullOrWhiteSpace(RegisterModel.UserName) && !string.IsNullOrWhiteSpace(RegisterModel.EmailAddress) && !string.IsNullOrWhiteSpace(RegisterModel.Password) && !string.IsNullOrWhiteSpace(RegisterModel.ConfirmPassword))
            {
                if (!IsValidEmail(RegisterModel.EmailAddress)) NoValideEmail = true;

                if (!ValidatePassword(RegisterModel.Password)) NoValidePasword = true;

                if (string.IsNullOrEmpty(RegisterModel.UserName)) NoValideUsername = true;

                if ( !(RegisterModel.Password == RegisterModel.ConfirmPassword) ) NoValideConfirmPass = true;


                if (!NoValideEmail && !NoValidePasword && !NoValideConfirmPass)
                {
                    
                    var popup = new Views.PopupPersonalise(new PopupPersonaliseViewModel());
                    //Shell.Current.ShowPopup(popup);
                    App.Current!.MainPage!.ShowPopup(popup);
                    RegisterModelSend model = new() { EmailAddress = RegisterModel.EmailAddress, UserName = RegisterModel.UserName, Password = RegisterModel.ConfirmPassword };

                    var res = await _userService.Registre(model);

                    popup.Close();
                    if (res == true)
                    {
                        User user = new User( ) { Email = RegisterModel.EmailAddress, UserName = RegisterModel.UserName };
                        App.AppUser = user;
                        Preferences.Default.Set(nameof(App.AppUser), JsonConvert.SerializeObject(user));

                        //await Shell.Current.GoToAsync(nameof(Views.AccueilPage), true);
                        App.Current!.MainPage = new AppShell();
                    }
                }

                return;
            }
            await CreatSnackBar.SnackBarShow("Veuillez remplir tous les champs !");
        }

        [RelayCommand]
        public void Skipe()
        {
            //await Shell.Current.GoToAsync(nameof(Views.AccueilPage), true);
            App.Current!.MainPage = new AppShell();
        }
        private bool ValidatePassword(string password)
        {
            // Au moins 8 caractères, au moins une lettre majuscule, une lettre minuscule, un chiffre et un caractère spécial
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$";

            Regex regex = new Regex(pattern);

            return regex.IsMatch(password);
        }

        private bool IsValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            Regex regex = new Regex(pattern);

            return regex.IsMatch(email);
        }

    }
}
