using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TEALINK.GARDEN.Interface.LeaveMnagement;
using TEALINK.GARDEN.Model.LeaveManagement;
using static TEALINK.GARDEN.Model.Common.EnamProcess;

namespace TEALINK.GARDEN.Repository.LeaveManagementRepository
{
    public class RLeaveManagement : ILeaveMangement
    {
        private readonly Context _context;
        string strMsg = "";
        string strStatusMsg = "";
        public RLeaveManagement(Context context)
        {
            _context = context;
        }
        public Tuple<string, string> Create(LeaveMaster master)
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

                SqlParameter WORKER = new SqlParameter("@WORKER", master.WORKER);
                SqlParameter LEAVETYPE = new SqlParameter("@LEAVETYPE", master.LEAVETYPEID);
                SqlParameter FROMDATE = new SqlParameter("@FROMDATE", master.FROMDATE);
                SqlParameter TODATE = new SqlParameter("@TODATE", master.TODATE);
                SqlParameter DIVISIONID = new SqlParameter("@DIVISIONID", master.DIVISIONID);
                SqlParameter UNITID = new SqlParameter("@UNITID", master.UNITID);
                _context.Database.ExecuteSqlRaw("Exec SPLEAVESAVE @WORKER,@LEAVETYPE,@FROMDATE,@TODATE,@DIVISIONID,@UNITID,@MSG OUTPUT,@STATUSMSG OUTPUT", WORKER, LEAVETYPE, FROMDATE, TODATE, DIVISIONID, UNITID, MSG, STATUSMSG);
                strMsg = MSG.Value.ToString();
                strStatusMsg = STATUSMSG.Value.ToString();
                return Tuple.Create(strMsg, strStatusMsg);
            }
            catch (Exception ex)
            {

            }
            return null;

        }

        public IQueryable<LeaveGetdata> LeaveGet(int workerid, int leavetypeid)
        {
            SqlParameter WORKERID = new SqlParameter("@WORKERID", workerid);
            SqlParameter LEAVETYPEID = new SqlParameter("@LEAVETYPEID", leavetypeid);
            return _context.LeaveGetdata.FromSqlRaw("Exec SPLEAVEGETDATA  @WORKERID,@LEAVETYPEID", WORKERID, LEAVETYPEID);
        }
        public IQueryable<LeaveDetails> Getdata(int unitid)
        {
            SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
            return _context.LeaveDetails.FromSqlRaw("Exec SPLEAVEDETAILS @UNITID", UNITID);
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
                _context.Database.ExecuteSqlRaw("Exec SPLEAVEDELETE @ID,@UNITID,@ENROLL,@MSGCODE,@MODULECODE,@MSG OUTPUT,@STATUSMSG OUTPUT", ID, UNITID, ENROLL, MSGCODE, MODULECODE, MSG, STATUSMSG);


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