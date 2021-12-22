using FreeCourse.Shared.ControllerBases;
using GuideService.Report.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;
using System.Threading.Tasks;

namespace GuideService.Report.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : CustomBaseController
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{

        //    var result = await _reportService.GetAllAsync();
        //    return Ok(result);
        //}

        [HttpGet("{id}")]
        //[Route("DownloadFile")]
        public async Task<ActionResult> DownloadFile(string id)
        {
            var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, $"{id}.xlsx");

            var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(bytes, "text/plain", Path.GetFileName(filePath));
        }
    }
}
