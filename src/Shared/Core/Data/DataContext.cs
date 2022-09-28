using Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Core.Data;

public abstract class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public override int SaveChanges()
    {
        return base.SaveChanges();
    }
   /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseInMemoryDatabase("in-memory");
        }
        base.OnConfiguring(optionsBuilder);
    }*/

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        return await base.SaveChangesAsync(cancellationToken);
    }

   

}