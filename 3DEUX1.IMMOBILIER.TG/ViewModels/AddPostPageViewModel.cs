using _3DEUX1.IMMOBILIER.TG.Helpers;
using _3DEUX1.IMMOBILIER.TG.Models;
using _3DEUX1.IMMOBILIER.TG.Services;
using _3DEUX1.IMMOBILIER.TG.Views;
using _3DEUX1.IMMOBILIER.TG.Views.PopupPersonaliser;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace _3DEUX1.IMMOBILIER.TG.ViewModels
{
    public partial class AddPostPageViewModel : ObservableObject
    {
        // Propriétés observables pour la liaison de données
        [ObservableProperty] private List<string>? _caracteristique;
        [ObservableProperty] private Post? _post;
        [ObservableProperty] private string _superficie = string.Empty;
        [ObservableProperty] private string _chambre = string.Empty;
        [ObservableProperty] private string _description = string.Empty;
        [ObservableProperty] private string _prix = string.Empty;
        [ObservableProperty] private string _avance = string.Empty;
        [ObservableProperty] private string _localisation = string.Empty;
        [ObservableProperty] private string _marque = string.Empty;
        [ObservableProperty] private string _modele = string.Empty;
        [ObservableProperty] private string _annee = string.Empty;
        [ObservableProperty] private string _capacite = string.Empty;
        [ObservableProperty] private string _kilometrage = string.Empty;

        // Collections observables pour les images et les localisations
        public MvvmHelpers.ObservableRangeCollection<string> Images { get; set; } = new();
        public MvvmHelpers.ObservableRangeCollection<string> LocalisationsList { get; set; } = new();
        private readonly IPostService _postService;
        // Constructeur
        public AddPostPageViewModel(IPostService postService)
        {
            LocalisationsList.AddRange(ApplicationData.GetLomeQuart());
            init();
            _postService = postService;
        }

        // Commande pour envoyer le post
        [RelayCommand]
        public async Task Envoyer()
        {
            await AffectationDeValeurAsync();
            if (await ValidationBeforUpload()) await UploadPost();
        }

        // Commande pour prévisualiser le post
        [RelayCommand]
        public async Task Previsualisation()
        {
            await AffectationDeValeurAsync();

            if (Post == null)
            {
                await CreatSnackBar.SnackBarShow("Erreur : l'objet Post n'est pas initialisé !");
                return;
            }

            await ValidationBeforUpload();

            await NavigateToPostDetailPage();
        }

        // Méthode pour naviguer vers la page de détails du post
        private async Task NavigateToPostDetailPage()
        {
            await NavigationHelper.NavigateAsync(nameof(PostDetailPage), new Dictionary<string, object>
            {
                { "PostDetails", Post! }
            });
        }

        // Méthode pour affecter les valeurs au post
        private async Task AffectationDeValeurAsync()
        {
            Caracteristique?.Clear();
            Post!.DateTime = DateTime.Now;
            Post.Caracteristique = Caracteristique;
            Post.Description = Description;
            Post.Localisation = Localisation;

            switch (Post.Categories)
            {
                case "Maison":
                case "Appartement":
                    Caracteristique?.Add($"{Superficie} superficie");
                    Caracteristique?.Add($"{Chambre} chambre");
                    break;
                case "Voiture":
                    Caracteristique?.Add($"Marque: {Marque}");
                    Caracteristique?.Add($"Modèle: {Modele}");
                    Caracteristique?.Add($"Année: {Annee}");
                    Caracteristique?.Add($"Kilométrage: {Kilometrage}");
                    break;
                case "Salle de Reunion":
                    Caracteristique?.Add($"Capacité: {Capacite} personnes");
                    Caracteristique?.Add($"{Superficie} m²");
                    break;
            }

            // Gestion de l'avance pour une location
            if (!int.TryParse(Avance, out int avanceValue) && Post.Type == "Location")
            {
                Post.Avance = null;
                await CreatSnackBar.SnackBarShow("Veuillez renseigner uniquement des chiffres pour l'avance !");
            }
            else
            {
                Post.Avance = avanceValue;
            }

            // Gestion du prix
            if (!int.TryParse(Prix, out int prixValue))
            {
                Post.Prix = null;
                await CreatSnackBar.SnackBarShow("Veuillez renseigner uniquement des chiffres pour le prix !");
            }
            else
            {
                Post.Prix = prixValue;
            }
        }

        // Méthode d'initialisation
        private void init()
        {
            Caracteristique = new();
            Chambre = string.Empty;
            Superficie = string.Empty;
            Post = new Post();
            Post.Caracteristique = new();
            Post.Images = new List<string>();
            Prix = null!;
            Images.Clear();
            Avance = string.Empty;
        }

        // Commande pour sélectionner des images
        [RelayCommand]
        public async Task SelectImage()
        {
            try
            {
                Images.Clear();
                Post!.Images!.Clear();

                var response = await FilePicker.PickMultipleAsync(new PickOptions
                {
                    PickerTitle = "Sélectionner les images",
                    FileTypes = FilePickerFileType.Images,
                });

                if (response == null || !response.Any()) return;
                if (response.Count() > 3)
                {
                    await CreatSnackBar.SnackBarShow($"Vous ne pouver selectionner plus de 3 photos!");
                    return;
                }

                foreach (var img in response)
                {
                    Images.Add(img.FullPath);
                    using var openStream = await img.OpenReadAsync();
                    using var mstream = new MemoryStream();
                    await openStream.CopyToAsync(mstream);
                    var convertImage = Convert.ToBase64String(mstream.ToArray());
                    Post.Images.Add(convertImage);
                }
            }
            catch (Exception ex)
            {
                await CreatSnackBar.SnackBarShow($"Erreur lors de la sélection des images : {ex.Message}");
            }
        }

        // Méthode pour sélectionner une catégorie
        public void SlectedCat(string val) => Post!.Categories = val;

        // Méthode pour sélectionner un type
        public void SlectedType(string val) => Post!.Type = val;

        // Méthode pour valider le post
        private bool ValidatePost()
        {
            bool isValid = Post!.Categories != null &&
                           Post.Type != null &&
                           Post.Images!.Any() &&
                           !string.IsNullOrEmpty(Localisation) &&
                           !string.IsNullOrEmpty(Description);

            switch (Post.Categories)
            {
                case "Maison":
                case "Appartement":
                    isValid &= !string.IsNullOrEmpty(Chambre) && !string.IsNullOrEmpty(Superficie);
                    break;
                case "Voiture":
                    isValid &= !string.IsNullOrEmpty(Marque) && !string.IsNullOrEmpty(Modele) && !string.IsNullOrEmpty(Annee) && !string.IsNullOrEmpty(Kilometrage);
                    break;
                case "Salle de Reunion":
                    isValid &= !string.IsNullOrEmpty(Capacite) && !string.IsNullOrEmpty(Superficie);
                    break;
            }

            return isValid;
        }
        private async Task<bool> ValidationBeforUpload()
        {
            if (!ValidatePost())
            {
                await CreatSnackBar.SnackBarShow("Veuillez renseigner toutes les informations !");
                return false;
            }
            if (Post!.Type == "Location" && (Post.Avance == null || Post.Avance == 0) && Post.Categories != "Voiture")
            {
                await CreatSnackBar.SnackBarShow("Veuillez mettre l'avance pour une location !");
                return false;
            }
            if (string.IsNullOrEmpty(Post.Type) || string.IsNullOrEmpty(Post.Categories))
            {
                await CreatSnackBar.SnackBarShow("Veuillez sélectionner le type et la catégorie !");
                return false;
            }
            return true;
        }

        // Méthode pour uploader le post
        private async Task UploadPost()
        {
            var popup = new ChargementPopup();
            App.Current!.MainPage!.ShowPopup(popup);
            bool success = await _postService.UploadPost(Post!);
            popup.Close();
            if (success) init();
        }
    }
}
