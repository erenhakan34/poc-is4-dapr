using Dapr.Client;
using Notification.API.Models;

namespace Notification.API.Services;
public class EmailSender
{
    private readonly DaprClient daprClient;
    private readonly ILogger<EmailSender> logger;

    public EmailSender(DaprClient daprClient, ILogger<EmailSender> logger)
    {
        this.daprClient = daprClient;
        this.logger = logger;
    }

    public async Task SendEmailForOrder(UserRegistered user)
    {
        logger.LogInformation($"Received a new user register for {user.FullName} {user.Email}");

        var daprEnabled = !String.IsNullOrEmpty(Environment.GetEnvironmentVariable("DAPR_HTTP_PORT"));
        if (!daprEnabled) 
        { 
            logger.LogWarning("Not using Dapr so no email sent");
            return;
        }
        
        logger.LogInformation($"Sending email");
        var metadata = new Dictionary<string, string>
        {
            ["emailFrom"] = "kbxototest3@outlook.com",
            ["emailTo"] = user.Email,
            ["subject"] = $"Thank you for register"
        };
        var body = $"<h2>Your register completed</h2>";
        await daprClient.InvokeBindingAsync("sendmail", "create", 
            body, metadata);        
    }
}