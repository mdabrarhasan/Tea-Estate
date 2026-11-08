using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TEALINK.GARDEN.Interface.Common;
using TEALINK.GARDEN.Interface.DailyNonPlucking;
using TEALINK.GARDEN.Interface.DailyPlucking;
using TEALINK.GARDEN.Model.DailyNonPlucking;
using TEALINK.GARDEN.Model.DailyPlucking;

namespace TEALINK.GARDEN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DailyNonPluckingController : ControllerBase
    {
        private readonly IDailyNonPlucking _cm;
        private readonly ICommon _com;


        public DailyNonPluckingController(IDailyNonPlucking cm, ICommon com)
        {
            _cm = cm;
            _com = com;
        }


        [HttpGet]
        public JsonResult Get(string pluckingdate, int? kamjaritypeid = 0, int?  bookid=0, int? operatorid=0, int? deviceid=0, int? batchid = 0, int? recordid = 0, int? divisionid = 0, int? unitid = 0, int enroll = 0)
        {
            var li = _cm.GetData(pluckingdate, kamjaritypeid, bookid, operatorid, deviceid, batchid, recordid, divisionid, unitid, enroll);
            return new JsonResult(_cm.GetData(pluckingdate, kamjaritypeid, bookid, operatorid, deviceid, batchid, recordid, divisionid, unitid, enroll));
        }



        [HttpGet]
        [Route("GetDataByID/{id}/{pluckeddate}/{enroll}/{unitid}")]
        public JsonResult GetDataByID(int id, string pluckeddate, int unitid, int enroll)
        {
            var obj = _cm.GetDataByID(id, pluckeddate, unitid, enroll);
            return new JsonResult(obj);
        }


        [HttpGet]
        [Route("GetDataEmpCountDateWise/{pluckeddate}/{unitid}")]
        public JsonResult GetDataEmpCountDateWise(string pluckeddate, int unitid)
        {
            var obj = _cm.GetDataCountDateWise(pluckeddate, unitid);
            return new JsonResult(obj);
        }

        //[HttpPost]
        //public async JsonResult Post(List<DailyNonPluckingView> master)
        //{
        //    return new JsonResult(await _cm.Create(master));
        //}

        [HttpPost]
        public async Task<JsonResult> Post([FromBody] List<DailyNonPluckingView> master)
        {
            var result = await _cm.Create(master);
            return new JsonResult(result);
        }

        [HttpPost]
        [Route("KamjariUpdate")]
        public JsonResult KamjariUpdate([FromBody] GetEditKamjari data)
        {
            return new JsonResult(_cm.KamjariUpdate(data.Id, data.Kamjariid,data.Teamid));
        }
        [HttpDelete]
        public JsonResult Delete(int id, int enroll, int unitid)
        {
            return new JsonResult(_cm.Delete(id, unitid, enroll));
        }
    }
}
