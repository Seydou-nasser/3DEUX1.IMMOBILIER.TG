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
    public partial class ManagementPageViewModel : ObservableObject
    {
        private readonly AdminZoneService adminZoneService;
        public MvvmHelpers.ObservableRangeCollection<Post> Posts { get; set; } = new();
        [ObservableProperty]
        private string _currentState = "Loading";

        [ObservableProperty]
        private bool _isBusy;

        private int pageIndex = 1;
        AdminZoneService service;
        public ManagementPageViewModel(AdminZoneService adminZoneService)
        {
            IsBusy = false;
            this.adminZoneService = adminZoneService;
            CurrentState = "Loading";
            service = new();
            LoadDataAsync();
            Posts.CollectionChanged += Posts_CollectionChanged;
        }

        private void Posts_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (Posts.Count() == 0) CurrentState = "Empty";
        }

        private async Task LoadDataAsync()
        {
            
            var res = await service.AdminZoneGetPostIsNoAvailable(1);
            
            if (res!.Count() <= 1) CurrentState = "Empty";
            else if (res is not null)
            {
                Posts.AddRange(res);
                CurrentState = "Success";
            }
            else
                CurrentState = "Empty";
        }
        [RelayCommand]
        private async Task LoadDataFromBtn()
        {
            //CurrentState = "Loading";
            //var res = await service.AdminZoneGetPostIsNoAvailable(1);
            //if (res!.Count() <= 1) CurrentState = "Empty";
            //else if (res is not null)
            //{
            //    Posts.AddRange(res);
            //    CurrentState = "Success";
            //}
            //else CurrentState = "Empty";
            await LoadDataAsync();
        }

        [RelayCommand]
        public async Task LoadDataByScroll()
        {
            if (!IsBusy)
            {
                IsBusy = true;
                var res = await service.AdminZoneGetPostIsNoAvailable(1);
                if (res is not null)
                {
                    Posts.AddRange(res);
                    IsBusy = false;
                    pageIndex++;
                    return;
                }
                //await Task.Delay(3000);
                IsBusy = false;
            }

        }

        [RelayCommand]
        public void ManagePopupCall(Post post)
        {
            var popup = new ManagerPopup(new ManagerPopupViewModel(post));
            App.Current!.MainPage!.ShowPopup(popup);
            //try { popup.Close(); }
            //catch { }
        }

        [RelayCommand]
        public async Task AutorizePostFunc(Post post)
        {
            UpdatePostModel model = new()
            {
                Id = post.Id,
                Isavailable = true,
                IsDelete = post.IsDelete,
                AdminZone = App.AppUser!.Email,
            };
            var res = await adminZoneService.AutorisePost(model);
            if (res == true) Posts.Remove(post);
        }
        [RelayCommand]
        public void AfficheMenuFunc(Border obj)
        {
            if (obj!.IsVisible) obj!.IsVisible = false;
            else obj!.IsVisible = true;
        }

        [RelayCommand]
        public async Task Previsualisation(Post post)
        {
            await NavigationHelper.NavigateAsync(nameof(PostDetailPage), new Dictionary<string, object>
            {
                { "PostDetails", post! }
            });
        }

    }
}
