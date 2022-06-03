using ACME.Backend.Entities;

namespace ACME.Backend.EntityFramework;

public class DbInitializer
{
    public static void Initialize(ShopContext ctx)
    {
        if (ctx.Database.EnsureCreated())
        {
            Seed(ctx);
        }
    }
    private static void Seed(ShopContext ctx)
    {
        ctx.Brands.AddRangeAsync(TestData_Brand.TestData());
        ctx.SaveChanges();
        ctx.ProductGroups.AddRangeAsync(TestData_ProductGroup.TestData());
        ctx.SaveChanges();
        ctx.Products.AddRangeAsync(TestData_Product.TestData());
        ctx.SaveChanges();
        ctx.SpecificationDefinitions.AddRangeAsync(TestData_SpecificationDefinition.TestData());
        ctx.SaveChanges();
        ctx.Specifications.AddRangeAsync(TestData_Specification.TestData());
        ctx.SaveChanges();
        ctx.Reviews.AddRangeAsync(TestData_Review.TestData());
        ctx.SaveChanges();
        ctx.Prices.AddRangeAsync(TestData_Price.TestData());
        ctx.SaveChanges();
    }
}
