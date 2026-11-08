using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TEALINK.GARDEN.Interface.Common;
using TEALINK.GARDEN.Interface.GardenManagement;
using TEALINK.GARDEN.Interface.SectionManagement;
using TEALINK.GARDEN.Model.GardenManagement;
using TEALINK.GARDEN.Model.Section;

namespace TEALINK.GARDEN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionManagementController : ControllerBase
    {
        private readonly ISectionManagement _cm;
        private readonly ICommon _com;
        public SectionManagementController(ISectionManagement cm, ICommon com)
        {
            _cm = cm;
            _com = com;
        }

        [HttpGet]
        public JsonResult Get(int unitid,int enroll)
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
        public JsonResult Post(SectionManagementMaster master)
        {
            return new JsonResult(_cm.Create(master));
        }

        [HttpPut]
        public JsonResult Put(int id, SectionManagementMaster master)
        {
            var li = _cm.Update(id, master);
            return new JsonResult(li);
        }

        [HttpDelete]
        public JsonResult Delete(int id, int unitid, int enroll)
        {
            return new JsonResult(_cm.Delete(id, unitid,enroll));
        }
    }
}
