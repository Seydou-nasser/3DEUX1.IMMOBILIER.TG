using CommunityToolkit.Maui.Views;
using _3DEUX1.IMMOBILIER.TG.Models;
using CommunityToolkit.Mvvm.Input;
using _3DEUX1.IMMOBILIER.TG.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;
namespace _3DEUX1.IMMOBILIER.TG.Views.PopupPersonaliser;

public partial class ChoicePopup : Popup
{
	public MvvmHelpers.ObservableRangeCollection<ChoicePopupArg> Values { get; set; } = new(); 
    public ChoicePopup(List<ChoicePopupArg> valuePairs)
	{
		InitializeComponent();
		BindingContext = this;
		Values.AddRange(valuePairs);
    }
	[RelayCommand]
	public async Task NavigationToPage(string route)
	{
		await NavigationHelper.NavigateAsync(route);
		await this.CloseAsync();
	}
}