using ACME.Backend.Entities;
using ACME.Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ACME.Backend.Services.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class SpecificationDefinitionController : BaseController
{
    private readonly ISpecificationDefinitionRepository _repository;

    public SpecificationDefinitionController(IShopUnitOfWork unitOfWork) : base(unitOfWork)
    {
        _repository = _unitOfWork.SpecificationDefinitions;
    }

    [HttpGet]
    public async Task<IEnumerable<SpecificationDefinition>> Get(int page = 1, int count = 10)
    {
        return await _repository.GetPagedAsync((page - 1) * count, count);
    }
    [HttpGet("{id}")]
    public async Task<SpecificationDefinition?> Get(uint id)
    {
        return await _repository.GetAsync(id);
    }
    [HttpPost]
    public async Task<IActionResult> Post([FromBody]SpecificationDefinition entity)
    {
        var result  = await _repository.InsertAsync(entity);
        return CreatedAtAction(nameof(Get), new { id= result});
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(uint id, [FromBody]SpecificationDefinition entity)
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
