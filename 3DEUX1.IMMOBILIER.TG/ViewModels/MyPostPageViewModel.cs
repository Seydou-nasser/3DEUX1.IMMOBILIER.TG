using _3DEUX1.IMMOBILIER.TG.Models;
using _3DEUX1.IMMOBILIER.TG.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace _3DEUX1.IMMOBILIER.TG.ViewModels
{
    public partial class MyPostPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _currentState;

        [ObservableProperty]
        private bool _isBusy;

        public MvvmHelpers.ObservableRangeCollection<Post> Posts { get; set; } = new();

        private readonly IPostService _postService;
        private readonly IDialogService _dialogService;
        private int _page = 1;

        public MyPostPageViewModel(IPostService postService, IDialogService dialogService)
        {
            _postService = postService;
            _dialogService = dialogService;
            CurrentState = "Loading";
            Posts.Clear();
            LoadData();
            Posts.CollectionChanged += (sender, e) => CurrentState = Posts.Any() ? "Success" : "Empty";
        }

        private async Task LoadData()
        {
            var posts = await _postService.GetPostsByEmail(App.AppUser!.Email, _page);
            if (posts.Count > 0)
            {
                Posts.AddRange(posts);
                CurrentState = "Success";
            }
            else CurrentState = "Empty";
        }

        [RelayCommand]
        private async Task TryLoadData()
        {
            CurrentState = "Loading";
            await LoadData();
        }

        [RelayCommand]
        public async Task ScrollToEnd(object sender)
        {
            var scrollView = sender as ScrollView;
            if (scrollView == null || IsBusy) return;

            double remainingScrollSpace = scrollView.ContentSize.Height - (scrollView.Height + scrollView.ScrollY);
            const double threshold = 50;

            if (remainingScrollSpace <= threshold)
            {
                IsBusy = true;
                _page++;
                await LoadData();
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task ConfirmDeletePost(Post post)
        {
            bool answer = await _dialogService.DisplayAlert("Confirmation", "Voulez-vous vraiment supprimer ce post ?", "Oui", "Non");
            if (answer)
            {
                await DeletePost(post);
            }
        }
        private async Task DeletePost(Post post)
        {
            // Implémentez ici la logique de suppression du post
            // Par exemple :
            await _postService.DeletePostByUser(post.Id ?? 0);
            Posts.Remove(post);
        }
    }
}
