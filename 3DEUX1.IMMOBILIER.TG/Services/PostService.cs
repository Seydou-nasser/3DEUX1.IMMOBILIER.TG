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
        public async Task<List<Post>> GetAllPost(int pageNumber)
        {
            try
            {
                var response = await _httpClient.GetAsync($"Post/GetAllPostNotDelected?pageNumber={pageNumber}");
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
        public async Task<List<Post>> GetPostByCategories(int pageNumber, string type, string category)
        {
            try
            {
                var response = await _httpClient.GetAsync($"Post/GetAllPostByCategory/{type}/{category}?pageNumber={pageNumber}");
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

        // Méthode pour récupérer les posts par email
        public async Task<List<Post>> GetPostsByEmail(string email, int pageNumber)
        {
            try
            {
                var response = await _httpClient.GetAsync($"Post/GetPostsByUser/{email}?pageNumber={pageNumber}");
                if (response.IsSuccessStatusCode)
                {
                    var resString = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Post>>(resString) ?? new List<Post>();
                }
                return new List<Post>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la récupération des posts par email : {ex.Message}");
                return new List<Post>();
            }
        }

        public async Task<bool> DeletePostByUser(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"Post/DeletePostByUser/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la suppression du post : {ex.Message}");
                return false;
            }
        }

        // Nouvelle méthode pour récupérer un post par son ID
        public async Task<Post> GetPostById(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"Post/GetPost/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var resString = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Post>(resString) ?? new Post();
                }
                return new Post();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la récupération du post par ID : {ex.Message}");
                return new Post();
            }
        }

        // Nouvelle méthode pour mettre à jour un post
        public async Task<bool> UpdatePost(UpdatePostModel model)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("Post/UpdatePost", model);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la mise à jour du post : {ex.Message}");
                return false;
            }
        }
    }


}
