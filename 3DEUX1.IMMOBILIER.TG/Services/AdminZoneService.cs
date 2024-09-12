using _3DEUX1.IMMOBILIER.TG.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace _3DEUX1.IMMOBILIER.TG.Services;

public class AdminZoneService
{
    private string apiAdress; //"http://10.0.2.2:5223/api/";//localhost:5223/api/
    public AdminZoneService()
    {
        apiAdress = ApiData.GetApiBaseAddress();
    }
    public async Task<List<Post>> AdminZoneGetPostIsNoAvailable(int pageIndex)
    {
        try
        {
            using (HttpClient client = new HttpClient())
            {
                //pageIndex++;
                client.BaseAddress = new Uri(apiAdress);
                var response = await client.GetAsync($"Post/AdminZoneGetPostIsNoAvailable?UserEmail={App.AppUser!.Email}&pageNumber={pageIndex}");
                if (response.IsSuccessStatusCode)
                {
                    var ResString = await response.Content.ReadAsStringAsync();

                    List<Post> res = JsonConvert.DeserializeObject<List<Post>>(ResString)!.ToList();
                    return res;
                }
                return null!;
            }
        }
        catch { return null!; }
    }

    public async Task<bool> AutorisePost(UpdatePostModel model)
    {
        using (HttpClient client = new HttpClient())
        {
            client.BaseAddress = new Uri(apiAdress);
            //string JsonPost = JsonConvert.SerializeObject(post);
            //var content = new StringContent(JsonPost, Encoding.UTF8, "application/json");
            var res = await client.PutAsJsonAsync("Post/UpdatePost", model);
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
