using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TEALINK.GARDEN.Interface.Common;
using TEALINK.GARDEN.Interface.PruneType;
using TEALINK.GARDEN.Interface.WorkerBook;
using TEALINK.GARDEN.Model.PruneingType;
using TEALINK.GARDEN.Model.WorkerBook;

namespace TEALINK.GARDEN.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WorkerBookController : ControllerBase
    {
        private readonly IWorkerBook _cm;
        private readonly ICommon _com;
        public WorkerBookController(IWorkerBook cm, ICommon com)
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
        public JsonResult GetDataByID(int id)
        {
            var obj = _cm.GetDataByID(id);
            return new JsonResult(obj);
        }

        [HttpPost]
        public JsonResult Post(WorkerBookMaster master)
        {
            return new JsonResult(_cm.Create(master));
        }

        [HttpPut]
        public JsonResult Put(int id, WorkerBookMaster master)
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
