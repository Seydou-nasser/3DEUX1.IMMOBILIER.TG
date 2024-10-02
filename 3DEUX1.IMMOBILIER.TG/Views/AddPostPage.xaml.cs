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
        // Supprimez cette ligne car la visibilité est maintenant gérée par le trigger
        // AvanceFrame.IsVisible = false;
    }


    private void CategoryCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedCategory = e.CurrentSelection.FirstOrDefault() as string;
        viewModel.SlectedCat(selectedCategory!);
        if (selectedCategory == "Voiture") AvanceFrame.IsVisible = false;
        else AvanceFrame.IsVisible = true;
    }

    private void TypeCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedType = e.CurrentSelection.FirstOrDefault() as string;
        viewModel.SlectedType(selectedType!);
        if (selectedType == "Location" && (string)CategoryCollectionView.SelectedItem != "Voiture") AvanceFrame.IsVisible = true;
        else AvanceFrame.IsVisible = false;
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