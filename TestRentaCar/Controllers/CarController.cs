using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestRentaCarSln.Buisness.Abstractions;
using TestRentaCarSln.Buisness.Dtos.Car;

namespace TestRentaCar.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(PostCarDto postCarDto)
        {
            var model = await _carService.CreateAsync(postCarDto);
            return StatusCode(model.StatusCode, model.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var model = await _carService.GetAllAsync();
            return StatusCode(model.StatusCode, model.Data);
        }

    }
}
