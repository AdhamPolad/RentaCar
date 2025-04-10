using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestRentaCar.Api.Controllers.Base;
using TestRentaCar.Buisness.Abstractions;
using TestRentaCar.Buisness.Dtos.Review;
using TestRentaCarDataAccess.Enums;
using TestRentaCarDataAccess.Model;

namespace TestRentaCar.Api.Controllers
{
    [Route("api/v1/reviews")]
    [Authorize(Roles ="Admin, Customer")]
    public class ReviewController : BaseController
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostReviewDto postReviewDto) =>
            Execute(await _reviewService.CreateAsync(postReviewDto));

        [HttpGet]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> All([FromQuery] PaginationRequest paginationRequest, int? carID, ReviewRating? reviewRating) =>
            Execute(await _reviewService.GetAllAsync(paginationRequest, carID, reviewRating));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) =>
            Execute(await _reviewService.GetById(id));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id,[FromQuery] DeleteType deleteType) =>
            Execute(await _reviewService.DeleteAsync(id, deleteType));

        [HttpPut]
        public async Task<IActionResult> Update(UpdateReviewDto updateReviewDto) =>
            Execute(await _reviewService.UpdateAsync(updateReviewDto));
    }
}
