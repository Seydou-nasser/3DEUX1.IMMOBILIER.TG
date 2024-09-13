using _3DEUX1.IMMOBILIER.TG.Models;
using _3DEUX1.IMMOBILIER.TG.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Threading.Tasks;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.ApplicationModel.Communication;
using _3DEUX1.IMMOBILIER.TG.Views.PopupPersonaliser;
using CommunityToolkit.Maui.Views;

namespace _3DEUX1.IMMOBILIER.TG.ViewModels
{
    [QueryProperty(nameof(Post),"PostDetails")]
    public partial class PostDetailPageViewModel : ObservableObject
    {
        [ObservableProperty]
        public Post _post;
        //[ObservableProperty]
        //public bool _avanceVisibility;
        //[ObservableProperty]
        //public string _prix;

        public PostDetailPageViewModel()
        {
        }

        [RelayCommand]
        public void ContacterFunc()
        {
            var popup = new PopupContact(Post);
            App.Current!.MainPage!.ShowPopup(popup);
        }

    }
}
