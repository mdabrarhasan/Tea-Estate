using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TEALINK.GARDEN.Interface.Common;
using TEALINK.GARDEN.Interface.CompanyManagement;
using TEALINK.GARDEN.Interface.Division;
using TEALINK.GARDEN.Model.Division;

namespace TEALINK.GARDEN.Controllers
{
 
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]

    public class DivisionController : ControllerBase
    {

        private readonly IDivision _cm;
        private readonly ICommon _com;
        public DivisionController(IDivision cm, ICommon com)
        {
            _cm = cm;
            _com = com;
        }

        [HttpGet]
        public JsonResult Get(int unitid)
        {
            return new JsonResult(_cm.GetDivisionInfo(unitid));
        }

        [HttpGet]
        [Route("GetDataByID/{id}")]
        public JsonResult GetDataByID(int id)
        {
            var obj = _cm.GetDataByID(id);
            return new JsonResult(obj);
        }

        [HttpPost]
        public JsonResult Post(DivisionMaster master)
        {
            return new JsonResult(_cm.Create(master));
        }

        [HttpPut]
        public JsonResult Put(int id, DivisionMaster master)
        {
            var li = _cm.Update(id, master);
            return new JsonResult(li);
        }

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            return new JsonResult(_cm.Delete(id));
        }

       
    }
}


