using _3DEUX1.IMMOBILIER.TG.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace _3DEUX1.IMMOBILIER.TG.Services
{
    public class PostService : IPostService
    {
        //int pageIndex = 0;
        private string apiAdress; //"http://10.0.2.2:5223/api/";//localhost:5223/api/

        public PostService()
        {
            apiAdress = ApiData.GetApiBaseAddress();
        }

        public async Task<List<Post>> DownloadFourRecentPost()
        {

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiAdress);
                    //client.Timeout = TimeSpan.FromSeconds(120) ;
                    var response = await client.GetAsync("Post/GetFourPost");
                    if (response.IsSuccessStatusCode)
                    {
                        var ResString = await response.Content.ReadAsStringAsync();

                        var res = JsonConvert.DeserializeObject<List<Post>>(ResString)!.ToList();
                        return res;
                    }
                    return new();
                }
            }
            catch { return null!; }
        }

        public async Task<List<Post>> GetAllPost(int pageIndex)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    //pageIndex++;
                    client.BaseAddress = new Uri(apiAdress);
                    var response = await client.GetAsync($"Post/GetAllPostNotDelected?pageNumber={pageIndex}");
                    if (response.IsSuccessStatusCode)
                    {
                        var ResString = await response.Content.ReadAsStringAsync();

                        List<Post> res = JsonConvert.DeserializeObject<List<Post>>(ResString)!.ToList();
                        return res;
                    }
                    return new List<Post>();
                }
            }
            catch { return null!; }
        }

        public async Task<List<Post>> GetPostByCategories(int pageIndex, string type, string cat)
        {
            // Post/GetAllPostByCategory/Locations/Villa?pageNumber=1

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    //pageIndex++;
                    client.BaseAddress = new Uri(apiAdress);
                    var response = await client.GetAsync($"Post/GetAllPostByCategory/{type}/{cat}?pageNumber={pageIndex}");
                    if (response.IsSuccessStatusCode)
                    {
                        var ResString = await response.Content.ReadAsStringAsync();

                        List<Post> res = JsonConvert.DeserializeObject<List<Post>>(ResString)!.ToList();
                        return res;
                    }
                    return new List<Post>();
                }
            }
            catch { return null!; }
        }

        public async Task<bool> UploadPost(Post post)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiAdress);
                //string JsonPost = JsonConvert.SerializeObject(post);
                //var content = new StringContent(JsonPost, Encoding.UTF8, "application/json");
                var res = await client.PostAsJsonAsync("Post", post);
                if (res.IsSuccessStatusCode)
                {
                    await CreatSnackBar.SnackBarShow("Envoyer !");
                    return true;
                }
            }
            await CreatSnackBar.SnackBarShow("veuillez reeseyer !");
            return false;
        }
    }
}
