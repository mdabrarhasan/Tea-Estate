using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TEALINK.GARDEN.Interface.WorkLeave;
using TEALINK.GARDEN.Model.WorkerBook;
using TEALINK.GARDEN.Model.WorkerLeave;
using static TEALINK.GARDEN.Model.Common.EnamProcess;

namespace TEALINK.GARDEN.Repository.WorkLeaveRepository
{
    public class RWorkLeave:IWorkLeave
    {
        private readonly Context _context;
        string strMsg = "";
        string strStatusMsg = "";

        public RWorkLeave(Context context)
        {
            _context = context;
        }
        public Tuple<string, string> Create(WorkerLeaveMaster master)
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
                SqlParameter LEAVECODE = new SqlParameter("@LEAVECODE", master.LEAVECODE);
                SqlParameter ATTENDENCE = new SqlParameter("@ATTENDENCE", master.ATTENDENCE);
                SqlParameter MAXALLOWED = new SqlParameter("@MAXALLOWED", master.MAXALLOWED);
                SqlParameter ENROLL = new SqlParameter("@ENROLL", master.ENROLL);
                SqlParameter UNITID = new SqlParameter("@UNITID", master.UNITID);
                SqlParameter MSGCODE = new SqlParameter("@MSGCODE", EnumMessage.SAVE.ToString());
                SqlParameter MODULECODE = new SqlParameter("@MODULECODE", EnumSubMenuCode.WB0001.ToString());
                _context.Database.ExecuteSqlRaw("Exec SPWORKERLEAVESAVE @LEAVECODE,@ATTENDENCE,@MAXALLOWED,@ENROLL,@UNITID,@MSGCODE,@MODULECODE,@MSG OUTPUT,@STATUSMSG OUTPUT", LEAVECODE, ATTENDENCE, MAXALLOWED, @ENROLL, @UNITID, @MSGCODE, @MODULECODE, @MSG , @STATUSMSG );
                strMsg = MSG.Value.ToString();
                strStatusMsg = STATUSMSG.Value.ToString();
                return Tuple.Create(strMsg, strStatusMsg);

            }
            catch (Exception ex)
            {

            }
            return null;

        }
        public IQueryable<WorkerLeaveMaster> GetData(int unitid, int enroll)
        {
            SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
            SqlParameter ENROLL = new SqlParameter("@ENROLL", enroll);
            return _context.WorkerLeaveMaster.FromSqlRaw("Exec SPWORKERLEAVEGETDATA @UNITID,@ENROLL", UNITID, ENROLL);
        }
        public IQueryable<WorkerLeaveMaster> GetDataByID(int id)
        {
            
            SqlParameter ID = new SqlParameter("@ID", id);
           

            return _context.WorkerLeaveMaster.FromSqlRaw("Exec SPWORKERLEAVEVIEWGETDATA @ID",  ID);
        }
        public Tuple<string, string> Update(int id, WorkerLeaveMaster master)
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
                SqlParameter LEAVECODE = new SqlParameter("@LEAVECODE", master.LEAVECODE == null ? "" : master.LEAVECODE);
                SqlParameter ATTENDENCE = new SqlParameter("@ATTENDENCE", master.ATTENDENCE);
                SqlParameter MAXALLOWED = new SqlParameter("@MAXALLOWED", master.MAXALLOWED);
                SqlParameter ENROLL = new SqlParameter("@ENROLL", master.ENROLL);
                SqlParameter UNITID = new SqlParameter("@UNITID", master.UNITID);
                SqlParameter MSGCODE = new SqlParameter("@MSGCODE", EnumMessage.EDIT.ToString());
                SqlParameter MODULECODE = new SqlParameter("@MODULECODE", EnumSubMenuCode.WB0001.ToString());


                //SqlParameter ENROLL = new SqlParameter("@ENROLL", 0);
                _context.Database.ExecuteSqlRaw("Exec SPWORKERLEAVEUPDATE @ID,@LEAVECODE,@ATTENDENCE,@MAXALLOWED,@ENROLL,@UNITID,@MSGCODE,@MODULECODE,@MSG OUTPUT,@STATUSMSG OUTPUT", ID, LEAVECODE, ATTENDENCE, MAXALLOWED, ENROLL, UNITID, MSGCODE, MODULECODE, MSG, STATUSMSG);


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

                SqlParameter MSGCODE = new SqlParameter("@MSGCODE", EnumMessage.DEL.ToString());
                SqlParameter MODULECODE = new SqlParameter("@MODULECODE", EnumSubMenuCode.PRUNE0001.ToString());
                SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
                SqlParameter ENROLL = new SqlParameter("@ENROLL", enroll);

                //SqlParameter ENROLL = new SqlParameter("@ENROLL", 0);
                _context.Database.ExecuteSqlRaw("Exec SPWORKERLEAVEDELETE @ID,@UNITID,@ENROLL,@MSGCODE,@MODULECODE,@MSG OUTPUT,@STATUSMSG OUTPUT", ID, UNITID, ENROLL, MSGCODE, MODULECODE, MSG, STATUSMSG);


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
