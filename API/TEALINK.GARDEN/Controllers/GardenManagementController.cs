using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TEALINK.GARDEN.Interface.Common;
using TEALINK.GARDEN.Interface.Division;
using TEALINK.GARDEN.Interface.GardenManagement;
using TEALINK.GARDEN.Model.Division;
using TEALINK.GARDEN.Model.GardenManagement;

namespace TEALINK.GARDEN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GardenManagementController : ControllerBase
    {
        private readonly IGardenManagement _cm;
        private readonly ICommon _com;
        public GardenManagementController(IGardenManagement cm, ICommon com)
        {
            _cm = cm;
            _com = com;
        }

        [HttpGet]
        public JsonResult Get(int unitid)
        {
            return new JsonResult(_cm.GetData(unitid));
        }

        [HttpGet]
        [Route("GetDataByID/{id}")]
        public JsonResult GetDataByID(int id)
        {
            var obj = _cm.GetDataByID(id);
            return new JsonResult(obj);
        }

        [HttpPost]
        public JsonResult Post(GardenManagementMaster master)
        {
            return new JsonResult(_cm.Create(master));
        }

        [HttpPut]
        public JsonResult Put(int id, GardenManagementMaster master)
        {
            var li = _cm.Update(id, master);
            return new JsonResult(li);
        }

        [HttpDelete]
        public JsonResult Delete(int id,int unitid)
        {
            return new JsonResult(_cm.Delete(id, unitid));
        }
    }
}
