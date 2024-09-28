using _3DEUX1.IMMOBILIER.TG.ViewModels;

namespace _3DEUX1.IMMOBILIER.TG.Views;

public partial class AllPostPage : ContentPage
{
    private readonly AllPostPageViewModel viewModel;

    public AllPostPage(AllPostPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        this.viewModel = viewModel;
    }

    private void ScrollView_Scrolled(object sender, ScrolledEventArgs e)
    {
        var scrollView = sender as ScrollView;
        if (scrollView == null) return;

        // Calcul de l'espace de défilement restant
        double remainingScrollSpace = scrollView.ContentSize.Height - (scrollView.Height + e.ScrollY);

        // Seuil de déclenchement (en pixels)
        const double threshold = 50;

        // Vérification si l'utilisateur est proche de la fin du ScrollView
        if (remainingScrollSpace <= threshold)
        {
            // L'utilisateur est proche de la fin du ScrollView
            Console.WriteLine("Proche de la fin du ScrollView");
            viewModel.LoadDataByScroll();
            // Vous pouvez ajouter d'autres actions ici si nécessaire
        }
    }
}