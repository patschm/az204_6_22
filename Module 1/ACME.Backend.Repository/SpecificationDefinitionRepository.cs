using ACME.Backend.Entities;
using ACME.Backend.EntityFramework;
using ACME.Backend.Interfaces;
using Microsoft.Extensions.Logging;

namespace ACME.Backend.Repository;
public class SpecificationDefinitionRepository: Repository<SpecificationDefinition>, ISpecificationDefinitionRepository
{
    public SpecificationDefinitionRepository(ShopContext context, ILogger logger): base(context, logger)
    {
        
    }
}
