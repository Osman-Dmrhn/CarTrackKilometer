using CarKilometerTrack.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace CarKilometerTrack.Controllers
{
    [ApiController]
    [Route("api/logs")]
    public class LogController : Controller
    {
        private readonly ILogServices _logservices;

        public LogController(ILogServices logservices)
        {
            _logservices = logservices;
        }

        [Authorize(Roles ="Admin")]
        [HttpGet("getAllLogs")]
        public async  Task<IActionResult> getAllLogs()
        {
            var result=await _logservices.GetAllLogs();
            return Ok(result);
        }

        [Authorize(Roles ="Admin")]
        [HttpGet("GetAllLogsByUser")]
        public async Task<IActionResult> GetAllLogsByUser(int page,int take, string? searchString)
        {
            var result = await _logservices.GetAllLogsByUser(page,take,searchString);
            return Ok(result);
        }

        [Authorize(Roles ="Admin")]
        [HttpGet("getAllLogsByCar/{id}")]
        public async Task<IActionResult> getAllLogsByCar(int id,int take,int page,string? searchString)
        {
           var result = await _logservices.GetAllLogsByCar(id,take,page,searchString);
           return Ok(result);
        }

        [Authorize(Roles ="Admin")]
        [HttpDelete("deleteAllCarLogs")]
        public async Task<IActionResult> deleteAllCarLogs()
        {
            var result = await _logservices.DeleteAllCarLogs();
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteAllLogsByCar/{id}")]
        public async Task<IActionResult> DeleteAllLogsByCar(int id)
        {
            var result = await _logservices.DeleteAllLogsByCar(id);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("deleteAllUserLogs")]
        public async Task<IActionResult> deleteAllUserLogs()
        {
            var result = await _logservices.DeleteAllUserLogs();
            return Ok(result);
        }


    }
}
