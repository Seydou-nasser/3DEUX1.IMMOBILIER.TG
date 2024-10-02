
using CommunityToolkit.Mvvm.ComponentModel;

namespace _3DEUX1.IMMOBILIER.TG.Models
{
    public partial class Post : ObservableObject
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? Prix { get; set; }
        public int? Avance { get; set; }
        public List<string>? Caracteristique { get; set; }
        [ObservableProperty]
        public string? type;
        [ObservableProperty]
        public string? categories;
        public bool? IsDelete { get; set; } = false;
        public bool? Isavailable { get; set; } = false;
        public DateTime? DateTime { get; set; }
        public string? Localisation { get; set; }
        public List<string>? Images { get; set; }
        public string? User { get; set; }
        public string? UserNum { get; set; }
        public string? AdminZoneConfirmed { get; set; }
    }
}
