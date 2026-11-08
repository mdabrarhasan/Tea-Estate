// Controllers/OperatorPermissionController.cs
using Microsoft.AspNetCore.Mvc;
using TEALINK.GARDEN.Interface.Permission;
using TEALINK.GARDEN.Model.Permission;

namespace TEALINK.GARDEN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperatorPermissionController : ControllerBase
    {
        private readonly IOperatorPermission _perm;
        public OperatorPermissionController(IOperatorPermission perm) => _perm = perm;

        [HttpGet]
        public JsonResult Get() => new JsonResult(_perm.GetOperatorPermissions());

        [HttpPost]
        public JsonResult Post([FromBody] SaveOperatorPermissionDto dto)
            => new JsonResult(_perm.SavePermission(dto));
    }
}