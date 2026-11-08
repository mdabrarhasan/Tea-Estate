using TEALINK.GARDEN.Model.Auth;
using TEALINK.GARDEN.Model.Permission;

namespace TEALINK.GARDEN.Interface.Login
{
    public interface ILogin
    {
        string GenerateJsonWebToken(int enroll, int unitId, int typeId);
        IQueryable<LoginModel> AuthenticateUser(string strUserID, string strUserPass);
        List<OperatorPermissionListRow> GetOperatorPermissionList();
    }
}
