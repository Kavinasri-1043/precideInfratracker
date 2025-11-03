using Buildflow.Infrastructure.Entities;
using Buildflow.Library.UOW;

using Buildflow.Service.Service.Inventory;
using Buildflow.Utility.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Buildflow.Api.Controllers.Inventory
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;
        private readonly IConfiguration _config;
        private readonly IUnitOfWork _unitOfWork;

        public InventoryController(IInventoryService inventoryService, IConfiguration config, IUnitOfWork unitOfWork)
        {
            _inventoryService = inventoryService;
            _config = config;
            _unitOfWork = unitOfWork;
        }
        [HttpGet("project-team/{projectId}")]
        public async Task<IActionResult> GetProjectTeamMembers(int projectId)
        {
            var members = await _inventoryService.GetProjectTeamMembersAsync(projectId);
            return Ok(members);
        }

        [HttpPost("stock-inward")]
        public async Task<IActionResult> CreateStockInward([FromBody] StockInwardDto dto)
        {
            if (dto == null) return BadRequest("Invalid stock inward data.");
            var result = await _inventoryService.CreateStockInwardAsync(dto);
            return Ok(result);
        }

        [HttpPost("stock-outward")]
        public async Task<IActionResult> CreateStockOutward([FromBody] StockOutwardDto dto)
        {
            if (dto == null) return BadRequest("Invalid stock outward data.");
            var result = await _inventoryService.CreateStockOutwardAsync(dto);
            return Ok(result);
        }
    }
}
