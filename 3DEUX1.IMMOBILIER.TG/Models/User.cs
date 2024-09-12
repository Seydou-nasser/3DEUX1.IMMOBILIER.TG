namespace _3DEUX1.IMMOBILIER.TG.Models;

public class User
{
    public string Email { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Photo { get; set; } = string.Empty;
    public List<string>? Role { get; set; }
}
