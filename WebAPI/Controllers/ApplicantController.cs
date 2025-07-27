using Business.Abstracts;
using Business.Dtos.Requests.Applicant;
using Business.Dtos.Responses.Applicant;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicantController : ControllerBase
    {
        private readonly IApplicantService _applicantService;

        public ApplicantController(IApplicantService applicantService)
        {
            _applicantService = applicantService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetApplicantResponse>> Get(int id)
        {
            var response = await _applicantService.GetByIdAsync(id);
            if (response == null) return NotFound();
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<GetApplicantResponse>> Create(CreateApplicantRequest request)
        {
            var response = await _applicantService.CreateAsync(request);
            return CreatedAtAction(nameof(Get), new { id = response.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateApplicantRequest request)
        {
            request.Id = id;
            await _applicantService.UpdateAsync(request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _applicantService.DeleteAsync(id);
            return NoContent();
        }
    }
}
