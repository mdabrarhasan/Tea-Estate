using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TEALINK.GARDEN.Filters;
using TEALINK.GARDEN.Interface.Common;
using TEALINK.GARDEN.Interface.ShiftManagement;
using TEALINK.GARDEN.Interface.WageCalender;
using TEALINK.GARDEN.Model.ShiftManagement;
using TEALINK.GARDEN.Model.WageCalender;

namespace TEALINK.GARDEN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WageCalenderController : ControllerBase
    {
        private readonly IWageCalender _cm;
        private readonly ICommon _com;
        public WageCalenderController(IWageCalender cm, ICommon com)
        {
            _cm = cm;
            _com = com;
        }

        [HttpGet]
        public JsonResult Get(int unitid, int enroll)
        {
            var li = _cm.GetData(unitid, enroll);
            return new JsonResult(_cm.GetData(unitid, enroll));
        }

        
        [HttpGet]
        [Route("GetDataByID/{id}/{unitid}")]
        public JsonResult GetDataByID(int id, int unitid)
        {
            return new JsonResult(_cm.GetDataByID(id, unitid));
        }

        [HttpPost]
        public JsonResult Post(WageCalenderModel master)
        {
            return new JsonResult(_cm.Create(master));
        }

        [HttpPost]
        [Route("SalaryGenerate")]
        public async Task<JsonResult> SalaryGenerate(WageCalenderModel master)
        {
            var result = await _cm.SalaryGenerate(master);
            return new JsonResult(result);
        }

        [HttpPut]
        public JsonResult Put(int id, WageCalenderModel master)
        {
            var li = _cm.Update(id, master);
            return new JsonResult(li);
        }

        [HttpDelete]
        public JsonResult Delete(int id, int unitid, int enroll)
        {
            return new JsonResult(_cm.Delete(id, unitid, enroll));
        }
        
        [HttpDelete]
        [Route("SalaryCancel/{unitid}/{bookid}")]
        public JsonResult SalaryCancel(int unitid,  int bookid)
        {
            var obj = _cm.SalaryCancel(unitid,  bookid);
            return new JsonResult(obj);
        }
    }
}
