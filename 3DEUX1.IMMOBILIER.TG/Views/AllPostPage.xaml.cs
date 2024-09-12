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

        // On r�cup�re la hauteur totale du contenu
        double scrollingSpace = scrollView!.ContentSize.Height - scrollView.Height;

        // Si la position actuelle du scroll (scrollY) est proche ou �gale � l'espace de d�filement disponible
        if (scrollingSpace <= e.ScrollY + 10)
        {
            // L'utilisateur a atteint la fin du ScrollView
            Console.WriteLine("Arriv� � la fin du ScrollView");
            viewModel.LoadDataByScroll();
            // Place ton code ici pour d�clencher une action lorsque l'utilisateur arrive � la fin
        }

    }
}