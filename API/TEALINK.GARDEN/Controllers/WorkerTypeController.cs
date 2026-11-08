using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TEALINK.GARDEN.Interface.Common;
using TEALINK.GARDEN.Interface.WorkerBook;
using TEALINK.GARDEN.Interface.WorkerType;
using TEALINK.GARDEN.Model.WorkerBook;
using TEALINK.GARDEN.Model.WorkerType;

namespace TEALINK.GARDEN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerTypeController : ControllerBase
    {
        private readonly IWorkerType _cm;
        private readonly ICommon _com;
        public WorkerTypeController(IWorkerType cm, ICommon com)
        {
            _cm = cm;
            _com = com;
        }

        [HttpGet]
        public JsonResult Get(int unitid, int enroll)
        {
            return new JsonResult(_cm.GetData(unitid, enroll));
        }

        [HttpGet]
        [Route("GetDataByID/{id}")]
        public JsonResult GetDataByID(int id, int unitid, int enroll)
        {
            var obj = _cm.GetDataByID(id,unitid,enroll);
            return new JsonResult(obj);
        }

        [HttpPost]
        public JsonResult Post(WorkerTypeModel master)
        {
            return new JsonResult(_cm.Create(master));
        }

        [HttpPut]
        public JsonResult Put(int id, WorkerTypeModel master)
        {
            var li = _cm.Update(id, master);
            return new JsonResult(li);
        }

        [HttpDelete]
        public JsonResult Delete(int id, int unitid, int enroll)
        {
            return new JsonResult(_cm.Delete(id, unitid, enroll));
        }
    }
}
