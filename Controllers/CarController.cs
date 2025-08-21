using CarKilometerTrack.Dtos.CarDtos;
using CarKilometerTrack.Helpers;
using CarKilometerTrack.Interfaces;
using CarKilometerTrack.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarKilometerTrack.Controllers
{
    [Route("api/cars")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarServices _carServices;
        private readonly IUserService _userService;
        public CarController(ICarServices carServices,IUserService userService)
        {
            _carServices = carServices;
            _userService= userService;
        }

        [Authorize]
        [HttpGet ("getAllCars")]
        public async Task<IActionResult> getAllCars(int page,int take,string? searchString)
        {
            var result = await _carServices.GetAllCarsAsync(page,take,searchString);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("getCar/{id}")]
        public async Task<IActionResult> getCar(int id)
        {
            var result = await _carServices.GetCarsAsync(id);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("addCar")]
        public async Task<IActionResult> addCar([FromBody] CarAddDto data)
        {
                var userId = User.GetUserId();
                var result = await _carServices.AddCarAsync(data,userId.Value);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("updateCar/{id}")]
        public async Task<IActionResult> updateCar(int id ,[FromBody] CarAddDto data)
        {
            var userId = User.GetUserId();
            var result = await _carServices.UpdateCarAsync(id,data, userId.Value);
            return Ok(result);
        }

        [Authorize]
        [HttpPut("updateKilometer/{id}")]
        public async Task<IActionResult> updateKilometerCar(int id, [FromBody] CarKilometerUpdate data)
        {
            var userId = User.GetUserId();
            var result = await _carServices.CarKilometerUpdateAsync(id,data.kilometer, userId.Value);
            return Ok(result);
        }


        [Authorize(Roles ="Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteCar(int id)
        {
            var userId = User.GetUserId();
            var result = await _carServices.DeleteCarAsync(id, userId.Value);
            return Ok(result);
        }

        [Authorize]
        [HttpPut("inUseCar/{id}")]
        public async Task<IActionResult> inUseCar([FromBody] CarUseUpdateDto data,int id)
        {
            var userId = User.GetUserId();
            var result = await _carServices.CarInUseUpdate(id,data, userId.Value);
            return Ok(result);
        }
    }
}
