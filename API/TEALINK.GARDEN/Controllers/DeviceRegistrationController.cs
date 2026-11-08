using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TEALINK.GARDEN.Interface.Device;
using TEALINK.GARDEN.Model.Device;

namespace TEALINK.GARDEN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceRegistrationController : ControllerBase
    {
        private readonly IDeviceRegistration _deviceRegistration;

        public DeviceRegistrationController(IDeviceRegistration deviceRegistration)
        {
            _deviceRegistration = deviceRegistration;
        }

        [HttpPost]
        public JsonResult Post([FromBody] DeviceInfo device)
        {
            return new JsonResult(_deviceRegistration.Create(device));
        }

        [HttpDelete]
        public JsonResult Delete(string code, int unitid)
        {
            return new JsonResult(_deviceRegistration.Delete(code, unitid));
        }

        [HttpGet]
        public JsonResult Get(int unitid)
        {
            var devices = _deviceRegistration.GetDevicesByUnit(unitid);
            return new JsonResult(devices);
        }
    }
}
