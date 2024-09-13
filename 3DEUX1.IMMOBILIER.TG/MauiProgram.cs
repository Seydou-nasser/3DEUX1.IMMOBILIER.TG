using _3DEUX1.IMMOBILIER.TG.Services;
using _3DEUX1.IMMOBILIER.TG.ViewModels;
using _3DEUX1.IMMOBILIER.TG.Views;
using _3DEUX1.IMMOBILIER.TG.Views.PopupPersonaliser;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Core;
using Microsoft.Extensions.Logging;
namespace _3DEUX1.IMMOBILIER.TG
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseMauiCommunityToolkitCore()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<LoginPageViewModel>();

            builder.Services.AddSingleton<RegisterPageViewModel>();
            builder.Services.AddSingleton<RegisterPage>();

            builder.Services.AddTransientPopup<PopupPersonalise, PopupPersonaliseViewModel>();
            //builder.Services.AddSingleton<PopupPersonaliseViewModel>();

            builder.Services.AddSingleton<AccueilPage>();
            builder.Services.AddSingleton<AccueilPageViewModel>();

            builder.Services.AddSingleton<ChatPage>();
            builder.Services.AddSingleton<ChatPageViewModel>();

            builder.Services.AddTransient<PostDetailPage>();
            builder.Services.AddTransient<PostDetailPageViewModel>();

            builder.Services.AddSingleton<ParametrePage>();
            builder.Services.AddSingleton<ParametrePageViewModel>();

            builder.Services.AddSingleton<AddPostPage>();
            builder.Services.AddSingleton<AddPostPageViewModel>();

            builder.Services.AddSingleton<MenuPage>();
            builder.Services.AddSingleton<MenuPageViewModel>();

            builder.Services.AddTransient<AllPostPage>();
            builder.Services.AddTransient<AllPostPageViewModel>();

            builder.Services.AddSingleton<ManagementPage>();
            builder.Services.AddSingleton<ManagementPageViewModel>();

            builder.Services.AddSingleton<IUserService, UserService>();
            builder.Services.AddSingleton<PartageService>();
            builder.Services.AddSingleton<AdminZoneService>();
            builder.Services.AddSingleton<IPostService, PostService>();

            builder.Services.AddTransient<ManagerPopup>();

#if DEBUG
            builder.Logging.AddDebug();
#endif
            return builder.Build();
        }
    }
}
