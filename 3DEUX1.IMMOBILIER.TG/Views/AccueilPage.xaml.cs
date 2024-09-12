using _3DEUX1.IMMOBILIER.TG.Services;
using _3DEUX1.IMMOBILIER.TG.ViewModels;

namespace _3DEUX1.IMMOBILIER.TG.Views;

public partial class AccueilPage : ContentPage
{
    private readonly AccueilPageViewModel model;

    public AccueilPage(AccueilPageViewModel model)
	{
		InitializeComponent();
        this.model = model;
        BindingContext = model;
    }
    protected override void OnAppearing()
    {
        //model.LoadData();
        //UserService userService = new UserService();
        ////await userService.UserVerifier();
        //if (App.AppUser != null)
        //{
        //    try
        //    {
        //        //App.AppUser = JsonConvert.DeserializeObject<User>(Preferences.Get(nameof(App.AppUser), null!));
                
        //       //userService.Login(App.AppUser.Email);


        //    }
        //    catch { }
        //}
        
        base.OnAppearing();
    }

}