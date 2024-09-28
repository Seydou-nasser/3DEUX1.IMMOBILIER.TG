using System.Net.Http.Headers;
using _3DEUX1.IMMOBILIER.TG.Services;

public class JwtAuthenticationHandler : DelegatingHandler
{

    public JwtAuthenticationHandler()
    {
    }

    //protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    //{
    //    var token = await UserService.GetJwtToken();
    //    if (!string.IsNullOrEmpty(token))
    //    {
    //        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
    //    }
    //    return await base.SendAsync(request, cancellationToken);
    //}
}