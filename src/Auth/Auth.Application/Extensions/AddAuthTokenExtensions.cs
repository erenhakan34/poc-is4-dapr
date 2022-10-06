using System.Security.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Application.Extensions;

public static class AddAuthTokenExtensions
{
    public static void AddAuthToken(this HttpClient httpClient, IServiceProvider serviceProvider,
        bool isRequired = true)
    {
        var httpContextProvider = serviceProvider.GetRequiredService<IHttpContextProvider>();

        if (isRequired && string.IsNullOrEmpty(httpContextProvider.Token))
            throw new AuthenticationException();

        if (httpClient.DefaultRequestHeaders.Any(x => x.Key == "Authorization"))
            httpClient.DefaultRequestHeaders.Remove("Authorization");

        if (string.IsNullOrEmpty(httpContextProvider.Token) == false)
            httpClient.DefaultRequestHeaders.Add("Authorization", httpContextProvider.Token);
    }
}