using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using TEALINK.GARDEN.Interface.PluckingAllocation;
using TEALINK.GARDEN.Model.Division;
using TEALINK.GARDEN.Model.PluckingAllocation;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TEALINK.GARDEN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PluckingAllocationController : ControllerBase
    {
        private readonly IPluckingAllocation _IPA;

        public PluckingAllocationController(IPluckingAllocation IPA)
        {
            _IPA = IPA;
        }

        [HttpGet("GetDataByID")]
        public JsonResult GetDataByID(string unitid, int id, string date)
        {
            int unit= int.Parse(unitid);
            var li = _IPA.GetDataByID(unit, id, date);
            return new JsonResult(li);
        }
        [HttpPost("Save")]

       
        public JsonResult Save([FromBody] PluckingSaveRequest request)
        {

            return new JsonResult(_IPA.Create(request.pl, request.master));
        }
    }
}
