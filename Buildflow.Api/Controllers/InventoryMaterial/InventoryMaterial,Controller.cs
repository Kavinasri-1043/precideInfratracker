using Buildflow.Library.Services.Interfaces;
using Buildflow.Service.Service.InventoryMaterial;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Buildflow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialStatusController : ControllerBase
    {
        private readonly IInventoryMaterialService _service;

        public MaterialStatusController(IInventoryMaterialService service)
        {
            _service = service;
        }

        [HttpGet("status")]
        public async Task<IActionResult> GetMaterialStatus()
        {
            var result = await _service.GetMaterialStatusAsync();
            return Ok(result);
        }
    }
}
