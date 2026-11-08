using Microsoft.EntityFrameworkCore;
namespace TEALINK.GARDEN.Model.Permission
{
    [Keyless]
    public class PermissionCheckResult
    {
        public bool ALLOWED { get; set; }
    }
}