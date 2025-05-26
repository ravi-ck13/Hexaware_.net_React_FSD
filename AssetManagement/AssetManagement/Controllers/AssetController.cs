using AssetManagement.Models;
using AssetManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssetController : ControllerBase
    {
        private readonly IAssetService _assetService;

        public AssetController(IAssetService assetService)
        {
            _assetService = assetService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _assetService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var asset = await _assetService.GetByIdAsync(id);
            return asset == null ? NotFound() : Ok(asset);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Asset asset)
        {
            var created = await _assetService.CreateAsync(asset);
            return CreatedAtAction(nameof(GetById), new { id = created.AssetID }, created);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Asset asset)
        {
            var updated = await _assetService.UpdateAsync(id, asset);
            return updated == null ? NotFound() : Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _assetService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
