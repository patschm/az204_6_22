using System.Threading.Tasks;
using ACME.Backend.Repository.InMemory;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace ACME.Backend.Repository.Tests;

public class BrandRepositorrTests
{
    [Fact]
    public async Task Test_GetPagedAsync()
    {
        var repo = new ShopUnitOfWork(NullLogger<ShopUnitOfWork>.Instance);
        var result = await repo.Brands.GetPagedAsync(1, 12);
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.True(result.Count == 12);
    }
}