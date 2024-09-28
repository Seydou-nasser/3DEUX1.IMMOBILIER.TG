using _3DEUX1.IMMOBILIER.TG.ViewModels;

namespace _3DEUX1.IMMOBILIER.TG.Views;

public partial class MyPostPage : ContentPage
{
    private readonly MyPostPageViewModel viewModel;

    public MyPostPage(MyPostPageViewModel viewModel)
    {
        InitializeComponent();
        this.viewModel = viewModel;
        BindingContext = viewModel;
    }
}