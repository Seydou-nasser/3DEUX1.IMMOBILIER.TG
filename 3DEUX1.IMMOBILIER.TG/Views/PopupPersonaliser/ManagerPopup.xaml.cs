using _3DEUX1.IMMOBILIER.TG.ViewModels;

namespace _3DEUX1.IMMOBILIER.TG.Views.PopupPersonaliser;

public partial class ManagerPopup : CommunityToolkit.Maui.Views.Popup
{
    private readonly ManagerPopupViewModel viewModel;

    public ManagerPopup(ManagerPopupViewModel viewModel)
	{
		InitializeComponent();
        this.viewModel = viewModel;
        BindingContext = viewModel;
    }
}