using _3DEUX1.IMMOBILIER.TG.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace _3DEUX1.IMMOBILIER.TG.Services
{
    public class UserService : IUserService
    {
        private string apiAdress ;//"http://10.0.2.2:5223/api/Users/";//http://localhost:5223/api/Users/

        public UserService()
        {
            apiAdress = ApiData.GetApiBaseAddress() + "Users/";
        }

        public async Task<User> Login(string email, string password)
        {
            try
            {
                using (HttpClient client = new())
                {
                    client.BaseAddress = new Uri(apiAdress);
                    var response = await client.GetAsync($"{email}/{password}");
                    if (response.IsSuccessStatusCode)
                    {
                        User? user = await response.Content.ReadFromJsonAsync<User>();
                        return user!;
                    }
                    else
                    {
                        await Application.Current!.MainPage!.DisplayAlert(email, $"{response.Content.ReadAsStringAsync().Result}", "OK");
                    }

                }
                return null!;
            }
            catch
            {

                return null!;
            }

        }

        public async Task<User> RefrechLogin(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email)) return null!;
                using (HttpClient client = new())
                {
                    client.BaseAddress = new Uri(apiAdress);
                    var response = await client.GetAsync($"{email}");
                    //Console.WriteLine(response.IsSuccessStatusCode);
                    if (response.IsSuccessStatusCode)
                    {
                        User? user = await response.Content.ReadFromJsonAsync<User>();
                        return user!;
                    }
                    return null!;
                }
            }
            catch { return null!; }
        }

        public void logout()
        {
            Preferences.Default.Clear();
        }

        public async Task<bool> Registre(RegisterModelSend model)
        {
            try
            {
                using (HttpClient client = new())
                {

                    client.BaseAddress = new Uri(apiAdress);
                    var response = await client.PostAsJsonAsync("RegisterUser", model);
                    if (response.IsSuccessStatusCode)
                    {
                        //return response.Result.Content.ReadAsStringAsync();
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
            try
            {
                if (App.AppUser != null)
                {
                    post.User = App.AppUser.Email;
                    IPostService postService = new PostService();
                    bool res = await postService.UploadPost(post);
                    return res;
                }
                return false;
            }
            catch
            {
                return false;
            }

        }

    }
}
