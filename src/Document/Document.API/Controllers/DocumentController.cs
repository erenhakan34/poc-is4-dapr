using Document.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Document.API.Controllers;

[ApiController]
[Route("[controller]")]
public class DocumentsController : ControllerBase
{
    private readonly IDocumentRepository _documentRepository;
    private readonly IUserDocumentRepository _userDocumentRepository;
    public DocumentsController(IDocumentRepository documentRepository, IUserDocumentRepository userDocumentRepository)
    {
        _documentRepository = documentRepository;
        _userDocumentRepository = userDocumentRepository;
    }

    [HttpGet("all")]
    public async Task<ActionResult> GetAll()
    {
        var result = await _documentRepository.GetAll();
        return Ok(result);
    }
    
    [HttpGet("items/by_userid")]
    public async Task<ActionResult> ItemsAsync([FromQuery] string id)
    {
        var result = await _documentRepository.GetAll();
        return Ok(result);
    }

    [HttpPost("Add")]
    public async Task<ActionResult> Add([FromBody] Domain.Document request)
    {
        await _documentRepository.AddAsync(request);

        return Ok();
    }
}