using Dapr;
using Microsoft.AspNetCore.Mvc;
using Notification.API.Models;
using Notification.API.Services;

namespace Notification.API.Controllers;

[ApiController]
[Route("[controller]")]
public class NotificationController : ControllerBase
{
    private const string DAPR_PUBSUB_NAME = "daprpoc-pubsub";
    private readonly ILogger<NotificationController> _logger;
    private readonly EmailSender _emailSender;

    public NotificationController(ILogger<NotificationController> logger, EmailSender emailSender)
    {
        this._logger = logger;
        this._emailSender = emailSender;
    }

    [HttpPost("/SubmitUserRegister")]
    [Topic(DAPR_PUBSUB_NAME, "UserRegistered")]
    public async Task<IActionResult> Submit(UserRegistered registered)
    {
        _logger.LogInformation($"Received a new register from {registered.FullName}");
        await _emailSender.SendEmailForOrder(registered);
        return Ok();
    }
    
    
    [HttpPost("/SubmitSmsOtp")]
    [Topic(DAPR_PUBSUB_NAME, "SmsOtp")]
    public async Task<IActionResult> Submit(SmsOtp registered)
    {
        _logger.LogInformation($"Received a new otpfrom {registered.Email}");
        await _emailSender.SendEmailForOrder(registered);
        return Ok();
    }
}
