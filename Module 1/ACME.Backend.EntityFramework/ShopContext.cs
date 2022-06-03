using ACME.Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace ACME.Backend.EntityFramework;
public class ShopContext : DbContext
{
    public ShopContext(DbContextOptions options): base(options)
    {
        DbInitializer.Initialize(this);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>(ba => {
            ba.Property(p => p.Timestamp).IsRowVersion();
        });
        modelBuilder.Entity<ProductGroup>(ba => {
            ba.Navigation(p => p.SpecificationDefinitions).AutoInclude();
            ba.Property(p => p.Timestamp).IsRowVersion();
        });
        modelBuilder.Entity<Product>(ba => { 
            ba.Navigation(p=>p.Brand).AutoInclude();
            ba.Property(p => p.Timestamp).IsRowVersion();
        });
        modelBuilder.Entity<Price>(ba => {
            ba.Property(p => p.Timestamp).IsRowVersion();
        });
        modelBuilder.Entity<Review>(ba => { 
            ba.Property(p => p.Timestamp).IsRowVersion();
        });
        modelBuilder.Entity<Specification>(ba => {
            ba.Property(p => p.Timestamp).IsRowVersion();
        });
        modelBuilder.Entity<SpecificationDefinition>(ba => {
            ba.Property(p => p.Timestamp).IsRowVersion();
        });
    }

    public DbSet<Brand> Brands => Set<Brand>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<ProductGroup> ProductGroups => Set<ProductGroup>();
    public DbSet<Price> Prices => Set<Price>();
    public DbSet<Review> Reviews => Set<Review>();
    public DbSet<Specification> Specifications => Set<Specification>();
    public DbSet<SpecificationDefinition> SpecificationDefinitions => Set<SpecificationDefinition>();
}
