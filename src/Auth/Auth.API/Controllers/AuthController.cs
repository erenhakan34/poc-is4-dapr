using Auth.API.Services;
using Auth.Application;
using Dapr;
using DaprPoc.BuildingBlocks.EventBus.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers;

[Route("[controller]")]
[Authorize]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IIdentityService _identityService;
    private readonly ILogger<AuthController> _logger;
    private readonly IEventBus _eventBus;
    private readonly IDocumentService _documentService;

    private readonly IDocumentRequestGrpcService _documentRequestGrpcService;

    public AuthController(IIdentityService identityService, ILogger<AuthController> logger, IEventBus eventBus,
        IDocumentService documentService, IDocumentRequestGrpcService documentRequestGrpcService)
    {
        _identityService = identityService;
        _logger = logger;
        _eventBus = eventBus;
        _documentService = documentService;
        _documentRequestGrpcService = documentRequestGrpcService;
    }

    [HttpGet("userinfo")]
    public async Task<ActionResult> Get()
    {
        var userId = _identityService.GetUserIdentity();
        var eventMessage = new UserRegisteredIntegrationEvent(userId);
        await _eventBus.PublishAsync(eventMessage);
        return Ok(userId);
    }

    [HttpGet("sendSms")]
    public async Task<ActionResult> SendSms()
    {

        Random rnd = new();
        var sms = rnd.Next(100000,999999);
        //var name = _identityService.GetUserNameIdentity();
        var email = _identityService.GetUserEmailIdentity();
        SmsOtp eventMessage = new(sms.ToString(), email);
        await _eventBus.PublishAsync(eventMessage);
        return Ok();
    }

    [HttpGet("userdocuments")]
    public async Task<ActionResult> GetUserDocuments()
    {
        var userId = _identityService.GetUserIdentity();
        var docs = await _documentService.GetDocumentItemsAsync(userId);
        return Ok(docs);
    }
    
    [HttpGet("usergrpcdocuments")]
    public async Task<ActionResult> GetUserGrpcDocuments()
    {
        var userId = _identityService.GetUserIdentity();
        var docs = await _documentRequestGrpcService.GetDocument(userId);
        return Ok(docs);
    }
}