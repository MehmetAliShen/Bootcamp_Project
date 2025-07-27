using Business.Abstracts;
using Business.Dtos.Requests.Instructor;
using Business.Dtos.Responses.Instructor;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InstructorController : ControllerBase
    {
        private readonly IInstructorService _instructorService;

        public InstructorController(IInstructorService instructorService)
        {
            _instructorService = instructorService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetInstructorResponse>> Get(int id)
        {
            var response = await _instructorService.GetByIdAsync(id);
            if (response == null) return NotFound();
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<GetInstructorResponse>> Create(CreateInstructorRequest request)
        {
            var response = await _instructorService.CreateAsync(request);
            return CreatedAtAction(nameof(Get), new { id = response.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateInstructorRequest request)
        {
            await _instructorService.UpdateAsync(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _instructorService.DeleteAsync(id);
            return NoContent();
        }
    }
}
