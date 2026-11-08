using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TEALINK.GARDEN.Interface.Login;
using TEALINK.GARDEN.Model.Auth;

namespace TEALINK.GARDEN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public HtmlString HtmlUserID { get; set; }
        public HtmlString HtmlUserPass { get; set; }


        private readonly ILogin _login;
        public LoginController(ILogin login)
        {
            _login = login;
        }

        //[HttpPost]
        //public JsonResult Post(Login objlogin)
        //{
        //    try
        //    {
        //        string tokenString = "";

        //        var query = _login.AuthenticateUser(objlogin.UserID, objlogin.Emppass).ToList();

        //        if (query.Count != 0)
        //        {
        //            foreach (var m in query)
        //            {

        //            }

        //            tokenString = _login.GenerateJsonWebToken();
        //            var allData = new { query = query, token = tokenString };

        //            return new JsonResult(allData);
        //        }
        //    }
        //    catch (Exception EX)
        //    {
        //        return new JsonResult(404);
        //    }
        //    return new JsonResult(404);

        //}

        [HttpPost]
        public JsonResult Post(Login objlogin)
        {
            try
            {
                var query = _login.AuthenticateUser(objlogin.UserID, objlogin.Emppass).ToList();
                if (query.Count != 0)
                {
                    var u = query[0];
                    string tokenString = _login.GenerateJsonWebToken(u.Enroll, u.Unit, u.TypeId);
                    var permissions = _login.GetOperatorPermissionList();          // ADDED
                    var allData = new { query = query, token = tokenString, permissions = permissions };
                    return new JsonResult(allData);
                }
                //if (query.Count != 0)
                //{
                //    var u = query[0];
                //    string tokenString = _login.GenerateJsonWebToken(u.Enroll, u.Unit, u.TypeId);
                //    var allData = new { query = query, token = tokenString };
                //    return new JsonResult(allData);
                //}
            }
            catch (Exception)
            {
                return new JsonResult(404);
            }
            return new JsonResult(404);
        }


    }
}
