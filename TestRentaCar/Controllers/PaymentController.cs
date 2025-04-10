using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestRentaCar.Api.Controllers.Base;
using TestRentaCar.Buisness.Abstractions;
using TestRentaCar.Buisness.Dtos.Payment;
using TestRentaCarDataAccess.Enums;
using TestRentaCarDataAccess.Model;

namespace TestRentaCar.Api.Controllers
{
    [Route("api/v1/payments")]
    [Authorize(Roles = "Admin")]
    public class PaymentController : BaseController
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [Authorize(Roles = "Admin, Customer")]
        [HttpGet]
        public async Task<IActionResult> All([FromQuery] PaginationRequest paginationRequest, PaymentStatus? paymentStatus, [FromQuery] PaymentFilterDto paymentFilterDto) =>
            Execute(await _paymentService.GetAllAsync(paginationRequest, paymentStatus, paymentFilterDto));

        [Authorize(Roles = "Admin, Customer")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) =>
            Execute(await _paymentService.GetById(id));

        [HttpPut("confirm-payment")]
        public async Task<IActionResult> ConfirmPayment(int paymentId) =>
            Execute(await _paymentService.ConfirmPaymentAsync(paymentId));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromQuery] DeleteType deleteType,[FromRoute] int id) =>
            Execute(await _paymentService.DeleteAsync(deleteType, id));

        [HttpPut]
        public async Task<IActionResult> Update(UpdatePaymentDto updatePaymentDto) =>
            Execute(await _paymentService.UpdateAsync(updatePaymentDto));

    }
}
