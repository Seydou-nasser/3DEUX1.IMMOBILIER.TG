using _3DEUX1.IMMOBILIER.TG.ViewModels;

namespace _3DEUX1.IMMOBILIER.TG.Views;

public partial class PopupPersonalise : CommunityToolkit.Maui.Views.Popup
{
	private readonly PopupPersonaliseViewModel? loginPageViewModel;
	public PopupPersonalise(PopupPersonaliseViewModel ViewModel)
	{
		InitializeComponent();
        loginPageViewModel = ViewModel;
		BindingContext = loginPageViewModel;
	}
}