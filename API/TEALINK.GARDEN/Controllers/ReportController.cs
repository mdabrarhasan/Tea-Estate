using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TEALINK.GARDEN.Interface.Report;
using TEALINK.GARDEN.Model.Report;

namespace TEALINK.GARDEN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _obj;
        public ReportController(IReportService obj)
        {
            _obj = obj;
        }

        [HttpGet]
        public IActionResult Report(int id)
        {
            var reportData = _obj.GetReportCredentials(id);
            if (reportData == null)
            {
                return NotFound();
            }
            return Ok(reportData);
        }

        [HttpGet]
        [Route("GetWageSheet")]
        public IActionResult GetSalaryDate(int id,int bookid,int modeid,int reportid)
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
