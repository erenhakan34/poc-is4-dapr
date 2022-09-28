using Core.Data;
using Microsoft.EntityFrameworkCore;

namespace Document.Data;

public class DocumentDataContext :DataContext
{
    public DocumentDataContext(DbContextOptions options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
     
       modelBuilder.ApplyConfiguration(new DocumentConfiguration());
    }
    public DbSet<Domain.Document> Documents { get; set; }
}