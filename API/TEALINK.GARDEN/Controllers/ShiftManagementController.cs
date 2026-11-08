using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TEALINK.GARDEN.Interface.Common;
using TEALINK.GARDEN.Interface.ShiftManagement;
using TEALINK.GARDEN.Interface.WorkerBook;
using TEALINK.GARDEN.Model.ShiftManagement;
using TEALINK.GARDEN.Model.WorkerBook;

namespace TEALINK.GARDEN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftManagementController : ControllerBase
    {
        private readonly IShiftManagement _cm;
        private readonly ICommon _com;
        public ShiftManagementController(IShiftManagement cm, ICommon com)
        {
            _cm = cm;
            _com = com;
        }

        [HttpGet]
        public JsonResult Get(int unitid, int enroll)
        {
            var obj = _cm.GetData(unitid, enroll);
            return new JsonResult(obj);
        }

        [HttpGet]
        [Route("GetDataByID/{id}")]
        public JsonResult GetDataByID(int id)
        {
            var obj = _cm.GetDataByID(id);
            return new JsonResult(obj);
        }

        [HttpPost]
        public JsonResult Post(ShiftMaster master)
        {
            return new JsonResult(_cm.Create(master));
        }

        [HttpPut]
        public JsonResult Put(int id, ShiftMaster master)
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
