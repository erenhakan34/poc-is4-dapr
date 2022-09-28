using Document.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Document.API.Controllers;

[ApiController]
[Route("[controller]")]
public class DocumentsController : ControllerBase
{
    private readonly IDocumentRepository _documentRepository;

    public DocumentsController(IDocumentRepository documentRepository)
    {
        _documentRepository = documentRepository;
    }

    [HttpGet("all")]
    public async Task<ActionResult> GetAll()
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