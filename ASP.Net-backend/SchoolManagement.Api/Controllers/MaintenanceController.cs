using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.Maintenance;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Api.Controllers
{
    [ApiController]
    [Route("maintenance")]
    public class MaintenanceController : ControllerBase
    {
        private readonly IMaintenanceService _maintenanceService;

        public MaintenanceController(IMaintenanceService maintenanceService)
        {
            _maintenanceService = maintenanceService;
        }

        [HttpPost]
        [Route("create")]
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<IActionResult> CreateMaintenance(MaintenanceDto maintenance)
        {
            var createdMaintenance = await _maintenanceService.CreateMaintenanceAsync(maintenance);
            return Ok(createdMaintenance);
        }

        [HttpGet]
        [Route("list")]
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<ActionResult<IEnumerable<MaintenanceResponseDto>>> ListMaintenances()
        {
            var maintenances = await _maintenanceService.ListMaintenancesAsync();
            return Ok(maintenances);
        }

        [HttpPut]
        [Route("update")]
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<ActionResult> UpdateMaintenance(MaintenanceResponseDto maintenance)
        {
            var updatedMaintenance = await _maintenanceService.UpdateMaintenanceAsync(maintenance);
            return Ok(updatedMaintenance);
        }

        [HttpDelete]
        [Route("delete")]
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<IActionResult> DeleteMaintenance(int maintenanceId)
        {
            var maintenance = await _maintenanceService.DeleteMaintenanceByIdAsync(maintenanceId);
            return Ok(maintenance);
        }
    }
}
