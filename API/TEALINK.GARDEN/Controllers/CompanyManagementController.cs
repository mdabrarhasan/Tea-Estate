using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TEALINK.GARDEN.Interface.Common;
using TEALINK.GARDEN.Interface.CompanyManagement;
using TEALINK.GARDEN.Interface.Menu;

namespace TEALINK.GARDEN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyManagementController : ControllerBase
    {
        private readonly ICompanyManagement _cm;
        private readonly ICommon _com;
        public CompanyManagementController(ICompanyManagement cm,ICommon com)
        {
            _cm = cm;
            _com= com;
        }

        [HttpGet]
        public JsonResult Get(int unitid)
        {
            return new JsonResult(_cm.GetCompanyInfo(unitid));
        }
    }
}
