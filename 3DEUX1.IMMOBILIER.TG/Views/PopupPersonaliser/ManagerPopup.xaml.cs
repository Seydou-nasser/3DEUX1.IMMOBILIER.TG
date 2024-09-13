using _3DEUX1.IMMOBILIER.TG.Helpers;
using _3DEUX1.IMMOBILIER.TG.Models;
using _3DEUX1.IMMOBILIER.TG.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace _3DEUX1.IMMOBILIER.TG.Views.PopupPersonaliser;

public partial class ManagerPopup : CommunityToolkit.Maui.Views.Popup
{
    //private readonly ManagerPopupViewModel viewModel;
    private Post post { get; set; }

    private AdminZoneService adminZoneService;

    public ManagerPopup(Post post)
    {
        InitializeComponent();
        //this.viewModel = viewModel;
        BindingContext = this;
        this.post = post;
    }
    [RelayCommand]
    public async Task Previsualisation()
    {
        await NavigationHelper.NavigateAsync(nameof(PostDetailPage), new Dictionary<string, object>
        {
            { "PostDetails", post! }
        });
        this.Close();
    }
    [RelayCommand]
    public async Task Envoyer()
    {
        adminZoneService = new();
        UpdatePostModel model = new()
        {
            Id = post.Id,
            Isavailable = true,
            IsDelete = post.IsDelete,
            AdminZone = App.AppUser!.Email,
        };
        var res = await adminZoneService.AutorisePost(model);
        if (res == true) this.Close(res);
    }
}