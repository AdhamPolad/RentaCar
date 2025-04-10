using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestRentaCar.Api.Controllers.Base;
using TestRentaCar.Buisness.Abstractions;
using TestRentaCar.Buisness.Dtos.Model;
using TestRentaCarDataAccess.Model;

namespace TestRentaCar.Api.Controllers
{
    [Route("api/v1/models")]
    public class ModelController : BaseController
    {
        private readonly IModelService _modelService;

        public ModelController(IModelService modelService)
        {
            _modelService = modelService;
        }

        [HttpGet("get-modelscount-by-brand")]
        public async Task<IActionResult> GetModelsCountByBrand() =>
             Execute(await _modelService.GetModelsCountByBrand());

        [Authorize(Roles = "Admin, Customer")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationRequest paginationRequest, int? brandId, int? minYear, int? maxYear)
        {
            return Execute(await _modelService.GetModelsAsync(paginationRequest, brandId, minYear, maxYear));
        }

        [Authorize(Roles = "Admin, Customer")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Execute(await _modelService.GetByIdAsync(id));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(PostModelDto postModelDto)
        {
            return Execute(await _modelService.CreateAsync(postModelDto));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Execute(await _modelService.DeleteAsync(id));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Update(int id, UpdateModelDto updateModelDto)
        {
            return Execute(await _modelService.UpdateAsync(id, updateModelDto));
        }
    }
}
