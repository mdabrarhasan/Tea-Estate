// Interface/Permission/IOperatorPermission.cs
using TEALINK.GARDEN.Model.Permission;
namespace TEALINK.GARDEN.Interface.Permission
{
    public interface IOperatorPermission
    {
        List<OperatorPermissionRow> GetOperatorPermissions();
        Tuple<string, string> SavePermission(SaveOperatorPermissionDto dto);
    }
}