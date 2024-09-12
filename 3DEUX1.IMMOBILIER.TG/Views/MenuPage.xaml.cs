using _3DEUX1.IMMOBILIER.TG.ViewModels;

namespace _3DEUX1.IMMOBILIER.TG.Views;

public partial class MenuPage : ContentPage
{
    private readonly MenuPageViewModel viewModel;

    public MenuPage(MenuPageViewModel viewModel)
	{
		InitializeComponent();
        this.viewModel = viewModel;
        BindingContext = viewModel;
    }
}