using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TEALINK.GARDEN.Interface.Holyday;
using TEALINK.GARDEN.Model.Holiday;
using TEALINK.GARDEN.Model.LeaveManagement;
using static TEALINK.GARDEN.Model.Common.EnamProcess;

namespace TEALINK.GARDEN.Repository.HolidayRepository
{
    public class RHoliday : IHoliday
    {
        private readonly Context _context;
        string strMsg = "";
        string strStatusMsg = "";

        public RHoliday(Context context)
        {
            _context = context;
        }
        public Tuple<string, string> Create(HolidayModel master)
        {
            try
            {
                var MSG = new SqlParameter
                {
                    ParameterName = "@MSG",
                    Size = 100,
                    SqlDbType = System.Data.SqlDbType.Char,
                    Direction = System.Data.ParameterDirection.Output
                };


                var STATUSMSG = new SqlParameter
                {
                    ParameterName = "@STATUSMSG",
                    Size = 100,
                    SqlDbType = System.Data.SqlDbType.Char,
                    Direction = System.Data.ParameterDirection.Output
                };

                SqlParameter Name = new SqlParameter("@Name", master.Name);
                
                SqlParameter FromDate = new SqlParameter("@FromDate", master.FromDate);
                SqlParameter ToDate = new SqlParameter("@ToDate", master.ToDate);
                
                _context.Database.ExecuteSqlRaw("Exec SPHOLIDAYSAVE @Name, @FromDate, @ToDate,@MSG OUTPUT,@STATUSMSG OUTPUT", Name, FromDate, ToDate, MSG, STATUSMSG);
                strMsg = MSG.Value.ToString();
                strStatusMsg = STATUSMSG.Value.ToString();
                return Tuple.Create(strMsg, strStatusMsg);
            }
            catch (Exception ex)
            {

            }
            return null;

        }

        public IQueryable<HolidayModel> GetData(int unitid, int enroll)
        {
            SqlParameter TYPEID = new SqlParameter("@TYPEID", 1);
            SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
            SqlParameter ENROLL = new SqlParameter("@ENROLL", enroll);

            return _context.HolidayModel.FromSqlRaw("Exec SPGETHOLIDAY @TYPEID,@UNITID,@ENROLL", TYPEID, UNITID, ENROLL);
        }
        public Tuple<string, string> Delete(int id, int unitid, int enroll)
        {
            try
            {
                var MSG = new SqlParameter
                {
                    ParameterName = "@MSG",
                    Size = 100,
                    SqlDbType = System.Data.SqlDbType.Char,
                    Direction = System.Data.ParameterDirection.Output
                };


                var STATUSMSG = new SqlParameter
                {
                    ParameterName = "@STATUSMSG",
                    Size = 100,
                    SqlDbType = System.Data.SqlDbType.Char,
                    Direction = System.Data.ParameterDirection.Output
                };

                SqlParameter ID = new SqlParameter("@ID", id);
                SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
                SqlParameter ENROLL = new SqlParameter("@ENROLL", enroll);

                SqlParameter MSGCODE = new SqlParameter("@MSGCODE", EnumMessage.DEL.ToString());
                SqlParameter MODULECODE = new SqlParameter("@MODULECODE", EnumSubMenuCode.DP0001.ToString());

                //SqlParameter ENROLL = new SqlParameter("@ENROLL", 0);
                _context.Database.ExecuteSqlRaw("Exec SPHOLIDAYDELETE @ID,@UNITID,@ENROLL,@MSGCODE,@MODULECODE,@MSG OUTPUT,@STATUSMSG OUTPUT", ID, UNITID, ENROLL, MSGCODE, MODULECODE, MSG, STATUSMSG);


                strMsg = MSG.Value.ToString();
                strStatusMsg = STATUSMSG.Value.ToString();
                return Tuple.Create(strMsg, strStatusMsg);
            }
            catch (Exception ex)
            {
                // _common.ErrorLog("", ex.ToString(), enumIMPSubMenu.LCM00001.ToString(), enumIMPProcessName.LCM.ToString(), master.UNITID, master.DOCUMENTCREATEDBY, "");
            }
            return null;
        }

    }
}
