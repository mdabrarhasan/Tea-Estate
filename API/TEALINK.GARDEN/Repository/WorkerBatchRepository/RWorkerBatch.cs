using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TEALINK.GARDEN.Interface.WorkerBatchManagement;
using static TEALINK.GARDEN.Model.Common.EnamProcess;
using TEALINK.GARDEN.Model.WorkerBook;
using TEALINK.GARDEN.Model.WorkerBatch;

namespace TEALINK.GARDEN.Repository.WorkerBatchRepository
{
    public class RWorkerBatch : IWorkerBatch
    {
        private readonly Context _context;
        string strMsg = "";
        string strStatusMsg = "";

        public RWorkerBatch(Context context)
        {
            _context = context;
        }

        public Tuple<string, string> Create(WorkerBatchMaster master)
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

                SqlParameter ID = new SqlParameter("@ID", master.ID);
                SqlParameter NAME = new SqlParameter("@NAME", master.BATCHNAME == null ? "" : master.BATCHNAME);
                SqlParameter KAMJARITYPEID = new SqlParameter("@KAMJARITYPEID", master.KAMJARITYPEID);
                SqlParameter SHIFTID = new SqlParameter("@SHIFTID", master.SHIFTID);
                SqlParameter DIVISIONID = new SqlParameter("@DIVISIONID", master.DIVISIONID);
                SqlParameter ISOUTSIDEWORKER = new SqlParameter("@ISOUTSIDEWORKER", master.ISOUTSIDEWORKER);

                SqlParameter ENROLL = new SqlParameter("@ENROLL", master.ENROLL);
                SqlParameter UNITID = new SqlParameter("@UNITID", master.UNITID);
                SqlParameter MSGCODE = new SqlParameter("@MSGCODE", EnumMessage.SAVE.ToString());
                SqlParameter MODULECODE = new SqlParameter("@MODULECODE", EnumSubMenuCode.WBTCH0001.ToString());


                //SqlParameter ENROLL = new SqlParameter("@ENROLL", 0);
                _context.Database.ExecuteSqlRaw("Exec SPWORKERBOOKBATCHSAVE @ID,@NAME,@KAMJARITYPEID,@SHIFTID,@DIVISIONID,@ISOUTSIDEWORKER,@ENROLL,@UNITID,@MSGCODE,@MODULECODE,@MSG OUTPUT,@STATUSMSG OUTPUT",
                    ID, NAME, KAMJARITYPEID, SHIFTID, DIVISIONID, @ISOUTSIDEWORKER, ENROLL, UNITID, MSGCODE, MODULECODE, MSG, STATUSMSG);


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
                SqlParameter MODULECODE = new SqlParameter("@MODULECODE", EnumSubMenuCode.WBTCH0001.ToString());
                SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
                SqlParameter ENROLL = new SqlParameter("@ENROLL", enroll);

                //SqlParameter ENROLL = new SqlParameter("@ENROLL", 0);
                _context.Database.ExecuteSqlRaw("Exec SPWORKERBATCHDELETE @ID,@UNITID,@ENROLL,@MSGCODE,@MODULECODE,@MSG OUTPUT,@STATUSMSG OUTPUT", ID, UNITID, ENROLL, MSGCODE, MODULECODE, MSG, STATUSMSG);


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

        public IQueryable<WorkerBatchMasterView> GetData(int unitid, int enroll)
        {
            SqlParameter TYPEID = new SqlParameter("@TYPEID", 1);
            SqlParameter ID = new SqlParameter("@ID", 1);
            SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
            SqlParameter ENROLL = new SqlParameter("@ENROLL", enroll);

            return _context.WorkerBatchMasterView.FromSqlRaw("Exec SPGETWORKERBATCHDATA @TYPEID,@ID,@UNITID,@ENROLL", TYPEID, ID, UNITID, ENROLL);
        }

        public IQueryable<WorkerBatchMasterView> GetDataByID(int id)
        {
            SqlParameter TYPEID = new SqlParameter("@TYPEID", 2);
            SqlParameter ID = new SqlParameter("@ID", id);
            SqlParameter UNITID = new SqlParameter("@UNITID", 1);
            SqlParameter ENROLL = new SqlParameter("@ENROLL", 1);

            return _context.WorkerBatchMasterView.FromSqlRaw("Exec SPGETWORKERBATCHDATA @TYPEID,@ID,@UNITID,@ENROLL", TYPEID, ID, UNITID, ENROLL);
        }

        public Tuple<string, string> Update(int id, WorkerBatchMaster master)
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

                SqlParameter ID = new SqlParameter("@ID", master.ID);
                SqlParameter NAME = new SqlParameter("@NAME", master.BATCHNAME == null ? "" : master.BATCHNAME);
                SqlParameter KAMJARITYPEID = new SqlParameter("@KAMJARITYPEID", master.KAMJARITYPEID);
                SqlParameter SHIFTID = new SqlParameter("@SHIFTID", master.SHIFTID);
                SqlParameter DIVISIONID = new SqlParameter("@DIVISIONID", master.DIVISIONID);
                SqlParameter ISOUTSIDEWORKER = new SqlParameter("@ISOUTSIDEWORKER", master.ISOUTSIDEWORKER);

                SqlParameter ENROLL = new SqlParameter("@ENROLL", master.ENROLL);
                SqlParameter UNITID = new SqlParameter("@UNITID", master.UNITID);
                SqlParameter MSGCODE = new SqlParameter("@MSGCODE", EnumMessage.EDIT.ToString());
                SqlParameter MODULECODE = new SqlParameter("@MODULECODE", EnumSubMenuCode.WBTCH0001.ToString());


                //SqlParameter ENROLL = new SqlParameter("@ENROLL", 0);
                _context.Database.ExecuteSqlRaw("Exec SPWORKERBOOKBATCHUPDATE @ID,@NAME,@KAMJARITYPEID,@SHIFTID,@DIVISIONID,@ISOUTSIDEWORKER,@ENROLL,@UNITID,@MSGCODE,@MODULECODE,@MSG OUTPUT,@STATUSMSG OUTPUT",
                    ID, NAME, KAMJARITYPEID, SHIFTID, DIVISIONID, ISOUTSIDEWORKER, ENROLL, UNITID, MSGCODE, MODULECODE, MSG, STATUSMSG);

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
