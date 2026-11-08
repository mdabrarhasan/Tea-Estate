using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TEALINK.GARDEN.Interface.Common;
using TEALINK.GARDEN.Interface.DailyPlucking;
using TEALINK.GARDEN.Interface.GardenManagement;
using TEALINK.GARDEN.Model.DailyPlucking;
using TEALINK.GARDEN.Model.GardenManagement;

namespace TEALINK.GARDEN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DailyPluckingController : ControllerBase
    {
        private readonly IDailyPlucking _cm;
        private readonly ICommon _com;
        public DailyPluckingController(IDailyPlucking cm, ICommon com)
        {
            _cm = cm;
            _com = com;
        }


        [HttpGet]
        public JsonResult Get(string pluckingdate, int kamjaritypeid, int bookid, int operatorid, int deviceid, int batchid, int weighmentid, int divisionid, int unitid, int enroll)
        {
            return new JsonResult(_cm.GetData(pluckingdate,kamjaritypeid,bookid,operatorid,deviceid, batchid, weighmentid,divisionid, unitid, enroll));
        }

        [HttpGet]
        [Route("GetDataByID/{id}/{pluckeddate}/{enroll}/{unitid}")]
        public JsonResult GetDataByID(string id, string pluckeddate, int unitid, int enroll)
        {
            var obj = _cm.GetDataByID(id, pluckeddate, unitid, enroll);
            return new JsonResult(obj);
        }

        [HttpGet]
        [Route("GetQtyWeighmentWise/{pluckeddate}/{bookid}/{unitid}")]
        public JsonResult GetQtyWeighmentWise(string pluckeddate,int bookid, int unitid, int enroll)
        {
            var obj = _cm.GetQtyWeighmentWise(pluckeddate, bookid,unitid);
            return new JsonResult(obj);
        }

        [HttpGet]
        [Route("GetDataEmpCountDateWise/{pluckeddate}/{bookid}/{unitid}")]
        public JsonResult GetDataEmpCountDateWise(string pluckeddate, int bookid, int unitid)
        {
            var obj = _cm.GetDataCountDateWise(pluckeddate,bookid, unitid);
            return new JsonResult(obj);
        }

        [HttpGet]
        [Route("UpdateWeighment")]
        public JsonResult UpdateWeighment(int id, int weighment, int unitid, int enroll)
        {
            var obj = _cm.UpdateWeighment(id, weighment, unitid, enroll);
            return new JsonResult(obj);
        }

        [HttpPost]
        public JsonResult Post(List<DailyPluckingView> master)
        {
            return new JsonResult(_cm.Create(master));
        }

        [HttpDelete]
        public JsonResult Delete(int id, int enroll, int unitid)
        {
            return new JsonResult(_cm.Delete(id, unitid, enroll));
        }
    }
}
