namespace Auth.Application;

public interface IHttpContextProvider
{
    string? Token { get; }
    string? UserId { get; }
 
}