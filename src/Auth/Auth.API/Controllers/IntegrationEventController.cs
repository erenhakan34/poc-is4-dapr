using Auth.Application;
using Dapr;
using DaprPoc.BuildingBlocks.EventBus.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers;

[Route("[controller]")]
[ApiController]
public class IntegrationEventController : ControllerBase
{
    private const string DAPR_PUBSUB_NAME = "daprpoc-pubsub";

    private readonly IIdentityService _identityService;
    private readonly ILogger<IntegrationEventController> _logger;
    private readonly IEventBus _eventBus;

    public IntegrationEventController(IIdentityService identityService, ILogger<IntegrationEventController> logger, IEventBus eventBus)
    {
        _identityService = identityService;
        _logger = logger;
        _eventBus = eventBus;
    }
    [HttpPost("/UserRegistered")]
    [Topic(DAPR_PUBSUB_NAME, nameof(UserRegisteredIntegrationEvent))]
    public void  HandleAsync(UserRegisteredIntegrationEvent @event)
    {
        _logger.LogInformation("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", @event);
    }
}