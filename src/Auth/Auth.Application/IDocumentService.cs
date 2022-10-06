using Auth.Domain;

namespace Auth.Application;

public interface IDocumentService
{
    Task<IEnumerable<DocumentItem>?> GetDocumentItemsAsync(string userId);
}