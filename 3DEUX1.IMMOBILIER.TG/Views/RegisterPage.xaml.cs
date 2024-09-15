using _3DEUX1.IMMOBILIER.TG.Services;
using _3DEUX1.IMMOBILIER.TG.ViewModels;

namespace _3DEUX1.IMMOBILIER.TG.Views;

public partial class RegisterPage : ContentPage
{
	readonly RegisterPageViewModel viewModel;
	public RegisterPage(RegisterPageViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
        this.viewModel = viewModel;
	}
    protected override void OnAppearing()
    {
        UserService userService = new UserService(new HttpClient());
        userService.UserVerifier();
        base.OnAppearing();
    }

}