using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestRentaCar.Api.Controllers.Base;
using TestRentaCar.Buisness.Abstractions;
using TestRentaCar.Buisness.Dtos.Customer;
using TestRentaCarDataAccess.Enums;

namespace TestRentaCar.Api.Controllers
{
    //burda create olsun?
    [Route("api/v1/customers")]
    [Authorize(Roles = "Admin")]
    public class CustomerController : BaseController
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        [Authorize(Roles ="Admin")]
        [HttpGet]
        public async Task<IActionResult> All() =>
            Execute(await _customerService.GetAllAsync());

        [Authorize(Roles = "Admin, Customer")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) =>
            Execute(await _customerService.GetByIdAsync(id));

        [Authorize(Roles = "Admin, Customer")]
        [HttpPut]
        public async Task<IActionResult> Update(UpdateCustomerDto updateCustomerDto) =>
            Execute(await _customerService.UpdateAsync(updateCustomerDto));

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromQuery] DeleteType deleteType,[FromRoute] int id) =>
            Execute(await _customerService.DeleteAsync(deleteType, id));

    }
}
