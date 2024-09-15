﻿using CommunityToolkit.Mvvm.ComponentModel;

namespace _3DEUX1.IMMOBILIER.TG.ViewModels
{
    public partial class ChargementPopupViewModel : ObservableObject
    {
        [ObservableProperty]
        double _screenWidth;
        [ObservableProperty]
        double _screenHeight;
        public ChargementPopupViewModel()
        {
            var displayInfo = DeviceDisplay.Current.MainDisplayInfo; // Récupère les infos de l'ecran
            ScreenHeight = displayInfo.Height;
            ScreenWidth = displayInfo.Width;
        }

    }

        
}
