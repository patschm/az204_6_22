using ACME.Backend.Entities;
using ACME.Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ACME.Backend.Services.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductGroupController : BaseController
{
    private readonly IProductGroupRepository _repository;

    public ProductGroupController(IShopUnitOfWork unitOfWork) : base(unitOfWork)
    {
        _repository = _unitOfWork.ProductGroups;
    }

    [HttpGet]
    public async Task<IEnumerable<ProductGroup>> Get(int page = 1, int count = 10)
    {
        return await _repository.GetPagedAsync((page - 1) * count, count);
    }
    [HttpGet("{id}")]
    public async Task<ProductGroup?> Get(uint id)
    {
        return await _repository.GetAsync(id);
    }
    [HttpGet("{id}/products")]
    public async Task<IEnumerable<Product>> GetProducts(uint id, int page = 1, int count = 10)
    {
        return await _unitOfWork.Products.FindAsync(pg => pg.ID == id, (page - 1)*count, count);
    }
    [HttpPost]
    public async Task<IActionResult> Post([FromBody]ProductGroup entity)
    {
        var result  = await _repository.InsertAsync(entity);
        return CreatedAtAction(nameof(Get), new { id= result});
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(uint id, [FromBody]ProductGroup entity)
    {
        var result = await _repository.UpdateAsync(id, entity);
        return Ok(result);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(uint id)
    {
        await _repository.DeleteAsync(id);
        return Accepted();
    }
}
