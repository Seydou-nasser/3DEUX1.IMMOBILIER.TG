namespace _3DEUX1.IMMOBILIER.TG.Models.Models
{
    public class LoginResponse
    {
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Photo {  get; set; } = string.Empty;
        public object? Role { get; set; }
    }
}
