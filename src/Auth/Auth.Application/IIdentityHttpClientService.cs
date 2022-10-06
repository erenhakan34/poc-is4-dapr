using Auth.Domain;

namespace Auth.Application;

public interface IIdentityHttpClientService
{
    Task<IdentityUserModel> GetUserInformationAsync();
}