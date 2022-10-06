using Grpc.Core;
using Grpc.Net.Client;
using GrpcService;

namespace Auth.API.Services;

public class DocumentRequestGrpcService : IDocumentRequestGrpcService
{
    private readonly IConfiguration _configuration;
    public DocumentRequestGrpcService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task<GetDocumentDataResponse> GetDocument(string userId)
    {
        var channel = GrpcChannel.ForAddress(_configuration.GetValue<string>("DocumentGrpcUrl"));
        var client = new DocumentGrpc.DocumentGrpcClient(channel);
        var metadata = new Metadata() { { "dapr-app-id", "documents" } };
        var document = await client.GetDocumentAsync(new GetDocumentRequest()
        {
            UserId = userId
        }, metadata);
        return document;
    }
}

public interface IDocumentRequestGrpcService
{
    Task<GetDocumentDataResponse> GetDocument(string userId);
}