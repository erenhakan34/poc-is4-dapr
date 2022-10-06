using System.IdentityModel.Tokens.Jwt;
using Auth.API.Filters;
using Auth.API.Services;
using Auth.Application;
using Dapr.Client;
using Dapr.Extensions.Configuration;
using DaprPoc.BuildingBlocks.EventBus;
using DaprPoc.BuildingBlocks.EventBus.Abstractions;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using Polly;
using Serilog;

namespace Auth.API;

public static class ProgramExtensions
{
    private const string AppName = "auth_api";

    public static void AddCustomConfiguration(this WebApplicationBuilder builder)
    {
        builder.Configuration.AddDaprSecretStore(
         "daprpoc-secretstore",
        new DaprClientBuilder().Build());
    }

    public static void AddCustomSerilog(this WebApplicationBuilder builder)
    {
        var seqServerUrl = builder.Configuration["SeqServerUrl"];

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .WriteTo.Console()
            .WriteTo.Seq(seqServerUrl)
            .Enrich.WithProperty("ApplicationName", AppName)
            .CreateLogger();

        builder.Host.UseSerilog();
    }

    public static void AddCustomSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = $"AuthOnDapr - {AppName}", Version = "v1" });

            var identityUrlExternal = builder.Configuration.GetValue<string>("IdentityUrlExternal");

            c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows()
                {
                    Implicit = new OpenApiOAuthFlow()
                    {
                        AuthorizationUrl = new Uri($"{identityUrlExternal}/connect/authorize"),
                        TokenUrl = new Uri($"{identityUrlExternal}/connect/token"),
                        Scopes = new Dictionary<string, string>()
                            {
                                { "auth_api", AppName }
                            }
                    }
                }
            });

            c.OperationFilter<AuthorizeCheckOperationFilter>();
        });
    }

    public static void UseCustomSwagger(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{AppName} V1");
            c.OAuthClientId("skoruba_identity_admin_api_swaggerui");
            c.OAuthAppName("Auth Swagger UI");
        });
    }

    public static void AddCustomAuthentication(this WebApplicationBuilder builder)
    {
        // Prevent mapping "sub" claim to nameidentifier.
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

        builder.Services.AddAuthentication("Bearer")
            .AddJwtBearer(options =>
            {
                options.Audience = "auth_api";
                options.Authority = builder.Configuration.GetValue<string>("IdentityUrl");
                options.RequireHttpsMetadata = false;
            });
    }

    public static void AddCustomAuthorization(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("ApiScope", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("scope", "auth_api email openid profile");
            });
        });
    }

    public static void AddCustomHealthChecks(this WebApplicationBuilder builder) =>
        builder.Services.AddHealthChecks()
            .AddCheck("self", () => HealthCheckResult.Healthy());
         //   .AddDapr();
    
    public static void AddCustomApplicationServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IEventBus, DaprEventBus>();
        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        builder.Services.AddScoped<IIdentityService, IdentityService>();
        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        builder.Services.AddTransient<IDocumentRequestGrpcService, DocumentRequestGrpcService>();

        
        builder.Services.AddSingleton<IDocumentService, DocumentService>(
            _ => new DocumentService(DaprClient.CreateInvokeHttpClient("documents")));

      
    }
   

    private static Policy CreateRetryPolicy(IConfiguration configuration, Serilog.ILogger logger)
    {
        // Only use a retry policy if configured to do so.
        // When running in an orchestrator/K8s, it will take care of restarting failed services.
        if (bool.TryParse(configuration["RetryMigrations"], out bool retryMigrations))
        {
            return Policy.Handle<Exception>().
                WaitAndRetryForever(
                    sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                    onRetry: (exception, retry, timeSpan) =>
                    {
                        logger.Warning(
                            exception,
                            "Exception {ExceptionType} with message {Message} detected during database migration (retry attempt {retry})",
                            exception.GetType().Name,
                            exception.Message,
                            retry);
                    }
                );
        }

        return Policy.NoOp();
    }
}