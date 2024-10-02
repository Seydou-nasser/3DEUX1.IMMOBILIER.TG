using _3DEUX1.IMMOBILIER.TG.Helpers;
using _3DEUX1.IMMOBILIER.TG.Models;
using _3DEUX1.IMMOBILIER.TG.Services;
using _3DEUX1.IMMOBILIER.TG.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


namespace _3DEUX1.IMMOBILIER.TG.ViewModels
{
    public partial class AllPostPageViewModel : ObservableObject
    {
        public enum EnumCategories
        {
            Maison = 0,
            Appartement = 1,
            Voiture = 2,
            SalleDeReuinion = 3
        }

        private PostService postService;
        [ObservableProperty]
        public string _currentState;

        [ObservableProperty]
        public Color _backColorMaison;
        [ObservableProperty]
        public Color _backColorAppartement;
        [ObservableProperty]
        public Color _backColorVoiture;
        [ObservableProperty]
        public Color _backColorSalleDeReunion;

        [ObservableProperty]
        public bool _isBusy = false;

        private Color SelectedColor = Colors.LightGray;
        private Color UnSelectedColo = Colors.Transparent;

        public int _CatSelect;
        public string _CategorySelect;
        public int CatSelect
        {
            get { return _CatSelect; }
            set
            {
                _CatSelect = value;
                if (_CatSelect == ((int)EnumCategories.Maison))
                {
                    BackColorMaison = SelectedColor;
                    BackColorAppartement = UnSelectedColo;
                    BackColorSalleDeReunion = UnSelectedColo;
                    BackColorVoiture = UnSelectedColo;
                    _CategorySelect = "Maison";
                    LoadDataByCategorie("Maison");
                }
                else
                if (_CatSelect == ((int)EnumCategories.Appartement))
                {
                    BackColorMaison = UnSelectedColo;
                    BackColorAppartement = SelectedColor;
                    BackColorSalleDeReunion = UnSelectedColo;
                    BackColorVoiture = UnSelectedColo;
                    _CategorySelect = "Appartement";
                    LoadDataByCategorie("Appartement");
                }
                else
                if (_CatSelect == ((int)EnumCategories.SalleDeReuinion))
                {
                    BackColorMaison = UnSelectedColo;
                    BackColorAppartement = UnSelectedColo;
                    BackColorSalleDeReunion = SelectedColor;
                    BackColorVoiture = UnSelectedColo;
                    _CategorySelect = "Salle de reuinion";
                    LoadDataByCategorie("Salle de reuinion");
                }
                else
                if (_CatSelect == ((int)EnumCategories.Voiture))
                {
                    BackColorMaison = Colors.Transparent;
                    BackColorAppartement = Colors.Transparent;
                    BackColorSalleDeReunion = Colors.Transparent;
                    BackColorVoiture = SelectedColor;
                    _CategorySelect = "Voiture";
                    LoadDataByCategorie("Voiture");
                }

                OnPropertyChanged(nameof(CatSelect));
            }
        }
        private List<Post> _postsMaison = new();
        private List<Post> _postsAppartement = new();
        private List<Post> _postsVoiture = new();
        private List<Post> _postsSalleDeReunion = new();
        public MvvmHelpers.ObservableRangeCollection<Post> Posts { get; set; } = new();
        //[ObservableProperty]
        //public List<Post> _posts;
        private readonly IPostService _postService;

        private int pageIndex = 1;
        public AllPostPageViewModel(IPostService postService)
        {
            _postService = postService;
            CatSelect = (int)(EnumCategories.Maison);
            CurrentState = "Loading";
            //LoadData();
        }
        private async Task LoadData()
        {
            CurrentState = "Loading";
            var res = await _postService.GetAllPost(1);
            if (res is not null)
            {
                if (res.Count > 0)
                {
                    Posts.AddRange(res);
                    CurrentState = "Success";
                    return;
                }
                CurrentState = "Empty";
            }

            //Si null 

        }

        [RelayCommand]
        public async Task GotoAnnoncePage(Post post)
        {
            //Post? post = Posts.Where(x => x.Id == id).FirstOrDefault();
            await NavigationHelper.NavigateAsync(nameof(PostDetailPage), new Dictionary<string, object>
            {
                { "PostDetails", post! }
            });
        }

        [RelayCommand]
        public void CategorieSelectionerFunc(string SCat)
        {
            int cat = int.Parse(SCat);
            if (cat != CatSelect)
            {
                IsBusy = false;
                pageIndex = 1;
                Posts.Clear();
                CurrentState = "Loading";
                if (cat == (int)EnumCategories.Voiture) { CatSelect = (int)EnumCategories.Voiture; }
                else if (cat == (int)EnumCategories.Appartement) { CatSelect = (int)EnumCategories.Appartement; }
                else if (cat == (int)EnumCategories.SalleDeReuinion) { CatSelect = (int)EnumCategories.SalleDeReuinion; }
                else if (cat == (int)EnumCategories.Maison) { CatSelect = (int)EnumCategories.Maison; }
            }
        }
        private async Task LoadDataByCategorie(string Catg)
        {
            //CurrentState = "Loading";
            pageIndex = 1;
            var res = await _postService.GetPostByCategories(pageIndex, type: "Location", category: Catg);
            if (res is not null)
            {
                if (res.Count > 0)
                {
                    if (res[0].Categories == _CategorySelect)
                    {
                        Posts.Clear();
                        Posts.AddRange(res);
                        pageIndex = 2;
                        CurrentState = "Success";
                    }
                    return;
                }
                CurrentState = "Empty";
            }
        }
        public async Task LoadDataByScroll()
        {
            string catg = "";

            if (IsBusy == false)
            {
                switch (_CatSelect)
                {
                    case (int)EnumCategories.Voiture:
                        catg = "Voiture";
                        break;
                    case (int)EnumCategories.Appartement:
                        catg = "Appartement";
                        break;
                    case (int)EnumCategories.SalleDeReuinion:
                        catg = "Salle de reuinion";
                        break;
                    case (int)EnumCategories.Maison:
                        catg = "Maison";
                        break;
                }
                IsBusy = true;
                var res = await _postService.GetPostByCategories(pageIndex, type: "Locations", category: catg);
                if (res is not null)
                {
                    if (res.Count > 0)
                    {
                        Posts.AddRange(res);
                        IsBusy = false;
                        pageIndex++;
                        return;
                    }
                }
                //await Task.Delay(3000);
                IsBusy = false;
            }

        }

    }
}
