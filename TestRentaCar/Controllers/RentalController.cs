using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestRentaCar.Api.Controllers.Base;
using TestRentaCar.Buisness.Abstractions;
using TestRentaCar.Buisness.Dtos.Rental;
using TestRentaCarDataAccess.Enums;
using TestRentaCarDataAccess.Model;

namespace TestRentaCar.Api.Controllers
{
    [Route("api/v1/rentals")]
    public class RentalController : BaseController
    {
        private readonly IRentalService _rentalService;
        public RentalController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }
        [Authorize(Roles = "Customer, Admin")]
        [HttpPost]
        public async Task<IActionResult> RentCar(PostRentalDto postRentalDto) =>
            Execute(await _rentalService.RentCar(postRentalDto));

        [Authorize(Roles = "Admin")]
        [HttpPut("return-car")]
        public async Task<IActionResult> ReturnCar(int rentalId) =>
            Execute(await _rentalService.ReturnCar(rentalId));

        [Authorize(Roles ="Customer")]
        [HttpPut("cancel-rental/{rentalId}")]
        public async Task<IActionResult> CancelRental(int rentalId) =>
            Execute(await _rentalService.CancelRental(rentalId));

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> All([FromQuery] PaginationRequest paginationRequest, int? carId, int? customerId, RentalStatus? rentalStatus) =>
            Execute(await _rentalService.GetAllAsync(paginationRequest, carId, customerId, rentalStatus));

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromQuery] DeleteType deleteType, [FromRoute] int id) =>
            Execute(await _rentalService.DeleteAsync(deleteType, id));

        [Authorize(Roles = "Admin, Customer")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) =>
            Execute(await _rentalService.GetByIdAsync(id));

        [Authorize(Roles = "Admin, Customer")]
        [HttpPut]
        public async Task<IActionResult> Update(UpdateRentalDto updateRentalDto) =>
            Execute(await _rentalService.UpdateAsync(updateRentalDto));
    }
}
