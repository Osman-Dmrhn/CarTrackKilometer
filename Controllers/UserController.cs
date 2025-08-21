using CarKilometerTrack.Dtos;
using CarKilometerTrack.Dtos.UserDtos;
using CarKilometerTrack.Helpers;
using CarKilometerTrack.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarKilometerTrack.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost("login")]
        public async Task<IActionResult> login([FromBody] UserLoginDto data) 
        {
            var result = await _userService.VerificationUser(data);
            if (result.IsSuccess) { return Ok(result); }
            return BadRequest(result);
        }

        [Authorize]
        [HttpPost("me")]
        public async Task<IActionResult> me()
        {
            var userId =User.GetUserId();
            var result = await _userService.GetUserRoleById(userId.Value);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("addUser")]
        public async Task<IActionResult> addUser([FromBody] UserAddDto data)
        {
            var userId=User.GetUserId();
            var result = await _userService.AddUserAsync(data,userId.Value);
            return Ok(result);
        }


        [Authorize(Roles = "Admin")]
        [HttpPut("updateUser/{id}")]
        public async Task<IActionResult> updateUser(int id ,[FromBody] UserUpdateDto data)
        {
            var userId = User.GetUserId();
            var result = await _userService.UpdateUserAsync(id, data, userId.Value);
            return Ok(result);
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("deleteUser/{id}")]
        public async Task<IActionResult> deleteUser(int id)
        {
            var userId = User.GetUserId();
            var result = await _userService.DeleteUserAsync(id, userId.Value);
            return Ok(result);
        }


        [Authorize(Roles ="Admin")]
        [HttpGet("getAllUser")]
        public async Task<IActionResult> getAllUsers(int page, int take, string? searchString)
        {
            var result = await _userService.GetAllUsersAsync(page,take, searchString);
            return Ok(result);
        }


        [Authorize]
        [HttpGet("getUser/{id}")]
        public async Task<IActionResult> getUserById(int id)
        {
            var result = await _userService.GetUserAsync(id);
            return Ok(result);
        }

        [Authorize]
        [HttpPut("userPassUpdate")]
        public async Task<IActionResult> userPassUpdate([FromBody] UserPassDto data)
        {
            var userId = User.GetUserId();
            var result = await _userService.UpdateUserPassAsync(data,userId.Value);
            return Ok(result);
        }


        [Authorize(Roles ="Admin")]
        [HttpGet("resetPassUpdate/{id}")]
        public async Task<IActionResult> resetUserPass(int id)
        {
            var result = await _userService.ResetUserPass(id);
            return Ok(result);
        }
    }
}
