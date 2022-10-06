using Microsoft.AspNetCore.Http;
using System.Security.Claims;
namespace Auth.Application;

public class HttpContextProvider : IHttpContextProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpContextProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }


    public string? Token => _httpContextAccessor?.HttpContext?.Request?.Headers["Authorization"].ToString();
    public string? UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
    public string? RemoteIpAddress => _httpContextAccessor?.HttpContext?.Connection?.RemoteIpAddress?.ToString();

}