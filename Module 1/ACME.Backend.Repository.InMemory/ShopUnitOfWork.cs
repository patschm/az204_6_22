
using ACME.Backend.EntityFramework;
using ACME.Backend.Interfaces;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ACME.Backend.Repository.InMemory;

public class ShopUnitOfWork : IShopUnitOfWork
{
    private ShopContext _context;
    private ILogger<ShopUnitOfWork> _logger;

    private static SqliteConnection CreateInMemoryDatabase()
    {
        var connection = new SqliteConnection($"Data Source=:memory:");
        connection.Open();
        return connection;
    }
    protected ShopContext CreateContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<ShopContext>();
        optionsBuilder.UseSqlite(CreateInMemoryDatabase());
        return new ShopContext(optionsBuilder.Options);
    }
    public ShopUnitOfWork(ILogger<ShopUnitOfWork> logger)
    {
        _context = CreateContext();
        _logger = logger;
    }

    public IBrandRepository Brands => new BrandRepository(_context, _logger);

    public IProductRepository Products => new ProductRepository(_context, _logger);

    public IProductGroupRepository ProductGroups => new ProductGroupRepository(_context, _logger);

    public IPriceRepository Prices => new PriceRepository(_context, _logger);

    public IReviewRepository Reviews => new ReviewRepository(_context, _logger);

    public ISpecificationRepository Specifications => new SpecificationRepository(_context, _logger);

    public ISpecificationDefinitionRepository SpecificationDefinitions => new SpecificationDefinitionRepository(_context, _logger);
}
