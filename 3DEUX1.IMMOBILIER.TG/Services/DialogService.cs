public class DialogService : IDialogService
{
    public async Task<bool> DisplayAlert(string title, string message, string accept, string cancel)
    {
        return await Application.Current!.MainPage!.DisplayAlert(title, message, accept, cancel);
    }
}