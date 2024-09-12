namespace _3DEUX1.IMMOBILIER.TG.Services
{
    public class PartageService
    {
        public PartageService()
        {
        }

        public static async Task SendText(string text)
        {
            await Share.Default.RequestAsync(new ShareTextRequest
            {
                Text = text,
                Title = "Share Text"
            });
        }

        //public static async Task ShareUri(string uri, IShare share)
        //{
        //    await share.RequestAsync(new ShareTextRequest
        //    {
        //        Uri = uri,
        //        Title = "Share Web Link"
        //    });
        //}
    }
}
