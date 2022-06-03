using ACME.Backend.Entities;
using ACME.Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ACME.Backend.Services.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : BaseController
{
    private readonly IProductRepository _repository;

    public ProductController(IShopUnitOfWork unitOfWork) : base(unitOfWork)
    {
        _repository = _unitOfWork.Products;
    }

    [HttpGet]
    public async Task<IEnumerable<Product>> Get(int page = 1, int count = 10)
    {
        return await _repository.GetPagedAsync((page - 1) * count, count);
    }
    [HttpGet("{id}")]
    public async Task<Product?> Get(uint id)
    {
        return await _repository.GetAsync(id);
    }
    [HttpGet("{id}/reviews")]
    public async Task<IEnumerable<Review>> GetReviews(uint id, int page = 1, int count = 10)
    {
        return await _unitOfWork.Reviews.FindAsync(r => r.ID == id, (page - 1) * count, count);
    }
    [HttpGet("{id}/prices")]
    public async Task<IEnumerable<Price>> GetPrices(uint id, int page = 1, int count = 100)
    {
        return await _unitOfWork.Prices.FindAsync(r => r.ID == id, (page - 1) * count, count);
    }
    [HttpGet("{id}/specifications")]
    public async Task<IEnumerable<Specification>> GetSpecifications(uint id, int page = 1, int count = 100)
    {
        return await _unitOfWork.Specifications.FindAsync(r => r.ID == id, (page - 1) * count, count);
    }
    [HttpPost]
    public async Task<IActionResult> Post([FromBody]Product entity)
    {
        var result  = await _repository.InsertAsync(entity);
        return CreatedAtAction(nameof(Get), new { id= result});
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(uint id, [FromBody]Product entity)
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
