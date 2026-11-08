using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TEALINK.GARDEN.Interface.Report;

namespace TEALINK.GARDEN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WagesSummaryReportController : ControllerBase
    {
        private readonly IReportService _obj;
        public WagesSummaryReportController(IReportService obj)
        {
            _obj = obj;
        }


        [HttpGet]
        public IActionResult WagesSummaryReport(int id)
        {
            var reportData = _obj.GetReportCredentials(id);
            if (reportData == null)
            {
                return NotFound();
            }
            return Ok(reportData);
        }

    }
}
