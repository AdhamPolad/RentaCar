using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestRentaCar.Api.Controllers.Base;
using TestRentaCar.Buisness.Abstractions;
using TestRentaCar.Buisness.Dtos.Maintenance;
using TestRentaCarDataAccess.Enums;
using TestRentaCarDataAccess.Model;

namespace TestRentaCar.Api.Controllers
{
    [Route("api/v1/maintenances")]
    [Authorize(Roles = "Admin, Customer")]
    [ApiController]
    public class MaintenanceController : BaseController
    {
        private readonly IMaintenanceService _maintenanceService;

        public MaintenanceController(IMaintenanceService maintenanceService)
        {
            _maintenanceService = maintenanceService;
        }

        [HttpGet]
        public async Task<IActionResult> GeltAllAsync([FromQuery] PaginationRequest paginationRequest, int? rentalId, DateTime minMaintenanceDate, DateTime maxMaintenanceDate) =>
            Execute(await _maintenanceService.GetAllAsync(paginationRequest, rentalId, minMaintenanceDate, maxMaintenanceDate));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id) =>
            Execute(await _maintenanceService.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> CreateAsync(PostMaintenanceDto postMaintenanceDto) =>
            Execute(await _maintenanceService.CreateAsync(postMaintenanceDto));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromQuery] DeleteType deleteType, int id) =>
            Execute(await _maintenanceService.DeleteAsync(deleteType, id));

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UpdateMaintenanceDto updateMaintenanceDto) =>
            Execute(await _maintenanceService.UpdateAsync(updateMaintenanceDto));

    }
}
