using Microsoft.EntityFrameworkCore;
using CEP.SmartWallet.Domain.Entities;

namespace CEP.SmartWallet.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Transaction> Transactions  => Set<Transaction>();

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base (options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transaction> (entity =>
        {
            entity.HasKey(t => t.Id);
            entity.Property(t => t.FromAccount).IsRequired();
            entity.Property(t => t.ToAccount).IsRequired();
            entity.Property(t => t.Amount).HasColumnType("decimal(18,2)");
            entity.Property(t => t.Currency).HasMaxLength(10);
            entity.Property(t => t.Status).HasConversion<int>();
        });
    }
}