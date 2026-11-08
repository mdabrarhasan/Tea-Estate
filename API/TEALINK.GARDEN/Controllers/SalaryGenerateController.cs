using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TEALINK.GARDEN.Interface.Common;
using TEALINK.GARDEN.Interface.PruneType;
using TEALINK.GARDEN.Interface.SalaryGenerate;
using TEALINK.GARDEN.Model.PruneingType;

namespace TEALINK.GARDEN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryGenerateController : ControllerBase
    {
        private readonly ISalaryGenerate _cm;
        private readonly ICommon _com;
        public SalaryGenerateController(ISalaryGenerate cm, ICommon com)
        {
            _cm = cm;
            _com = com;
        }

        [HttpGet]
        public JsonResult Get(string sdate,string edate, int uniid,int enroll)
        {
            return new JsonResult(_cm.Get(sdate, edate, uniid,enroll));
        }


    }
}
