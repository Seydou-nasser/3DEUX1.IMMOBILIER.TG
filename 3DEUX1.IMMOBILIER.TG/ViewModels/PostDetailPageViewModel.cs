using _3DEUX1.IMMOBILIER.TG.Models;
using _3DEUX1.IMMOBILIER.TG.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


namespace _3DEUX1.IMMOBILIER.TG.ViewModels
{
    [QueryProperty(nameof(Post),"PostDetails")]
    public partial class PostDetailPageViewModel : ObservableObject
    {
        [ObservableProperty]
        public Post _post;
        [ObservableProperty]
        public bool _avanceVisibility;
        [ObservableProperty]
        public string _prix;

        public PostDetailPageViewModel()
        {
            AvanceVisibility = false;
            
        }

        [RelayCommand]
        public async Task ContacterFunc()
        {
            await PartageService.SendText(Post.Name!);
        }
    }
}
