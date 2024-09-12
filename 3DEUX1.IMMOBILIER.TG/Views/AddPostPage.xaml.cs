using _3DEUX1.IMMOBILIER.TG.ViewModels;

namespace _3DEUX1.IMMOBILIER.TG.Views;

public partial class AddPostPage : ContentPage
{
    private readonly AddPostPageViewModel viewModel;

    public AddPostPage(AddPostPageViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
        this.viewModel = viewModel;
        AvanceFrame.IsVisible = false;
    }


    private void CategoryCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedCategory = e.CurrentSelection.FirstOrDefault() as string;
        // Traitez la sélection ici
        viewModel.SlectedCat(selectedCategory!);
    }

    private void TypeCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedCategory = e.CurrentSelection.FirstOrDefault() as string;
        // Traitez la sélection ici
        if (selectedCategory == "Location") AvanceFrame.IsVisible = true;
        else AvanceFrame.IsVisible = false;
        viewModel.SlectedType(selectedCategory!);
    }

    private void Picker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;
        if (selectedIndex != -1)
        {
            string choix = (string)picker.ItemsSource[selectedIndex]!;
            viewModel.Localisation = choix;
        }

    }
}