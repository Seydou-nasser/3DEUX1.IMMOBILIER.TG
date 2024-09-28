using _3DEUX1.IMMOBILIER.TG.Models;
using _3DEUX1.IMMOBILIER.TG.Services;
using _3DEUX1.IMMOBILIER.TG.Views;
using Newtonsoft.Json;

namespace _3DEUX1.IMMOBILIER.TG
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            RegisterRoutes();
            tabbar.Items.Clear();
            ManageTabBar();
        }

        // Enregistre les routes pour la navigation
        private void RegisterRoutes()
        {
            Routing.RegisterRoute(Routes.Login, typeof(LoginPage));
            Routing.RegisterRoute(Routes.Register, typeof(RegisterPage));
            Routing.RegisterRoute(Routes.Accueil, typeof(AccueilPage));
            Routing.RegisterRoute(Routes.PostDetail, typeof(PostDetailPage));
            Routing.RegisterRoute(Routes.Parametre, typeof(ParametrePage));
            Routing.RegisterRoute(Routes.AddPost, typeof(AddPostPage));
            Routing.RegisterRoute(Routes.AllPost, typeof(AllPostPage));
            Routing.RegisterRoute(Routes.Menu, typeof(MenuPage));
            Routing.RegisterRoute(Routes.Management, typeof(ManagementPage));
            Routing.RegisterRoute(Routes.Aide, typeof(AidePage));
            Routing.RegisterRoute(Routes.MyPost, typeof(MyPostPage));
        }

        // Vérifie l'utilisateur et rafraîchit ses informations
        private async Task VerifyUser()
        {
            UserService service = new UserService(new HttpClient());
            if (App.AppUser != null)
            {
                var res = await service.RefrechLogin(App.AppUser!.Email);
                Preferences.Default.Set(nameof(App.AppUser), JsonConvert.SerializeObject(res));
                App.AppUser = res;
                if (res == null) App.Current!.MainPage = new LoginPage(new ViewModels.LoginPageViewModel());
            }
        }

        // Définit la structure d'un élément de la barre d'onglets
        record TabBarItem(string Icon, Type Type, string Title = "");

        // Retourne les éléments de la barre d'onglets pour tous les utilisateurs
        private TabBarItem[] GetAllUserTabBar() => [
                new TabBarItem(Icon: "accueil.png", typeof(AccueilPage), Title: "Accueil"),
                new TabBarItem(Icon: "menu.png", typeof(MenuPage), Title: "Menu"),
            ];

        // Retourne les éléments de la barre d'onglets pour les utilisateurs connectés
        private TabBarItem[] GetUserConnectedTabBar() => [
                new TabBarItem(Icon: "accueil.png", typeof(AccueilPage), Title: "Accueil"),
                //new TabBarItem(Icon: "iconsajouter.png", typeof(AddPostPage), Title: "Ajout"),
                new TabBarItem(Icon: "menu.png", typeof(MenuPage), Title: "Menu"),
            ];

        // Retourne les éléments de la barre d'onglets pour les administrateurs
        private TabBarItem[] GetAdminTabBar() => [
                new TabBarItem(Icon: "accueil.png", typeof(AccueilPage), Title: "Accueil"),
                //new TabBarItem(Icon: "iconsajouter.png", typeof(AddPostPage), Title: "Ajout"),
                new TabBarItem(Icon: "usergroups.png", typeof(ManagementPage), Title: "Management"),
                new TabBarItem(Icon: "menu.png", typeof(MenuPage), Title: "Menu"),
            ];

        // Gère la création et l'affichage de la barre d'onglets
        private void ManageTabBar()
        {
            TabBarItem[] items = GetTabBarItems();

            foreach (var item in items)
            {
                tabbar.Items.Add(new ShellContent
                {
                    Icon = item.Icon,
                    Title = item.Title,
                    ContentTemplate = new DataTemplate(item.Type)
                });
            }
        }

        // Détermine quels éléments de la barre d'onglets afficher en fonction du rôle de l'utilisateur
        private TabBarItem[] GetTabBarItems()
        {
            if (App.AppUser?.Role == null) return GetAllUserTabBar();

            return App.AppUser.Role.Contains("Admin de Zone") ? GetAdminTabBar() :
                   App.AppUser.Role.Contains("client") ? GetUserConnectedTabBar() :
                   GetAllUserTabBar();
        }

        // Méthode appelée lorsque la page apparaît
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet) return;

            if (!Preferences.Default.ContainsKey(nameof(App.AppUser))) return;

            var userJson = Preferences.Get(nameof(App.AppUser), null!);
            App.AppUser = JsonConvert.DeserializeObject<User>(userJson);

            if (App.AppUser == null) return;

            var service = new UserService(new HttpClient());
            var user = await service.RefrechLogin(App.AppUser.Email);

            if (user != null)
            {
                App.AppUser = user;
                Preferences.Default.Set(nameof(App.AppUser), JsonConvert.SerializeObject(user));
            }
        }
    }
}

// Définition des routes de l'application
public static class Routes
{
    public const string Login = nameof(LoginPage);
    public const string Register = nameof(RegisterPage);
    public const string Accueil = nameof(AccueilPage);
    public const string PostDetail = nameof(PostDetailPage);
    public const string Parametre = nameof(ParametrePage);
    public const string AddPost = nameof(AddPostPage);
    public const string AllPost = nameof(AllPostPage);
    public const string Menu = nameof(MenuPage);
    public const string Management = nameof(ManagementPage);
    public const string Aide = nameof(AidePage);
    public const string MyPost = nameof(MyPostPage);
}
