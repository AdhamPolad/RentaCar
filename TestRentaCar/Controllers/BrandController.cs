using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestRentaCar.Api.Controllers.Base;
using TestRentaCar.Buisness.Abstractions;
using TestRentaCar.Buisness.Dtos.Brand;
using TestRentaCarDataAccess.Model;
using TestRentaCarSln.Buisness.Dtos.Brand;

namespace TestRentaCar.Api.Controllers
{
    [Route("api/v1/brands")]
    [ApiController]
    public class BrandController : BaseController
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }
        [Authorize(Roles = "Customer,Admin")]
        [HttpGet]
        public async Task<IActionResult> All([FromQuery] PaginationRequest paginationRequest) =>
            Execute(await _brandService.GetAllAsync(paginationRequest));

        [Authorize(Roles = "Customer,Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) =>
            Execute(await _brandService.GetByIdAsync(id));

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(PostBrandDto postBrandDto) =>
            Execute(await _brandService.CreateAsync(postBrandDto));

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Update(int id, UpdateBrandDto updateBrandDto) =>
            Execute(await _brandService.UpdateAsync(id, updateBrandDto));

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) =>
            Execute(await _brandService.DeleteAsync(id));

    }
}
