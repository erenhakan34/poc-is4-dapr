using Document.Domain;

namespace Document.Application.Services;

public interface IUserDocumentRepository
{
    Task<UserDocument> GetUserDocumentAsync(string userId);
    Task<UserDocument> UpdateUserDocumentAsync(UserDocument userDocument);
    Task DeleteUserDocumentAsync(string id);
}