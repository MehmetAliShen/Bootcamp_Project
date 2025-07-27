using Business.Abstracts;
using Business.Dtos.Requests.Application;
using Business.Dtos.Responses.Application;
using Microsoft.AspNetCore.Mvc;


namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public ApplicationController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetApplicationResponse>> Get(int id)
        {
            var response = await _applicationService.GetByIdAsync(id);
            if (response == null) return NotFound();
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<GetApplicationResponse>> Create(CreateApplicationRequest request)
        {
            var response = await _applicationService.CreateAsync(request);
            return CreatedAtAction(nameof(Get), new { id = response.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateApplicationRequest request)
        {
            await _applicationService.UpdateAsync(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _applicationService.DeleteAsync(id);
            return NoContent();
        }
    }
}