using ACME.Backend.Entities;
using ACME.Backend.EntityFramework;
using ACME.Backend.Interfaces;
using Microsoft.Extensions.Logging;

namespace ACME.Backend.Repository;
public class ReviewRepository: Repository<Review>, IReviewRepository
{
    public ReviewRepository(ShopContext context, ILogger logger): base(context, logger)
    {
        
    }
}