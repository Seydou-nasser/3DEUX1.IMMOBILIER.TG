
using CommunityToolkit.Mvvm.ComponentModel;

namespace _3DEUX1.IMMOBILIER.TG.Views.PopupPersonaliser;

public partial class ChargementPopup : CommunityToolkit.Maui.Views.Popup
{
    private double _screenHeight;
    private double _screenWidth;
    public double ScreenHeight 
    { 
        get{ return _screenHeight; } 
        set { _screenHeight = value; OnPropertyChanged(nameof(ScreenHeight)); } 
    }

    public double ScreenWidth
    {
        get { return _screenWidth; }
        set { _screenWidth = value; OnPropertyChanged(nameof(ScreenWidth)); }
    }

    public ChargementPopup()
    {
        InitializeComponent();
        //viewModel = ViewModel;
        BindingContext = this;
        var displayInfo = DeviceDisplay.Current.MainDisplayInfo; // Récupère les infos de l'ecran
        ScreenHeight = displayInfo.Height;
        ScreenWidth = displayInfo.Width;
    }
}