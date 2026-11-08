using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TEALINK.GARDEN.Filters;
using TEALINK.GARDEN.Interface.Common;
using TEALINK.GARDEN.Interface.Holyday;
using TEALINK.GARDEN.Interface.WageCalender;
using TEALINK.GARDEN.Model.Holiday;
using TEALINK.GARDEN.Model.LeaveManagement;

namespace TEALINK.GARDEN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HolidayController : ControllerBase
    {
        private readonly IHoliday _cm;
        private readonly ICommon _com;
        public HolidayController(IHoliday cm, ICommon com)
        {
            _cm = cm;
            _com = com;
        }

        [HttpGet]
        public JsonResult Get(int unitid, int enroll)
        {
            return new JsonResult(_cm.GetData(unitid, enroll));
        }
        
        [HttpPost]
        public JsonResult Post(HolidayModel master)
        {
            return new JsonResult(_cm.Create(master));
        }
        [HttpDelete]
        public JsonResult Delete(int id, int enroll, int unitid)
        {
            return new JsonResult(_cm.Delete(id, unitid, enroll));
        }
    }
}
