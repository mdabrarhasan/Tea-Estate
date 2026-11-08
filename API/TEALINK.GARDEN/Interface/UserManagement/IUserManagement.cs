using TEALINK.GARDEN.Model.UserManagement;

namespace TEALINK.GARDEN.Interface.UserManagement
{
    public interface IUserManagement
    {
        IQueryable<UserManagementMaster> GetUserInfo(int unitid);
        IQueryable<UserManagementMaster> GetDataByID(int id);
        IQueryable<UserTypeList> GetUserType();
        Tuple<string, string> Create(UserManagementMaster master);
        Tuple<string, string> Update(int id, UserManagementMaster master);
        Tuple<string, string> Delete(int id);
    }
}