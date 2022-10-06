using Microsoft.AspNetCore.Http;

namespace Auth.Application;

 
public class IdentityService : IIdentityService
{
    private IHttpContextAccessor _context; 

    public IdentityService(IHttpContextAccessor context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public string GetUserIdentity() =>
        _context.HttpContext?.User?.FindFirst("sub")?.Value ?? string.Empty;
    
    public string GetUserNameIdentity() =>
        _context.HttpContext?.User?.FindFirst("name")?.Value ?? string.Empty;
    public string GetUserEmailIdentity() =>
        _context.HttpContext?.User?.FindFirst("email")?.Value ?? string.Empty;
}
public interface IIdentityService
{
    string GetUserIdentity();
    string GetUserNameIdentity();
    string GetUserEmailIdentity();
}
