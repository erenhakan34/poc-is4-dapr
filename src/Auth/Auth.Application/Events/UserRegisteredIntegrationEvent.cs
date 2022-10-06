using DaprPoc.BuildingBlocks.EventBus.Events;

public record UserRegisteredIntegrationEvent(string UserId) : IntegrationEvent;