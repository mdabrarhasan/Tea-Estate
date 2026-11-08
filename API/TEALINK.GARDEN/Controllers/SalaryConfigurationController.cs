using Microsoft.AspNetCore.Mvc;
using TEALINK.GARDEN.Interface.SalaryConfiguration;
using TEALINK.GARDEN.Model.SalaryConfiguration;

namespace TEALINK.GARDEN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryConfigurationController : ControllerBase
    {
        private readonly ISalaryConfiguration _salaryConfiguration;

        public SalaryConfigurationController(ISalaryConfiguration salaryConfiguration)
        {
            _salaryConfiguration = salaryConfiguration;
        }

        [HttpGet]
        public JsonResult Get([FromQuery] int unitid, [FromQuery] int enroll)
        {
            var obj = _salaryConfiguration.GetData(unitid, enroll);
            return new JsonResult(obj);
        }
        [HttpGet]
        [Route("GetDataByID/{id}")]
        public JsonResult GetDataByID(int id, [FromQuery] int unitid, [FromQuery] int enroll)
        {
            var obj = _salaryConfiguration.GetDataByID(id, unitid, enroll);
            return new JsonResult(obj);
        }
        [HttpPost]
        public JsonResult Post(SalaryConfigurationMaster master)
        {
            return new JsonResult(_salaryConfiguration.Create(master));
        }
        [HttpPut]
        public JsonResult Put(int id, SalaryConfigurationMaster master)
        {
            return new JsonResult(_salaryConfiguration.Update(id, master));
        }
        [HttpDelete]
        public JsonResult Delete(int id, int unitid, int enroll)
        {
            return new JsonResult(_salaryConfiguration.Delete(id, unitid, enroll));
        }
    }
}
