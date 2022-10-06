namespace DaprPoc.BuildingBlocks.EventBus;

public class DaprEventBus : IEventBus
{
    private const string PubSubName = "daprpoc-pubsub";

    private readonly DaprClient _dapr;
    private readonly ILogger _logger;

    public DaprEventBus(DaprClient dapr, ILogger<DaprEventBus> logger)
    {
        _dapr = dapr;
        _logger = logger;
    }

    public async Task PublishAsync(IntegrationEvent integrationEvent)
    {
        var topicName = integrationEvent.GetType().Name;

        _logger.LogInformation(
            "Publishing event {@Event} to {PubsubName}.{TopicName}",
            integrationEvent,
            PubSubName,
            topicName);
        await _dapr.PublishEventAsync(PubSubName, topicName, (object)integrationEvent);
    }
}
