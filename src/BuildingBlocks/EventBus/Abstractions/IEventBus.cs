namespace DaprPoc.BuildingBlocks.EventBus.Abstractions;

public interface IEventBus
{
    Task PublishAsync(IntegrationEvent integrationEvent);
}
