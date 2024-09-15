using _3DEUX1.IMMOBILIER.TG.ViewModels;

namespace _3DEUX1.IMMOBILIER.TG.Views.PopupPersonaliser;

public partial class ChargementPopup : CommunityToolkit.Maui.Views.Popup
{
	private readonly ChargementPopupViewModel? viewModel;
	public ChargementPopup(ChargementPopupViewModel ViewModel)
	{
		InitializeComponent();
        viewModel = ViewModel;
		BindingContext = viewModel;
	}
}