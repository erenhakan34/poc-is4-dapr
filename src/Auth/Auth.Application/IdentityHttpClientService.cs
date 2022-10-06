using System.Net.Http.Json;
using Auth.Application.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Auth.Domain;
using Microsoft.Extensions.Configuration;

namespace Auth.Application;

public class IdentityHttpClientService:IIdentityHttpClientService
{
    private readonly HttpClient _httpClient;
    private readonly IServiceProvider _serviceProvider;

    public IdentityHttpClientService(
        HttpClient httpClient,
        IServiceProvider serviceProvider, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _serviceProvider = serviceProvider;
        _httpClient.BaseAddress = new Uri(configuration.GetValue<string>("IdentityUrl"));
    }

    public async Task<IdentityUserModel> GetUserInformationAsync()
    {
        using var scope = _serviceProvider.CreateScope();

        _httpClient.AddAuthToken(scope.ServiceProvider);
        
        var response = await _httpClient.GetAsync("/connect/userInfo").ConfigureAwait(false);
        Console.WriteLine(response);
        var model = await response.Content.ReadFromJsonAsync<IdentityUserModel>();

        return model;
    }
}