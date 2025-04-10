using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestRentaCar.Api.Controllers.Base;
using TestRentaCar.Buisness.Abstractions;
using TestRentaCar.Buisness.Dtos.Insurance;
using TestRentaCarDataAccess.Enums;
using TestRentaCarDataAccess.Model;

namespace TestRentaCar.Api.Controllers
{
    [Route("api/v1/insurances")]
    public class InsuranceController : BaseController
    {
        private readonly IInsuranceService _insuranceService;

        public InsuranceController(IInsuranceService insuranceService)
        {
            _insuranceService = insuranceService;
        }
        [Authorize(Roles = "Admin, Customer")]
        [HttpGet]
        public async Task<IActionResult> All([FromQuery] PaginationRequest paginationRequest, CarInsuranceType carInsuranceType, int minPrice, int maxPrice) =>
            Execute(await _insuranceService.GetAllAsync(paginationRequest, carInsuranceType, minPrice, maxPrice));

        [Authorize(Roles = "Admin, Customer")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) =>
            Execute(await _insuranceService.GetById(id));

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(PostInsuranceDto postInsuranceDto) =>
            Execute(await _insuranceService.CreateAsync(postInsuranceDto));

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromQuery] DeleteType deleteType,[FromRoute] int id) =>
            Execute(await _insuranceService.DeleteAsync(id, deleteType));

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Update(UpdateInsuranceDto updateInsuranceDto) =>
            Execute(await _insuranceService.UpdateAsync(updateInsuranceDto));
    }
}
