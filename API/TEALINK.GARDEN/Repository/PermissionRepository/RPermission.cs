// Repository/PermissionRepository/ROperatorPermission.cs
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TEALINK.GARDEN.Interface.Permission;
using TEALINK.GARDEN.Model.Permission;

namespace TEALINK.GARDEN.Repository.PermissionRepository
{
    public class ROperatorPermission : IOperatorPermission
    {
        private readonly Context _context;
        public ROperatorPermission(Context context) => _context = context;

        public List<OperatorPermissionRow> GetOperatorPermissions()
        {
            try
            {
                return _context.Set<OperatorPermissionRow>()
                    .FromSqlRaw("EXEC SPGETOPERATORPERMISSION")
                    .ToList();
            }
            catch
            {
                return new List<OperatorPermissionRow>();
            }
        }

        public Tuple<string, string> SavePermission(SaveOperatorPermissionDto d)
        {
            try
            {
                var ps = new[]
                {
                    new SqlParameter("@MENUID", d.MenuId),
                    new SqlParameter("@SUBMENUID", d.SubMenuId),
                    new SqlParameter("@CANVIEW", d.CanView),
                    new SqlParameter("@CANINSERT", d.CanInsert),
                    new SqlParameter("@CANUPDATE", d.CanUpdate),
                    new SqlParameter("@CANDELETE", d.CanDelete),
                    new SqlParameter("@CANPRINT", d.CanPrint),
                    new SqlParameter("@UPDATEDBY", d.UpdatedBy)
                };
                _context.Database.ExecuteSqlRaw(
                    "EXEC SPSAVEOPERATORPERMISSION @MENUID,@SUBMENUID,@CANVIEW,@CANINSERT,@CANUPDATE,@CANDELETE,@CANPRINT,@UPDATEDBY", ps);
                return Tuple.Create("Permission saved", "success");
            }
            catch
            {
                return Tuple.Create("Save failed", "error");
            }
        }
    }
}