using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TEALINK.GARDEN.Interface.DashBoard;

namespace TEALINK.GARDEN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {
        private readonly IDashBoard dashBoard;
        public DashBoardController(IDashBoard dash) { 
         dashBoard = dash;
        }

        [HttpGet]
        [Route("GetDataByID/{id}/{pluckeddate}/{enroll}/{unitid}")]
        public JsonResult GetDataByID(int id, string pluckeddate, int unitid, int enroll)
        {
            var obj = dashBoard.GetDataByID(id, pluckeddate, unitid, enroll);
            return new JsonResult(obj);
        }
        [HttpGet]
        [Route("GetDashboardStats/{pluckeddate}/{unitid}")]
        public JsonResult GetDashboardStats(string pluckeddate, int unitid)
        {
            var obj = dashBoard.GetDashboardStats(pluckeddate, unitid);
            return new JsonResult(obj);
        }

        [HttpGet]
        [Route("GetSectionPlucking/{pluckeddate}/{unitid}")]
        public JsonResult GetSectionPlucking(string pluckeddate, int unitid)
        {
            var obj = dashBoard.GetSectionPlucking(pluckeddate, unitid);
            return new JsonResult(obj);
        }

        [HttpGet]
        [Route("GetPluckingTrend/{pluckeddate}/{unitid}")]
        public JsonResult GetPluckingTrend(string pluckeddate, int unitid)
        {
            var obj = dashBoard.GetPluckingTrend(pluckeddate, unitid);
            return new JsonResult(obj);
        }
    }
}
