using ACME.Backend.Entities;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ACME.Backend.IntegrationTests;

public class BrandApiTests: BaseApiTest<Brand>
{
    public BrandApiTests(TestWebApplicationFactory<Program> factory): base(factory, "brand")
    {
    }

    [Theory]
    [InlineData(1, 12)]
    public override async Task Test_GetPagedAsync(int page, int count)
    {
        await base.Test_GetPagedAsync(page, count);
    }

    [Theory]
    [InlineData(5)]
    public override async Task Test_GetAsync(uint id)
    {
        await base.Test_GetAsync(id);
    }
    [Theory]
    [InlineData(5, 1, 4)]
    public async Task Test_GetProductsAsync(uint id, int page, int count)
    {
        var client = _factory.CreateClient();
        var respose = await client.GetAsync($"brand/{id}/products?page={page}&count={count}");
        var stringData = await respose.Content.ReadAsStringAsync();
        var data = JsonConvert.DeserializeObject<Product[]>(stringData);

        Assert.True(respose.IsSuccessStatusCode);
        Assert.NotNull(data);
        Assert.IsType<Product[]>(data);
        Assert.True(data?.Length == count);
    }
    [Fact]
    public async Task Test_PostAsync()
    {
        var brand = new Brand { Name = "Test", Website = "test.com" };
        var client = _factory.CreateClient();
        var respose = await client.PostAsync($"brand", 
            new StringContent(JsonConvert.SerializeObject(brand), Encoding.UTF8, "application/json"));
        var location = respose.Headers.Location;

        Assert.True(respose.StatusCode == HttpStatusCode.Created);
        Assert.NotNull(location);   
    }
    [Theory]
    [InlineData(2)]
    public async Task Test_PutAsync(uint id)
    {
        var client = _factory.CreateClient();
        var respose = await client.GetAsync($"brand/{id}");
        var stringData = await respose.Content.ReadAsStringAsync();
        var brand = JsonConvert.DeserializeObject<Brand>(stringData);
        brand!.Name = "Test";

        respose = await client.PutAsync($"brand/{id}",
            new StringContent(JsonConvert.SerializeObject(brand), Encoding.UTF8, "application/json"));
        
        Assert.True(respose.StatusCode == HttpStatusCode.OK);
    }
    [Theory]
    [InlineData(1)]
    public override async Task Test_DeleteAsync(uint id)
    {
        await base.Test_DeleteAsync(id);
    }
}