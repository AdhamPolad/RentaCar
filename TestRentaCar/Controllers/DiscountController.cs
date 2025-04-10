using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestRentaCar.Api.Controllers.Base;
using TestRentaCar.Buisness.Abstractions;
using TestRentaCar.Buisness.Dtos.Discount;
using TestRentaCarDataAccess.Enums;
using TestRentaCarDataAccess.Model;

namespace TestRentaCar.Api.Controllers
{
    [Route("api/v1/discounts")]
    public class DiscountController : BaseController
    {
        private readonly IDiscountService _discountService;

        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(PostDiscountDto postDiscountDto) =>
            Execute(await _discountService.CreateAsync(postDiscountDto));

        [Authorize(Roles = "Customer")]
        [HttpPatch("apply")]
        public async Task<IActionResult> ApplyAsync(string code, int rentalId) =>
            Execute(await _discountService.ApplyAsync(code, rentalId));

        [Authorize(Roles = "Customer,Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) =>
            Execute(await _discountService.GetByIdAsync(id));

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationRequest paginationRequest, bool? isActive) =>
            Execute(await _discountService.GetAllAsync(paginationRequest, isActive));

        [Authorize(Roles = "Admin")]
        [HttpPut("deactive-discount")]
        public async Task<IActionResult> DeActiveDiscount(int id) =>
            Execute(await _discountService.DeActiveDiscount(id));

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) =>
            Execute(await _discountService.DeleteAsync(id));

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Update(UpdateDiscountDto updateDiscountDto) =>
            Execute(await _discountService.UpdateAsync(updateDiscountDto));

    }
}
