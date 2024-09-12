using _3DEUX1.IMMOBILIER.TG.Services;
using _3DEUX1.IMMOBILIER.TG.ViewModels;

namespace _3DEUX1.IMMOBILIER.TG.Views;

public partial class LoginPage : ContentPage
{
	private readonly LoginPageViewModel viewModel;
	public LoginPage( LoginPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
        this.viewModel = viewModel;
    }


}