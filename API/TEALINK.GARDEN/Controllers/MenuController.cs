using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TEALINK.GARDEN.Interface.Menu;

namespace TEALINK.GARDEN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenu _menu;
        public MenuController(IMenu menu)
        {
            _menu = menu;
        }
        [HttpGet]
        public JsonResult Get(string UserID)
        {
            return new JsonResult(_menu.GetMenu(UserID));
        }

        [HttpGet]
        [Route("LoadGSubMenu")]
        public JsonResult SubMenu(string UserID)
        {
            return new JsonResult(_menu.GetSubMenu(UserID));
        }

        [HttpGet]
        [Route("MenuList")]
        public JsonResult MenuList(string UserID)
        {
            return new JsonResult(_menu.GetMenuList(UserID));
        }
    }
}
