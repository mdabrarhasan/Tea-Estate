using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TEALINK.GARDEN.Interface.LeaveMnagement;
using TEALINK.GARDEN.Model.DailyNonPlucking;
using TEALINK.GARDEN.Model.LeaveManagement;

namespace TEALINK.GARDEN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveManagementController : ControllerBase
    {
        private readonly ILeaveMangement _lm;
        public LeaveManagementController(ILeaveMangement lm)
        {
            _lm = lm;
        }
        [HttpPost]
        public JsonResult Post(LeaveMaster master)
        {
            return new JsonResult(_lm.Create(master));
        }

        [HttpGet]
        [Route("LeaveGet/{workerid}/{leavetypeid}")]
        public JsonResult LeaveGet(int workerid,int leavetypeid)
        {
            var li=_lm.LeaveGet(workerid, leavetypeid);
            return new JsonResult(li);
        }
        [HttpGet]
        public JsonResult Get(int unitid)
        {
            return new JsonResult(_lm.Getdata(unitid));
        }
        [HttpDelete]
        public JsonResult Delete(int id, int enroll, int unitid)
        {
            return new JsonResult(_lm.Delete(id, unitid, enroll));
        }
    }
}
