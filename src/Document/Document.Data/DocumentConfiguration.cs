using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Document.Data;

internal class DocumentConfiguration : IEntityTypeConfiguration<Domain.Document>
{
    public void Configure(EntityTypeBuilder<Domain.Document> builder)
    {
    }
}