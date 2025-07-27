using Business.Abstracts;
using Business.Dtos.Requests.Bootcamp;
using Business.Dtos.Responses.Bootcamp;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BootcampController : ControllerBase
    {
        private readonly IBootcampService _bootcampService;

        public BootcampController(IBootcampService bootcampService)
        {
            _bootcampService = bootcampService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetBootcampResponse>> Get(int id)
        {
            var response = await _bootcampService.GetByIdAsync(id);
            if (response == null) return NotFound();
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<GetBootcampResponse>> Create([FromBody] CreateBootcampRequest request)
        {
            if (request == null)
                return BadRequest("Request body cannot be null.");

            var response = await _bootcampService.CreateAsync(request);
            return CreatedAtAction(nameof(Get), new { id = response.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateBootcampRequest request)
        {
            if (request == null)
                return BadRequest("Request body cannot be null.");

            await _bootcampService.UpdateAsync(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _bootcampService.DeleteAsync(id);
            return NoContent();
        }
    }
}
