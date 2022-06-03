using ACME.Backend.Entities;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ACME.Backend.IntegrationTests;

public class ProductApiTests: BaseApiTest<Product>
{
    public ProductApiTests(TestWebApplicationFactory<Program> factory): base(factory, "product")
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
    [InlineData(5, 1, 3)]
    public async Task Test_GetReviewsAsync(uint id, int page, int count)
    {
        var client = _factory.CreateClient();
        var respose = await client.GetAsync($"product/{id}/reviews?page={page}&count={count}");
        var stringData = await respose.Content.ReadAsStringAsync();
        var data = JsonConvert.DeserializeObject<Review[]>(stringData);

        Assert.True(respose.IsSuccessStatusCode);
        Assert.NotNull(data);
        Assert.IsType<Product[]>(data);
        Assert.True(data?.Length == count);
    }
    [Theory]
    [InlineData(5, 1, 3)]
    public async Task Test_GetPricesAsync(uint id, int page, int count)
    {
        var client = _factory.CreateClient();
        var respose = await client.GetAsync($"product/{id}/prices?page={page}&count={count}");
        var stringData = await respose.Content.ReadAsStringAsync();
        var data = JsonConvert.DeserializeObject<Price[]>(stringData);

        Assert.True(respose.IsSuccessStatusCode);
        Assert.NotNull(data);
        Assert.IsType<Price[]>(data);
        Assert.True(data?.Length == count);
    }
    [Theory]
    [InlineData(5, 1, 3)]
    public async Task Test_GetSpecificationsAsync(uint id, int page, int count)
    {
        var client = _factory.CreateClient();
        var respose = await client.GetAsync($"product/{id}/specifications?page={page}&count={count}");
        var stringData = await respose.Content.ReadAsStringAsync();
        var data = JsonConvert.DeserializeObject<Specification[]>(stringData);

        Assert.True(respose.IsSuccessStatusCode);
        Assert.NotNull(data);
        Assert.IsType<Specification[]>(data);
        Assert.True(data?.Length == count);
    }
    [Fact]
    public async Task Test_PostAsync()
    {
        var entity = new Product { Name = "Test" };
        var client = _factory.CreateClient();
        var respose = await client.PostAsync($"product", 
            new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json"));
        var location = respose.Headers.Location;

        Assert.True(respose.StatusCode == HttpStatusCode.Created);
        Assert.NotNull(location);   
    }
    [Theory]
    [InlineData(2)]
    public async Task Test_PutAsync(uint id)
    {
        var client = _factory.CreateClient();
        var respose = await client.GetAsync($"product/{id}");
        var stringData = await respose.Content.ReadAsStringAsync();
        var entity = JsonConvert.DeserializeObject<Product>(stringData);
        entity!.Name = "Test";

        respose = await client.PutAsync($"brand/{id}",
            new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json"));
        
        Assert.True(respose.StatusCode == HttpStatusCode.OK);
    }
    [Theory]
    [InlineData(1)]
    public override async Task Test_DeleteAsync(uint id)
    {
        await base.Test_DeleteAsync(id);
        
    }
}