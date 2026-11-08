using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TEALINK.GARDEN.Interface.Permission;

namespace TEALINK.GARDEN.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class PermissionAttribute : Attribute, IFilterFactory
    {
        private readonly int _subMenuId;
        private readonly string _action;
        public bool IsReusable => false;

        public PermissionAttribute(int subMenuId, string action)
        {
            _subMenuId = subMenuId;
            _action = action;
        }

        public IFilterMetadata CreateInstance(IServiceProvider sp)
        {
            var check = sp.GetRequiredService<IPermissionCheck>();
            return new PermissionFilter(check, _subMenuId, _action);
        }

        private class PermissionFilter : IActionFilter
        {
            private readonly IPermissionCheck _check;
            private readonly int _subMenuId;
            private readonly string _action;

            public PermissionFilter(IPermissionCheck check, int subMenuId, string action)
            {
                _check = check; _subMenuId = subMenuId; _action = action;
            }

            public void OnActionExecuting(ActionExecutingContext context)
            {
                var typeId = context.HttpContext.User.FindFirst("TypeId")?.Value;

                if (string.IsNullOrEmpty(typeId))
                {
                    context.Result = new JsonResult(new { message = "Unauthorized", status = "error" })
                    { StatusCode = 401 };
                    return;
                }

                if (typeId == "2") return;   // admin bypass

                if (!_check.IsAllowed(_subMenuId, _action))
                {
                    context.Result = new JsonResult(new { message = "You don't have permission for this action", status = "error" })
                    { StatusCode = 403 };
                }
            }

            public void OnActionExecuted(ActionExecutedContext context) { }
        }
    }
}