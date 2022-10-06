using System.Net;
using Document.Application.Services;
using Document.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Document.API.Controllers;

[Route("[controller]")]
[ApiController]
public class UserDocumentController : ControllerBase
{
    private readonly IUserDocumentRepository _repository;
    private readonly ILogger<UserDocumentController> _logger;

    public UserDocumentController(
        IUserDocumentRepository repository,
      
        ILogger<UserDocumentController> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    [HttpGet]
    [ProducesResponseType(typeof(UserDocument), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<UserDocument>> GetUserDocumentAsync([FromQuery] string userId)
    {
        var userDocument = await _repository.GetUserDocumentAsync(userId);
        return Ok(userDocument ?? new UserDocument(userId));
    }

    [HttpPost]
    [ProducesResponseType(typeof(UserDocument), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<UserDocument>> UpdateUserDocumentAsync([FromQuery] string userId,[FromBody] UserDocument value)
    {

        value.UserId = userId;
        return Ok(await _repository.UpdateUserDocumentAsync(value));
    }

    // DELETE api/values/5
    [HttpDelete]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
    public async Task DeleteUserDocumentAsync(string userId)
    {
        _logger.LogInformation("Deleting UserDocument for user {UserId}...", userId);

        await _repository.DeleteUserDocumentAsync(userId);
    }
}
