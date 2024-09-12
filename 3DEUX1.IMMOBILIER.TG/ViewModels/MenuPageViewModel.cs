using _3DEUX1.IMMOBILIER.TG.Models;
using _3DEUX1.IMMOBILIER.TG.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace _3DEUX1.IMMOBILIER.TG.ViewModels
{
    public partial class MenuPageViewModel : ObservableObject
    {
        [ObservableProperty]
        public User _userMenuPage;
        public MenuPageViewModel() 
        {
            UserMenuPage = new();
            if (App.AppUser != null)
            {
                UserMenuPage = App.AppUser;
            }
        }
        [RelayCommand]
        public async Task ToLoginPage()
        {
            if (App.AppUser == null)
            {
                await Shell.Current.GoToAsync(nameof(LoginPage));
            }
            
        } 
    }
}
