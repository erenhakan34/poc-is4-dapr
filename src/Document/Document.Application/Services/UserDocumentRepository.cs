using Dapr.Client;
using Document.Domain;
using Microsoft.Extensions.Logging;

namespace Document.Application.Services;
 
public class UserDocumentRepository : IUserDocumentRepository
{
    private const string StateStoreName = "daprpoc-statestore";

    private readonly DaprClient _daprClient;
    private readonly ILogger _logger;

    public UserDocumentRepository(DaprClient daprClient, ILogger<UserDocumentRepository> logger)
    {
        _daprClient = daprClient;
        _logger = logger;
    }
    
    public Task DeleteUserDocumentAsync(string id) =>
        _daprClient.DeleteStateAsync(StateStoreName, id);
    public Task<UserDocument> GetUserDocumentAsync(string customerId) =>
        _daprClient.GetStateAsync<UserDocument>(StateStoreName, customerId);

    public async Task<UserDocument> UpdateUserDocumentAsync(UserDocument userDocument)
    {
        var state = await _daprClient.GetStateEntryAsync<UserDocument>(StateStoreName, userDocument.UserId);
        state.Value = userDocument;

        await state.SaveAsync();

        _logger.LogInformation("UserDocument item persisted successfully.");

        return await GetUserDocumentAsync(userDocument.UserId);
    }
}