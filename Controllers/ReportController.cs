using CarKilometerTrack.Dtos;
using CarKilometerTrack.Helpers;
using CarKilometerTrack.Interfaces;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;

namespace CarKilometerTrack.Controllers
{
    [Route("api/reports")]
    [ApiController]  
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportservice;
        private readonly ILogServices _logServices;

        public ReportController(IReportService reportService, ILogServices logServices)
        {
            _reportservice = reportService;
            _logServices = logServices;
        }

        [HttpGet("getAllCarLogsDownload")]
        public async Task<IActionResult> getAllCarLogsExcel()
        {
            if( _logServices.GetAllCarLogs().Result.Data == null) { return BadRequest(); }
            var result = await _reportservice.GetAllCarLogs();
            return File(result,"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",$"loglar_{DateTime.UtcNow:yyyyMMdd_HHmm}.xlsx");


        }

        [HttpGet("getAllLogByCarDownload/{id}")]
        public async Task<IActionResult> getAllLogByCarExcel(int id)
        {
            if (_logServices.GetAllLogsByCarReport(id).Result.Data == null) { return BadRequest(); }
            var result = await _reportservice.GetCarLogByID(id); 
            return File(result,"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",$"loglar_{DateTime.UtcNow:yyyyMMdd_HHmm}.xlsx");
        }

        [HttpGet("getAllUserLogsDownload")]
        public async Task<IActionResult> getAllUserLogsDownload()
        {
            if (_logServices.GetAllLogsByUserReport() == null) { return BadRequest(); }
            var result = await _reportservice.GetAllUserLogs();
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"loglar_{DateTime.UtcNow:yyyyMMdd_HHmm}.xlsx");
        }
    }
}
