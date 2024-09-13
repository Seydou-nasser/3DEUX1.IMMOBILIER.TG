using _3DEUX1.IMMOBILIER.TG.Helpers;
using _3DEUX1.IMMOBILIER.TG.Models;
using _3DEUX1.IMMOBILIER.TG.Services;
using _3DEUX1.IMMOBILIER.TG.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace _3DEUX1.IMMOBILIER.TG.ViewModels
{
    public partial class AccueilPageViewModel : ObservableObject
    {
        [ObservableProperty]
        public string _currentState;
        public MvvmHelpers.ObservableRangeCollection<Post> Posts { get; set; } = new();
        public AccueilPageViewModel()
        {
            CurrentState = "Loading";
            LoadData();
        }

        public async void LoadData()
        {
            CurrentState = "Loading";
            PostService postService = new PostService(new HttpClient());
            var res = await postService.DownloadFourRecentPost();
            if (res != null && res.Count() > 0)
            {
                Posts.Clear();
                Posts.AddRange(res);
                CurrentState = "Success";
                return;
            }
            CurrentState = "Empty";
                
        }

        [RelayCommand]
        public async Task GotoAnnoncePage(Post post)
        {
            await NavigationHelper.NavigateAsync(nameof(PostDetailPage), new Dictionary<string, object>
            {
                { "PostDetails", post }
            });
        }
        [RelayCommand]
        public async Task GotoAllPostPage()
        {
            await Shell.Current.GoToAsync(nameof(AllPostPage));
        }

        [RelayCommand]
        public void LoadDataReset()
        {
            LoadData();
        }
    }
}
