namespace ACME.Backend.Interfaces;

public interface IShopUnitOfWork
{
    public IBrandRepository Brands { get; }
    public IProductRepository Products { get; }    
    public IProductGroupRepository ProductGroups { get; }
    public IPriceRepository Prices { get; }
    public IReviewRepository Reviews { get; }
    public ISpecificationRepository Specifications { get; }
    public ISpecificationDefinitionRepository SpecificationDefinitions { get; }
}
