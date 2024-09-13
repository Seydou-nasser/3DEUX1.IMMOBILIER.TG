using _3DEUX1.IMMOBILIER.TG.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace _3DEUX1.IMMOBILIER.TG.Services
{
    public class UserService : IUserService
    {
        private readonly string _apiAddress;
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _apiAddress = ApiData.GetApiBaseAddress() + "Users/";
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(_apiAddress);
        }

        public async Task<User?> Login(string email, string password)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{email}/{password}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<User>();
                }
                
                await Application.Current!.MainPage!.DisplayAlert(email, await response.Content.ReadAsStringAsync(), "OK");
                return null;
            }
            catch (Exception ex)
            {
                // Loguer l'exception
                Console.WriteLine($"Erreur lors de la connexion : {ex.Message}");
                return null;
            }
        }

        public async Task<User?> RefrechLogin(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email)) return null;
                var response = await _httpClient.GetAsync($"{email}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<User>();
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors du rafraîchissement de la connexion : {ex.Message}");
                return null;
            }
        }

        public void Logout()
        {
            Preferences.Default.Clear();
        }

        public async Task<bool> Registre(RegisterModelSend model)
        {
            try
            {
                using (HttpClient client = new())
                {

                    var response = await client.PostAsJsonAsync("RegisterUser", model);
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("alert", await response.Content.ReadAsStringAsync(), "ok");
                        return false;
                    }

                }
            }
            catch { return false; }

        }

        public bool UserVerifier()
        {
            if (Preferences.Default.ContainsKey(nameof(App.AppUser)))
            {
                try
                {
                    App.AppUser = JsonConvert.DeserializeObject<User>(Preferences.Get(nameof(App.AppUser), null!));
                    if (App.AppUser != null) return true;
                    return false;
                }
                catch { return false; }
            }
            return false;
        }
        public async Task<bool> UploadPost(Post post)
        {
            if (App.AppUser == null)
            {
                return false;
            }

            try
            {
                post.User = App.AppUser.Email;
                IPostService postService = new PostService();
                return await postService.UploadPost(post);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors du téléchargement du post : {ex.Message}");
                return false;
            }
        }

    }
}
