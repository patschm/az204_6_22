using ACME.Backend.Entities;
using ACME.Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ACME.Backend.Services.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class BrandController : BaseController
{
    private readonly IBrandRepository _repository;

    public BrandController(IShopUnitOfWork unitOfWork) : base(unitOfWork)
    {
        _repository = _unitOfWork.Brands;
    }

    [HttpGet]
    public async Task<IEnumerable<Brand>> Get(int page = 1, int count = 10)
    {
        return await _repository.GetPagedAsync((page - 1) * count, count);
    }
    [HttpGet("{id}")]
    public async Task<Brand?> Get(uint id)
    {
        return await _repository.GetAsync(id);
    }
    [HttpGet("{id}/products")]
    public async Task<IEnumerable<Product>> GetProducts(uint id, int page = 1, int count = 10)
    {
        return await _unitOfWork.Products.FindAsync(pg => pg.BrandID == id, (page - 1) * count, count);
    }
    [HttpPost]
    public async Task<IActionResult> Post([FromBody]Brand entity)
    {
        var result  = await _repository.InsertAsync(entity);
        return CreatedAtAction(nameof(Get), new { id= result.ID});
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(uint id, [FromBody]Brand entity)
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
