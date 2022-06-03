using ACME.Backend.Entities;
using ACME.Backend.EntityFramework;
using ACME.Backend.Interfaces;
using Microsoft.Extensions.Logging;

namespace ACME.Backend.Repository;
public class BrandRepository: Repository<Brand>, IBrandRepository
{
    public BrandRepository(ShopContext context, ILogger logger): base(context, logger)
    {
        
    }
}
