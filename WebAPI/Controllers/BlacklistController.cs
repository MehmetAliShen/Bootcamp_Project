using Business.Abstracts;
using Business.Dtos.Requests.Blacklist;
using Business.Dtos.Responses.Blacklist;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlacklistController : ControllerBase
    {
        private readonly IBlacklistService _blacklistService;

        public BlacklistController(IBlacklistService blacklistService)
        {
            _blacklistService = blacklistService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetBlacklistResponse>> Get(int id)
        {
            var response = await _blacklistService.GetByIdAsync(id);
            if (response == null) return NotFound();
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<GetBlacklistResponse>> Create(CreateBlacklistRequest request)
        {
            var response = await _blacklistService.CreateAsync(request);
            return CreatedAtAction(nameof(Get), new { id = response.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateBlacklistRequest request)
        {
            await _blacklistService.UpdateAsync(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _blacklistService.DeleteAsync(id);
            return NoContent();
        }
    }
}
