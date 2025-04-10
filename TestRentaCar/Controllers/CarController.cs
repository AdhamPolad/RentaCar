using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using TestRentaCar.Api.Controllers.Base;
using TestRentaCar.Buisness.Dtos.Car;
using TestRentaCarDataAccess.Entities;
using TestRentaCarDataAccess.Enums;
using TestRentaCarDataAccess.Model;
using TestRentaCarSln.Buisness.Abstractions;
using TestRentaCarSln.Buisness.Dtos.Car;

namespace TestRentaCar.Api.Controllers
{
    [Route("api/v1/cars")]
    public class CarController : BaseController
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddAsync(PostCarDto postCarDto)
        {
            return Execute(await _carService.CreateAsync(postCarDto));
        }

        [Authorize(Roles = "Admin, Customer")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationRequest paginationRequest,int? modelId, CarCatagory? carCatagory, int? enginId)
        {
            return Execute(await _carService.GetAviableCarsAsync(paginationRequest, modelId, carCatagory, enginId));
        }

        [Authorize(Roles = "Admin, Customer")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Execute(await _carService.GetByIdAsync(id));
        }


        [HttpGet("get-cheapest-car")]
        public async Task<IActionResult> GetCheapestCar()
        {
            return Execute(await _carService.GetCheapestCar());
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Execute(await _carService.DeleteAsync(id));
        }
        [Authorize(Roles = "Admin")]        
        [HttpPut]
        public async Task<IActionResult> Update(int id, UpdateCarDto updateCarDto, CarCatagory carCatagory)
        {
            return Execute(await _carService.UpdateAsync(id, updateCarDto, carCatagory));
        }


    }
}
