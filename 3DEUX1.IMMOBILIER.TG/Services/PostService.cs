using _3DEUX1.IMMOBILIER.TG.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace _3DEUX1.IMMOBILIER.TG.Services
{
    public class PostService : IPostService
    {
        private readonly string _apiAddress;
        private readonly HttpClient _httpClient;

        public PostService(HttpClient httpClient)
        {
            _apiAddress = ApiData.GetApiBaseAddress();
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(_apiAddress);
        }

        // Méthode pour récupérer les quatre posts les plus récents
        public async Task<List<Post>> DownloadFourRecentPost()
        {
            try
            {
                // Envoi de la requête GET
                var response = await _httpClient.GetAsync("Post/GetFourPost");
                if (response.IsSuccessStatusCode)
                {
                    var resString = await response.Content.ReadAsStringAsync();
                    // Désérialisation de la réponse JSON en liste de Post
                    return JsonConvert.DeserializeObject<List<Post>>(resString) ?? new List<Post>();
                }
                return new List<Post>();
            }
            catch (Exception ex)
            {
                // Logging de l'erreur
                Console.WriteLine($"Erreur lors du téléchargement des posts récents : {ex.Message}");
                return new List<Post>();
            }
        }

        // Méthode pour récupérer tous les posts avec pagination
        public async Task<List<Post>> GetAllPost(int pageIndex)
        {
            try
            {
                var response = await _httpClient.GetAsync($"Post/GetAllPostNotDelected?pageNumber={pageIndex}");
                if (response.IsSuccessStatusCode)
                {
                    var resString = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Post>>(resString) ?? new List<Post>();
                }
                return new List<Post>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la récupération de tous les posts : {ex.Message}");
                return new List<Post>();
            }
        }

        // Méthode pour récupérer les posts par catégorie
        public async Task<List<Post>> GetPostByCategories(int pageIndex, string type, string cat)
        {
            try
            {
                // Construction de l'URL avec les paramètres
                var response = await _httpClient.GetAsync($"Post/GetAllPostByCategory/{type}/{cat}?pageNumber={pageIndex}");
                if (response.IsSuccessStatusCode)
                {
                    var resString = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Post>>(resString) ?? new List<Post>();
                }
                return new List<Post>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la récupération des posts par catégorie : {ex.Message}");
                return new List<Post>();
            }
        }

        // Méthode pour télécharger un nouveau post
        public async Task<bool> UploadPost(Post post)
        {
            try
            {
                // Envoi de la requête POST avec le post sérialisé en JSON
                var res = await _httpClient.PostAsJsonAsync("Post", post);
                if (res.IsSuccessStatusCode)
                {
                    await CreatSnackBar.SnackBarShow("Envoyé !");
                    return true;
                }
                await CreatSnackBar.SnackBarShow("Veuillez réessayer !");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de l'upload du post : {ex.Message}");
                await CreatSnackBar.SnackBarShow("Une erreur est survenue. Veuillez réessayer !");
                return false;
            }
        }
    }
}
