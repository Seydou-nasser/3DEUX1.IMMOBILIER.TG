using _3DEUX1.IMMOBILIER.TG.Services;
using _3DEUX1.IMMOBILIER.TG.ViewModels;
using _3DEUX1.IMMOBILIER.TG.Views;
using _3DEUX1.IMMOBILIER.TG.Views.PopupPersonaliser;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Core;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System;

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

            builder.Services.AddTransient<ChargementPopup>();
            builder.Services.AddTransient<AidePage>();
            //builder.Services.AddSingleton<PopupPersonaliseViewModel>();

            builder.Services.AddSingleton<AccueilPage, AccueilPageViewModel>();

            //builder.Services.AddSingleton<ChatPage,ChatPageViewModel>();

            builder.Services.AddTransient<PostDetailPage, PostDetailPageViewModel>();
            //builder.Services.AddTransient<PostDetailPageViewModel>();

            builder.Services.AddSingleton<ParametrePage, ParametrePageViewModel>();
            //builder.Services.AddSingleton<ParametrePageViewModel>();

            builder.Services.AddTransient<AddPostPage, AddPostPageViewModel>();
            //builder.Services.AddSingleton<AddPostPageViewModel>();

            builder.Services.AddSingleton<MenuPage, MenuPageViewModel>();
            //builder.Services.AddSingleton<MenuPageViewModel>();

            builder.Services.AddTransient<AllPostPage, AllPostPageViewModel>();
            //builder.Services.AddTransient<AllPostPageViewModel>();

            builder.Services.AddSingleton<ManagementPage, ManagementPageViewModel>();
            //builder.Services.AddSingleton<ManagementPageViewModel>();

            builder.Services.AddTransient<MyPostPage, MyPostPageViewModel>();

            builder.Services.AddSingleton<IUserService, UserService>();
            builder.Services.AddSingleton<IDialogService, DialogService>();
            builder.Services.AddSingleton<PartageService>();
            builder.Services.AddSingleton<AdminZoneService>();
            builder.Services.AddSingleton<IPostService, PostService>();

            builder.Services.AddTransient<ManagerPopup>();

            builder.Services.AddTransient<HttpClient>();

            //builder.Services.AddTransient<JwtAuthenticationHandler>();
            //builder.Services.AddHttpClient("API", client => client.BaseAddress = new Uri(ApiData.GetApiBaseAddress()))
            //    .AddHttpMessageHandler<JwtAuthenticationHandler>();

            //builder.Services.AddSingleton(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("API"));

#if DEBUG
            builder.Logging.AddDebug();
#endif
            return builder.Build();
        }
    }
}
