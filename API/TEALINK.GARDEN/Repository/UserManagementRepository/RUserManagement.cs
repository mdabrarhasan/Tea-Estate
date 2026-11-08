using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TEALINK.GARDEN.Interface.UserManagement;
using TEALINK.GARDEN.Model.UserManagement;

namespace TEALINK.GARDEN.Repository.UserManagementRepository
{
    public class RUserManagement : IUserManagement
    {
        private readonly Context _context;
        string strMsg = "";
        string strStatusMsg = "";

        public RUserManagement(Context context)
        {
            _context = context;
        }

        public IQueryable<UserManagementMaster> GetUserInfo(int unitid)
        {
            SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
            return _context.UserManagementMaster.FromSqlRaw("Exec SPGETUSER @UNITID", UNITID);
        }

        public IQueryable<UserManagementMaster> GetDataByID(int id)
        {
            SqlParameter ID = new SqlParameter("@ID", id);
            return _context.UserManagementMaster.FromSqlRaw("Exec SPGETUSERDATABYID @ID", ID);
        }

        public IQueryable<UserTypeList> GetUserType()
        {
            return _context.UserTypeList.FromSqlRaw("Exec SPGETUSERTYPE");
        }

        public Tuple<string, string> Create(UserManagementMaster master)
        {
            try
            {
                var MSG = new SqlParameter { ParameterName = "@MSG", Size = 100, SqlDbType = System.Data.SqlDbType.VarChar, Direction = System.Data.ParameterDirection.Output };
                var STATUSMSG = new SqlParameter { ParameterName = "@STATUSMSG", Size = 100, SqlDbType = System.Data.SqlDbType.VarChar, Direction = System.Data.ParameterDirection.Output };

                SqlParameter USERNAME = new SqlParameter("@USERNAME", (object)master.Username ?? DBNull.Value);
                SqlParameter EMAILID = new SqlParameter("@EMAILID", (object)master.Emailid ?? DBNull.Value);
                SqlParameter CONTACTNUMBER = new SqlParameter("@CONTACTNUMBER", (object)master.Contactnumber ?? DBNull.Value);
                SqlParameter USERTYPEID = new SqlParameter("@USERTYPEID", master.Usertypeid);
                SqlParameter LOGINID = new SqlParameter("@LOGINID", (object)master.Loginid ?? DBNull.Value);
                SqlParameter PASSWORDS = new SqlParameter("@PASSWORDS", (object)master.Passwords ?? DBNull.Value);
                SqlParameter DIVISION = new SqlParameter("@DIVISION", (object)master.Division ?? DBNull.Value);
                SqlParameter BOOK = new SqlParameter("@BOOK", (object)master.Book ?? DBNull.Value);
                SqlParameter UNITID = new SqlParameter("@UNITID", master.UnitId);

                _context.Database.ExecuteSqlRaw("Exec SPSAVEUSER @USERNAME,@EMAILID,@CONTACTNUMBER,@USERTYPEID,@LOGINID,@PASSWORDS,@DIVISION,@BOOK,@UNITID,@MSG OUTPUT,@STATUSMSG OUTPUT",
                    USERNAME, EMAILID, CONTACTNUMBER, USERTYPEID, LOGINID, PASSWORDS, DIVISION, BOOK, UNITID, MSG, STATUSMSG);

                strMsg = MSG.Value.ToString();
                strStatusMsg = STATUSMSG.Value.ToString();
                return Tuple.Create(strMsg, strStatusMsg);
            }
            catch (Exception ex)
            {
                return Tuple.Create(ex.Message, "error");
            }
        }

        public Tuple<string, string> Update(int id, UserManagementMaster master)
        {
            try
            {
                var MSG = new SqlParameter { ParameterName = "@MSG", Size = 100, SqlDbType = System.Data.SqlDbType.VarChar, Direction = System.Data.ParameterDirection.Output };
                var STATUSMSG = new SqlParameter { ParameterName = "@STATUSMSG", Size = 100, SqlDbType = System.Data.SqlDbType.VarChar, Direction = System.Data.ParameterDirection.Output };

                SqlParameter ID = new SqlParameter("@ID", master.Id);
                SqlParameter USERNAME = new SqlParameter("@USERNAME", (object)master.Username ?? DBNull.Value);
                SqlParameter EMAILID = new SqlParameter("@EMAILID", (object)master.Emailid ?? DBNull.Value);
                SqlParameter CONTACTNUMBER = new SqlParameter("@CONTACTNUMBER", (object)master.Contactnumber ?? DBNull.Value);
                SqlParameter USERTYPEID = new SqlParameter("@USERTYPEID", master.Usertypeid);
                SqlParameter LOGINID = new SqlParameter("@LOGINID", (object)master.Loginid ?? DBNull.Value);
                SqlParameter PASSWORDS = new SqlParameter("@PASSWORDS", (object)master.Passwords ?? DBNull.Value);
                SqlParameter DIVISION = new SqlParameter("@DIVISION", (object)master.Division ?? DBNull.Value);
                SqlParameter BOOK = new SqlParameter("@BOOK", (object)master.Book ?? DBNull.Value);

                _context.Database.ExecuteSqlRaw("Exec SPGETUSERUPDATE @ID,@USERNAME,@EMAILID,@CONTACTNUMBER,@USERTYPEID,@LOGINID,@PASSWORDS,@DIVISION,@BOOK,@MSG OUTPUT,@STATUSMSG OUTPUT",
                    ID, USERNAME, EMAILID, CONTACTNUMBER, USERTYPEID, LOGINID, PASSWORDS, DIVISION, BOOK, MSG, STATUSMSG);

                strMsg = MSG.Value.ToString();
                strStatusMsg = STATUSMSG.Value.ToString();
                return Tuple.Create(strMsg, strStatusMsg);
            }
            catch (Exception ex)
            {
                return Tuple.Create(ex.Message, "error");
            }
        }

        public Tuple<string, string> Delete(int id)
        {
            try
            {
                var MSG = new SqlParameter { ParameterName = "@MSG", Size = 100, SqlDbType = System.Data.SqlDbType.VarChar, Direction = System.Data.ParameterDirection.Output };
                var STATUSMSG = new SqlParameter { ParameterName = "@STATUSMSG", Size = 100, SqlDbType = System.Data.SqlDbType.VarChar, Direction = System.Data.ParameterDirection.Output };

                SqlParameter ID = new SqlParameter("@ID", id);

                _context.Database.ExecuteSqlRaw("Exec SPDELETEUSER @ID,@MSG OUTPUT,@STATUSMSG OUTPUT", ID, MSG, STATUSMSG);

                strMsg = MSG.Value.ToString();
                strStatusMsg = STATUSMSG.Value.ToString();
                return Tuple.Create(strMsg, strStatusMsg);
            }
            catch (Exception ex)
            {
                return Tuple.Create(ex.Message, "error");
            }
        }
    }
}