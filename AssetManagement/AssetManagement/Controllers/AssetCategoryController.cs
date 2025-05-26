using AssetManagement.Models;
using AssetManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssetCategoryController : ControllerBase
    {
        private readonly IAssetCategoryService _service;

        public AssetCategoryController(IAssetCategoryService service)
        {
            _service = service;
        }

        // GET: api/AssetCategory
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _service.GetAllAsync();
            return Ok(categories);
        }

        // GET: api/AssetCategory/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _service.GetByIdAsync(id);
            if (category == null)
                return NotFound();
            return Ok(category);
        }

        // POST: api/AssetCategory
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AssetCategory category)
        {
            if (string.IsNullOrWhiteSpace(category.CategoryName))
                return BadRequest("CategoryName is required.");

            var created = await _service.CreateAsync(category);
            return CreatedAtAction(nameof(GetById), new { id = created.AssetCategoryID }, created);
        }

        // PUT: api/AssetCategory/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AssetCategory category)
        {
            var updated = await _service.UpdateAsync(id, category);
            if (updated == null)
                return NotFound();
            return Ok(updated);
        }

        // DELETE: api/AssetCategory/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success)
                return NotFound();
            return NoContent();
        }
    }
}
