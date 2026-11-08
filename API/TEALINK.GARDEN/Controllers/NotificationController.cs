using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TEALINK.GARDEN.Interface.DashBoard;

namespace TEALINK.GARDEN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class NotificationController : ControllerBase
    {
        private readonly INotification _cm;

        public NotificationController(INotification cm)
        {
            _cm = cm;
        }

        [HttpGet]
        public JsonResult Get(int unitid, DateTime? date)
        {
            var d = date ?? DateTime.Today;
            return new JsonResult(_cm.GetSummary(unitid, d));
        }
    }
}