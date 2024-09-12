using _3DEUX1.IMMOBILIER.TG.ViewModels;

namespace _3DEUX1.IMMOBILIER.TG.Views;

public partial class PostDetailPage : ContentPage
{
    private readonly PostDetailPageViewModel viewModel;

    public PostDetailPage(PostDetailPageViewModel viewModel)
	{
        
        InitializeComponent();
        this.viewModel = viewModel;
        BindingContext = viewModel;
        
        
        
    }

    //private void Picker_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    viewModel.PrixValue(Picker.SelectedIndex);
    //}
    protected override void OnAppearing()
    {
        //Picker.SelectedIndex = 0;
        base.OnAppearing();
    }

    //protected override bool OnBackButtonPressed()
    //{
    //    Shell.Current.GoToAsync(nameof(AccueilPage));
    //    return base.OnBackButtonPressed();
    //}
}