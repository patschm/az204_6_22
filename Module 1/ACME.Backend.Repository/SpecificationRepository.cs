using ACME.Backend.Entities;
using ACME.Backend.EntityFramework;
using ACME.Backend.Interfaces;
using Microsoft.Extensions.Logging;

namespace ACME.Backend.Repository;
public class SpecificationRepository: Repository<Specification>, ISpecificationRepository
{
    public SpecificationRepository(ShopContext context, ILogger logger): base(context, logger)
    {
        
    }
}