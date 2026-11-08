using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TEALINK.GARDEN.Interface.Common;
using TEALINK.GARDEN.Interface.KamjariManagement;
using TEALINK.GARDEN.Model.Kamjari;


namespace TEALINK.GARDEN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KamjariController : ControllerBase
    {
        private readonly IKamjariManagement _cm;
        private readonly ICommon _com;

        public KamjariController(IKamjariManagement cm, ICommon com)
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
        public JsonResult GetDataByID(int id, [FromQuery] int unitid, [FromQuery] int enroll)
        {
            var obj = _cm.GetDataByID(id, unitid, enroll);
            return new JsonResult(obj);
        }

        [HttpPost]
        public JsonResult Post(KamjariMaster master)
        {
            return new JsonResult(_cm.Create(master));
        }

        [HttpPut]
        public JsonResult Put(int id, KamjariMaster master)
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
