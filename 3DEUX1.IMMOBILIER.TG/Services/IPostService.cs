using _3DEUX1.IMMOBILIER.TG.Models;

namespace _3DEUX1.IMMOBILIER.TG.Services
{
    public interface IPostService
    {
        Task<bool> UploadPost(Post post);
        Task<List<Post>> DownloadFourRecentPost();
        Task<List<Post>> GetAllPost(int pageIndex);
        Task<List<Post>> GetPostByCategories(int pageIndex, string type, string category);
        Task<List<Post>> GetPostsByEmail(string email, int pageIndex);
        Task<bool> DeletePostByUser(int id);
        Task<Post> GetPostById(int id);
    }
}
