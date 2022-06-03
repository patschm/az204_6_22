using ACME.Backend.Entities;
using ACME.Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ACME.Backend.Services.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PriceController : BaseController
{
    private readonly IPriceRepository _repository;

    public PriceController(IShopUnitOfWork unitOfWork) : base(unitOfWork)
    {
        _repository = _unitOfWork.Prices;
    }

    [HttpGet]
    public async Task<IEnumerable<Price>> Get(int page = 1, int count = 10)
    {
        var data = await _repository.GetPagedAsync((page - 1) * count, count);
        return data;
    }
    [HttpGet("{id}")]
    public async Task<Price?> Get(uint id)
    {
        return await _repository.GetAsync(id);
    }
    [HttpPost]
    public async Task<IActionResult> Post([FromBody]Price entity)
    {
        var result  = await _repository.InsertAsync(entity);
        return CreatedAtAction(nameof(Get), new { id= result});
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(uint id, [FromBody]Price entity)
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
