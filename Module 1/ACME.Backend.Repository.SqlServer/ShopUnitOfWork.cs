
using ACME.Backend.EntityFramework;
using ACME.Backend.Interfaces;
using Microsoft.Extensions.Logging;

namespace ACME.Backend.Repository.SqlServer;

public class ShopUnitOfWork : IShopUnitOfWork
{
    private ShopContext _context;
    private ILogger<ShopUnitOfWork> _logger;

    public ShopUnitOfWork(ShopContext context, ILogger<ShopUnitOfWork> logger)
    {
        _context = context;
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
