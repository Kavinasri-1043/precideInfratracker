using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Buildflow.Library.Services.Interfaces;

namespace Buildflow.Api.Controllers.MaterialController
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialController : ControllerBase
    {
        private readonly MaterialService _service;

        public MaterialController(MaterialService service)
        {
            _service = service;
        }

        [HttpGet("project/{projectId}")]
        public async Task<IActionResult> GetMaterialsByProjectId(int projectId)
        {
            var materials = await _service.GetMaterialsByProjectAsync(projectId);

            if (materials == null || !materials.Any())
                return NotFound(new { message = "No materials found for this project." });

            return Ok(materials);
        }
          [HttpGet("project/{projectId}/alerts")]
        public async Task<IActionResult> GetLowStockAlerts(int projectId)
        {
            var alerts = await _service.GetLowStockAlertsAsync(projectId);
            return Ok(alerts);
        }

    }
}
