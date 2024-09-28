namespace _3DEUX1.IMMOBILIER.TG.Services
{
    public class ApiData
    {

        public static string GetApiBaseAddress()
        {
            if (DeviceInfo.Platform == DevicePlatform.Android) return "http://10.0.2.2:5223/api/";
            else return "http://localhost:5223/api/";
        }
    }
}
