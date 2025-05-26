using AssetManagement.Models;
using AssetManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeAssetAllocationController : ControllerBase
    {
        private readonly IEmployeeAssetAllocationService _service;

        public EmployeeAssetAllocationController(IEmployeeAssetAllocationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var allocation = await _service.GetByIdAsync(id);
            return allocation == null ? NotFound() : Ok(allocation);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeAssetAllocation allocation)
        {
            var created = await _service.CreateAsync(allocation);
            return CreatedAtAction(nameof(GetById), new { id = created.AllocationID }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, EmployeeAssetAllocation allocation)
        {
            var updated = await _service.UpdateAsync(id, allocation);
            return updated == null ? NotFound() : Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
