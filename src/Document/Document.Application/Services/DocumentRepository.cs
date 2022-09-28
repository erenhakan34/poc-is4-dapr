using Core.Data;

namespace Document.Application.Services;

public class DocumentRepository:  BaseRepository<Domain.Document>, IDocumentRepository
{
    public DocumentRepository(DataContext context) : base(context)
    {
    }
}

public interface IDocumentRepository: IRepository<Domain.Document>
{
    
}