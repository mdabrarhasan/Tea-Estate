using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TEALINK.GARDEN.Interface.Permission;
using TEALINK.GARDEN.Model.Permission;

namespace TEALINK.GARDEN.Repository.PermissionRepository
{
    public class RPermissionCheck : IPermissionCheck
    {
        private readonly Context _context;
        public RPermissionCheck(Context context) => _context = context;

        public bool IsAllowed(int subMenuId, string action)
        {
            var ps = new[]
            {
                new SqlParameter("@SUBMENUID", subMenuId),
                new SqlParameter("@ACTION", action)
            };
            var row = _context.Set<PermissionCheckResult>()
                .FromSqlRaw("EXEC SPCHECKOPERATORPERMISSION @SUBMENUID,@ACTION", ps)
                .AsEnumerable()
                .FirstOrDefault();
            return row?.ALLOWED ?? false;      // no row => denied
        }
    }
}