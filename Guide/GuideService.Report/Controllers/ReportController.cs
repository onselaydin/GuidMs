using FreeCourse.Shared.ControllerBases;
using GuideService.Report.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _reportService.GetAllAsync();
            return Ok(result);
        }
    }
}
