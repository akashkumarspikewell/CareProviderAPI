using CareProviderAPI.Data.DTOs;
using CareProviderAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CareProviderAPI.Controllers
{
    [ApiController]
    [Route("api/v1/providers")]
    public class CareProvidersController:ControllerBase
    {
        private readonly ICareProviderService _service;

        public CareProvidersController(ICareProviderService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProviderDto>>> GetAllProviders()
        {
            var providers = await _service.GetAllProvidersAsync();
            return Ok(providers);
        }

        [HttpPost]
        public async Task<IActionResult> AddProvider([FromBody] AddCareProviderDto providerDto)
        {
            try
            {
                await _service.AddProviderAsync(providerDto);
                return Ok("Provider added successfully.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("inactive")]
        public async Task<IActionResult> GetInactive()
        {
            List<InactiveProviderDto> providers = await _service.GetInactiveProvidersAsync();
            return Ok(providers);
        }

        [HttpGet("department/{departmentId}")]
        public async Task<IActionResult> GetByDepartment(int departmentId)
        {
            List<ProviderDto> providers = await _service.GetProvidersByDepartmentAsync(departmentId);
            return Ok(providers);
        }

        [HttpGet("experience/{minYears}")]
        public async Task<IActionResult> GetByExperience(int minYears)
        {
            List<ProviderDto> providers = await _service.GetByMinExperienceAsync(minYears);
            return Ok(providers);
        }
    }
}
