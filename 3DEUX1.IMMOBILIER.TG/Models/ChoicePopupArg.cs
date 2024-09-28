
using CommunityToolkit.Mvvm.ComponentModel;

namespace _3DEUX1.IMMOBILIER.TG.Models
{
    public partial class ChoicePopupArg : ObservableObject
    {
        public string route { get;set; } = string.Empty;
        public string title { get; set; } = string.Empty;
        public string image { get; set; } = string.Empty;

        public ChoicePopupArg(string route, string title, string image)
        {
            this.route = route; 
            this.title = title;
            this.image = image;
        }
    }
}
