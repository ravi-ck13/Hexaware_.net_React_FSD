using AssetManagement.Models;
using AssetManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginHistoryController : ControllerBase
    {
        private readonly ILoginHistoryService _service;

        public LoginHistoryController(ILoginHistoryService service)
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
            var login = await _service.GetByIdAsync(id);
            return login == null ? NotFound() : Ok(login);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LoginHistory login)
        {
            var created = await _service.CreateAsync(login);
            return CreatedAtAction(nameof(GetById), new { id = created.LoginID }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] LoginHistory login)
        {
            var updated = await _service.UpdateAsync(id, login);
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
