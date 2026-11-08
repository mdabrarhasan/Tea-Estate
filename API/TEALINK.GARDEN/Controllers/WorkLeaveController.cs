using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TEALINK.GARDEN.Interface.Common;
using TEALINK.GARDEN.Interface.WorkLeave;
using TEALINK.GARDEN.Model.WorkerBook;
using TEALINK.GARDEN.Model.WorkerLeave;

namespace TEALINK.GARDEN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkLeaveController : ControllerBase
    {
        private readonly IWorkLeave _Iwl;
        private readonly ICommon _com;
        public WorkLeaveController(IWorkLeave Iwl, ICommon com)
        {
            _Iwl = Iwl;
            _com = com;
        }
        [HttpPost]
        public JsonResult Post(WorkerLeaveMaster master)
        {
            return new JsonResult(_Iwl.Create(master));
        }
        [HttpGet]
        public JsonResult Get(int unitid, int enroll)
        {
            return new JsonResult(_Iwl.GetData(unitid, enroll));
        }
        [HttpGet]
        [Route("GetDataByID/{id}")]
        public JsonResult GetDataByID(int id)
        {
            var obj = _Iwl.GetDataByID(id);
            return new JsonResult(obj);
        }
        [HttpPut]
        public JsonResult Put(int id, WorkerLeaveMaster master)
        {
            var li = _Iwl.Update(id, master);
            return new JsonResult(li);
        }
        [HttpDelete]
        public JsonResult Delete(int id, int unitid, int enroll)
        {
            return new JsonResult(_Iwl.Delete(id, unitid, enroll));
        }

    }
}
