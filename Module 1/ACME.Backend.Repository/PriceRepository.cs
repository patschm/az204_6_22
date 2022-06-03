using ACME.Backend.Entities;
using ACME.Backend.EntityFramework;
using ACME.Backend.Interfaces;
using Microsoft.Extensions.Logging;

namespace ACME.Backend.Repository;

public class PriceRepository: Repository<Price>, IPriceRepository
{
    public PriceRepository(ShopContext context, ILogger logger): base(context, logger)
    {
        
    }
}

