using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestRentaCar.Api.Controllers.Base;
using TestRentaCar.Buisness.Abstractions;
using TestRentaCar.Buisness.Dtos.Branch;
using TestRentaCarDataAccess.Enums;
using TestRentaCarDataAccess.Model;

namespace TestRentaCar.Api.Controllers
{
    [Route("api/v1/branchs")]
    [Authorize(Roles = "Admin")]    
    public class BranchController : BaseController
    {
        private readonly IBranchService _branchService;

        public BranchController(IBranchService branchService)
        {
            _branchService = branchService;
        }
        [Authorize(Roles = "Customer, Admin")]
        [HttpGet]
        public async Task<IActionResult> All([FromQuery] PaginationRequest paginationRequest) =>
            Execute(await _branchService.GetAllAsync(paginationRequest));

        [Authorize(Roles = "Customer, Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) =>
            Execute(await _branchService.GetByIdAsync(id));


        [HttpPost]
        public async Task<IActionResult> Create(PostBranchDto postBranchDto) =>
            Execute(await _branchService.CreateAsync(postBranchDto));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromQuery] DeleteType deleteType, [FromRoute] int id) =>
            Execute(await _branchService.DeleteAsync(deleteType, id));

        [HttpPut]
        public async Task<IActionResult> Update(UpdateBranchDto updateBranchDto) =>
            Execute(await _branchService.UpdateAsync(updateBranchDto));

    }
}
