using Document.Application.Services;
using Grpc.Core;
using GrpcService;

namespace Document.API.Services;

public class DocumentGrpcService : DocumentGrpc.DocumentGrpcBase
{
    private readonly IUserDocumentRepository _repository;

    public DocumentGrpcService(IUserDocumentRepository repository)
    {
        _repository = repository;
    }

    public async override Task<GetDocumentDataResponse> GetDocument(GetDocumentRequest request, ServerCallContext context)
    {
       var documents = await _repository.GetUserDocumentAsync(request.UserId);
       var response = new GetDocumentDataResponse();
     
       response.Items.AddRange(documents.Items.Select(d => new GetDocumentSingleResponse()
       {
           Id = d.Id.ToString(), Name = d.Name
       }).ToList());
       return response;
    }
}

