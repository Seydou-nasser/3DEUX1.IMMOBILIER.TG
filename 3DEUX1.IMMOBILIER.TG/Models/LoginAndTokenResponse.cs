using _3DEUX1.IMMOBILIER.TG.Services;

namespace _3DEUX1.IMMOBILIER.TG.Models
{
    public class LoginAndTokenResponse
    {
        public User? User { get; set; }
        public string? Token { get; set; }
    }
}
