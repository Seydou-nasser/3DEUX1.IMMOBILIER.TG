using _3DEUX1.IMMOBILIER.TG.Helpers;
using _3DEUX1.IMMOBILIER.TG.Models;
using _3DEUX1.IMMOBILIER.TG.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DEUX1.IMMOBILIER.TG.ViewModels
{
    public partial class ManagerPopupViewModel : ObservableObject
    {
        private Post post;
        [ObservableProperty]
        double _screenWidth;
        [ObservableProperty]
        double _screenHeight;
        public ManagerPopupViewModel(Post post)
        {
            this.post = post;
            var displayInfo = DeviceDisplay.Current.MainDisplayInfo; // Récupère les infos de l'ecran
            ScreenHeight = displayInfo.Height;
            ScreenWidth = displayInfo.Width;
        }

        [RelayCommand]
        public async Task Previsualisation()
        {
            await NavigationHelper.NavigateAsync(nameof(PostDetailPage), new Dictionary<string, object>
            {
                { "PostDetails", post! }
            });
        }
        [RelayCommand]
        public void Envoyer()
        {
            throw new NotImplementedException();
        }

    }
}
