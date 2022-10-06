using System.Net.Http.Json;
using Auth.Domain;

namespace Auth.Application;

public class DocumentService : IDocumentService
{
    private readonly HttpClient _httpClient;

    public DocumentService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<IEnumerable<DocumentItem>?> GetDocumentItemsAsync(string userId)
    {
        var requestUri = $"/documents/items/by_userid?id={userId}";

        return _httpClient.GetFromJsonAsync<IEnumerable<DocumentItem>>(requestUri);
    }
}