using _3DEUX1.IMMOBILIER.TG.Models;
using _3DEUX1.IMMOBILIER.TG.Models.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace _3DEUX1.IMMOBILIER.TG.Services
{
    public class UserService : IUserService
    {
        private readonly string _apiAddress;
        private readonly HttpClient _httpClient;

        // Constructeur avec injection de dépendance pour HttpClient
        public UserService(HttpClient httpClient)
        {
            _apiAddress = ApiData.GetApiBaseAddress() + "Users/";
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(_apiAddress);
        }

        // Méthode pour authentifier un utilisateur
        public async Task<User?> Login(string email, string password)
        {
            try
            {
                var loginModel = new LoginModel() { Email =  email, Password = password };
                var response = await _httpClient.PostAsJsonAsync("login",loginModel);
                if (response.IsSuccessStatusCode)
                {
                    var loginResponse = await response.Content.ReadFromJsonAsync<LoginAndTokenResponse>();
                    if (loginResponse != null)
                    {
                        // Stockez le token JWT
                        await SecureStorage.Default.SetAsync("jwt_token", loginResponse.Token!);
                        return loginResponse.User;
                    }
                }

                await Application.Current!.MainPage!.DisplayAlert(email, await response.Content.ReadAsStringAsync(), "OK");
                return null;
            }
            catch (Exception ex)
            {
                // Logging de l'erreur
                Console.WriteLine($"Erreur lors de la connexion : {ex.Message}");
                return null;
            }
        }

        // Méthode pour rafraîchir les informations de connexion d'un utilisateur
        public async Task<User?> RefrechLogin(string email)
        {
            try
            {
                var response = await _httpClient.GetAsync($"RefrechLogin/{email}");
                if (response.IsSuccessStatusCode)
                {
                    var loginResponse = await response.Content.ReadFromJsonAsync<LoginAndTokenResponse>();
                    if (loginResponse != null)
                    {
                        // Stockez le token JWT
                        await SecureStorage.SetAsync("jwt_token", loginResponse.Token!);
                        return loginResponse.User;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors du rafraîchissement de la connexion : {ex.Message}");
                return null;
            }
        }

        // Méthode pour déconnecter l'utilisateur
        public void Logout()
        {
            Preferences.Default.Clear();
        }

        // Méthode pour enregistrer un nouvel utilisateur
        public async Task<bool> Registre(RegisterModelSend model)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("RegisterUser", model);
                if (response.IsSuccessStatusCode) return true;

                // Affichage d'une alerte en cas d'échec d'enregistrement
                await Shell.Current.DisplayAlert("alert", await response.Content.ReadAsStringAsync(), "ok");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de l'enregistrement : {ex.Message}");
                return false;
            }
        }

        // Méthode pour vérifier si un utilisateur est connecté
        public async Task<bool> UserVerifier()
        {
            //var token = await GetJwtToken();
            //if (!string.IsNullOrEmpty(token))
            //{
            //    // Vérifiez la validité du token (vous pouvez ajouter une méthode pour vérifier le token côté serveur)
            //    var user = await RefrechLogin(token);
            //    if (user != null)
            //    {
            //        App.AppUser = user;
            //        return true;
            //    }
            //}
            return false;
        }

        // Méthode pour télécharger un nouveau post
        public async Task<bool> UploadPost(Post post)
        {
            if (App.AppUser == null)
            {
                return false;
            }

            try
            {
                post.User = App.AppUser.Email;
                _httpClient.BaseAddress = new Uri(ApiData.GetApiBaseAddress());
                var response = await _httpClient.PostAsJsonAsync("Post", post);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors du téléchargement du post : {ex.Message}");
                return false;
            }
        }

    }

}
