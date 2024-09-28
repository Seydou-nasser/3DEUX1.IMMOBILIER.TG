using _3DEUX1.IMMOBILIER.TG.Helpers;
using _3DEUX1.IMMOBILIER.TG.Models;
using _3DEUX1.IMMOBILIER.TG.Views;
using _3DEUX1.IMMOBILIER.TG.Views.PopupPersonaliser;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace _3DEUX1.IMMOBILIER.TG.ViewModels
{
    public partial class MenuPageViewModel : ObservableObject
    {
        [ObservableProperty]
        public User _userMenuPage;
        [ObservableProperty]
        public bool _showAddPost = false;
        public MenuPageViewModel() 
        {
            UserMenuPage = new();
            if (App.AppUser != null)
            {
                UserMenuPage = App.AppUser;
                ShowAddPost = true;
            }

        }
        [RelayCommand]
        public async Task ToLoginPage()
        {
            if (App.AppUser == null)
            {
                //await Shell.Current.GoToAsync(nameof(LoginPage));
                await NavigationHelper.NavigateAsync(nameof(LoginPage));
            }
            
        }
        [RelayCommand]
        public void ToAddPostPage()
        {
            //if (App.AppUser != null && App.AppUser.Role!.Any())
            //{
            //    await Shell.Current.GoToAsync(nameof(AddPostPage));
            //}
            List<ChoicePopupArg> items = new List<ChoicePopupArg>() 
            {
                new ChoicePopupArg(route: nameof(AddPostPage), title: "Ajouter un article", image: "iconsajouter.png"),
                new ChoicePopupArg(route: nameof(MyPostPage), title: "Mes articles", image: "ajouterunarticle.png"),

            };
            ChoicePopup popup = new ChoicePopup(items);
            Shell.Current.ShowPopup(popup);

        }
        [RelayCommand]
        public async Task ToAidePage()
        {
            if (App.AppUser != null && App.AppUser.Role!.Any())
            {
                //await Shell.Current.GoToAsync(nameof(AidePage));
                await NavigationHelper.NavigateAsync(nameof(AidePage));

            }

        }
    }
}
