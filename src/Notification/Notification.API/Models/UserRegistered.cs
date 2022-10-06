using DaprPoc.BuildingBlocks.EventBus.Events;

namespace Notification.API.Models;

public record UserRegistered(string FullName,string Email) : IntegrationEvent;


public record SmsOtp(string code,string Email) : IntegrationEvent;