using DaprPoc.BuildingBlocks.Healthchecks;

namespace Auth.Application.Extensions;
 
public static class DaprHealthCheckBuilderExtensions
{
    public static IHealthChecksBuilder AddDapr(this IHealthChecksBuilder builder) =>
        builder.AddCheck<DaprHealthCheck>("dapr");
}
 