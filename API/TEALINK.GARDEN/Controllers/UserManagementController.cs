using Microsoft.AspNetCore.Mvc;
using TEALINK.GARDEN.Interface.UserManagement;
using TEALINK.GARDEN.Model.UserManagement;

namespace TEALINK.GARDEN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {
        private readonly IUserManagement _cm;
        public UserManagementController(IUserManagement cm)
        {
            _cm = cm;
        }

        [HttpGet]
        public JsonResult Get(int unitid)
        {
            return new JsonResult(_cm.GetUserInfo(unitid));
        }

        [HttpGet]
        [Route("GetDataByID/{id}")]
        public JsonResult GetDataByID(int id)
        {
            return new JsonResult(_cm.GetDataByID(id));
        }

        [HttpGet]
        [Route("GetUserType")]
        public JsonResult GetUserType()
        {
            return new JsonResult(_cm.GetUserType());
        }

        [HttpPost]
        public JsonResult Post(UserManagementMaster master)
        {
            return new JsonResult(_cm.Create(master));
        }

        [HttpPut]
        public JsonResult Put(int id, UserManagementMaster master)
        {
            return new JsonResult(_cm.Update(id, master));
        }

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            return new JsonResult(_cm.Delete(id));
        }
    }
}