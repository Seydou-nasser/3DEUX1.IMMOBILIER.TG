namespace _3DEUX1.IMMOBILIER.TG.Helpers;

public static class ImageHelper
{
    public static ImageSource FromBase64(string base64)
    {
        byte[] imageBytes = Convert.FromBase64String(base64);
        return ImageSource.FromStream(() => new MemoryStream(imageBytes));
    }
}
