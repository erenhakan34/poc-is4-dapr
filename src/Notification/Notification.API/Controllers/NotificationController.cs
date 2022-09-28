using Dapr;
using Microsoft.AspNetCore.Mvc;
using Notification.API.Models;
using Notification.API.Services;

namespace Notification.API.Controllers;

[ApiController]
[Route("[controller]")]
public class NotificationController : ControllerBase
{
    private readonly ILogger<NotificationController> _logger;
    private readonly EmailSender _emailSender;

    public NotificationController(ILogger<NotificationController> logger, EmailSender emailSender)
    {
        this._logger = logger;
        this._emailSender = emailSender;
    }

    [HttpPost("", Name = "SubmitUserRegister")]
    [Topic("pubsub", "notifications")]
    public async Task<IActionResult> Submit(UserRegistered registered)
    {
        _logger.LogInformation($"Received a new register from {registered.FullName}");
        await _emailSender.SendEmailForOrder(registered);
        return Ok();
    }
}
