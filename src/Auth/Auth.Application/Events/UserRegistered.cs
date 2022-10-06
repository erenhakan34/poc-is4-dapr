using DaprPoc.BuildingBlocks.EventBus.Events;

public record UserRegistered(string FullName,string Email) : IntegrationEvent;

public record SmsOtp(string code,string Email) : IntegrationEvent;