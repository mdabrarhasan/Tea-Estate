using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using static TEALINK.GARDEN.Model.Common.EnamProcess;
using System.Data;
using TEALINK.GARDEN.Model.DailyNonPlucking;
using TEALINK.GARDEN.Interface.DailyNonPlucking;

namespace TEALINK.GARDEN.Repository.DailyNonPluckingRepository
{
    public class RDailyNonPlucking : IDailyNonPlucking
    {

        private readonly Context _context;
        string strMsg = "";
        string strStatusMsg = "";


        public RDailyNonPlucking(Context context)
        {
            _context = context;
        }


        public IQueryable<DailyNonPluckingView> GetData(string pluckingdate, int? kamjaritypeid, int? bookid, int? operatorid, int? deviceid, int? batchid, int? recordid, int? divisionid, int? unitid, int? enroll)
        {
            try
            {
                SqlParameter TYPEID = new SqlParameter("@TYPEID", 2);
                SqlParameter PLUCKINGDATE = new SqlParameter("@PLUCKINGDATE", pluckingdate);
                SqlParameter KAMJARITYPEID = new SqlParameter("@KAMJARITYPEID", kamjaritypeid);
                SqlParameter BOOKID = new SqlParameter("@BOOKID", bookid);
                SqlParameter OPERATORID = new SqlParameter("@OPERATORID", operatorid);
                SqlParameter DEVICEID = new SqlParameter("@DEVICEID", deviceid);
                SqlParameter BATCHID = new SqlParameter("@BATCHID", batchid);
                SqlParameter RECORDID = new SqlParameter("@RECORDID", recordid);
                SqlParameter DIVISIONID = new SqlParameter("@DIVISIONID", divisionid);
                SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
                SqlParameter ENROLL = new SqlParameter("@ENROLL", enroll);
                return _context.DailyNonPluckingView.FromSqlRaw("Exec SPGETNONDAILYPLUCKING @TYPEID,@PLUCKINGDATE, @KAMJARITYPEID, @BOOKID, @OPERATORID, @DEVICEID, @BATCHID, @RECORDID, @DIVISIONID, @UNITID, @ENROLL", TYPEID, PLUCKINGDATE, KAMJARITYPEID, BOOKID, OPERATORID, DEVICEID, BATCHID, RECORDID, DIVISIONID, UNITID, ENROLL);
            }
            catch(Exception ex)
            {

            }
            return null;
           
        }
        
        public async Task <Tuple<string, string>> Create( List<DailyNonPluckingView> obj)
        {
            string date = "";
            int unitid = 0;
            int wid = 0;

            try
            {
                // SqlParameter ENROLL = new SqlParameter("@ENROLL", enroll);
                DataTable dt = new DataTable { TableName = "Details" };
                dt.Columns.Add("ID", typeof(string));
                dt.Columns.Add("PLUCKEDDATE", typeof(string));
                dt.Columns.Add("UNITID", typeof(int));
                dt.Columns.Add("DBLY", typeof(int));

                for (int i = 0; i < obj.Count(); i++)
                {
                    date = obj[i].PLUCKEDDATE;
                    unitid = obj[i].UNITID;
                    wid = obj[i].WID;

                    dt.Rows.Add(obj[i].ID, obj[i].PLUCKEDDATE, obj[i].UNITID, obj[i].DBLY);
                }


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

                SqlParameter detailsparam = new SqlParameter();
                detailsparam.ParameterName = "@TYPENONDAILYPLUCKING";
                detailsparam.SqlDbType = SqlDbType.Structured;
                detailsparam.TypeName = "TYPENONDAILYPLUCKING";
                detailsparam.SqlValue = dt;


                SqlParameter DATE = new SqlParameter("@DATE", date);
                SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
                SqlParameter WID = new SqlParameter("@WID", wid);

                SqlParameter MSGCODE = new SqlParameter("@MSGCODE", EnumMessage.SAVE.ToString());
                SqlParameter MODULECODE = new SqlParameter("@MODULECODE", EnumSubMenuCode.DM0001.ToString());

               await _context.Database.ExecuteSqlRawAsync("Exec SPNONDAILYPLUCKINGSAVE @WID,@DATE,@UNITID,@MSGCODE,@MODULECODE,@MSG OUTPUT,@STATUSMSG OUTPUT", WID, DATE, UNITID, MSGCODE, MODULECODE, MSG, STATUSMSG);
                //_context.Database.ExecuteSqlRaw("Exec SPNONDAILYPLUCKINGSAVE @TYPENONDAILYPLUCKING,@WID,@DATE,@UNITID,@MSGCODE,@MODULECODE,@MSG OUTPUT,@STATUSMSG OUTPUT", detailsparam, WID, DATE, UNITID, MSGCODE, MODULECODE, MSG, STATUSMSG);


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
                SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
                SqlParameter ENROLL = new SqlParameter("@ENROLL", enroll);

                SqlParameter MSGCODE = new SqlParameter("@MSGCODE", EnumMessage.DEL.ToString());
                SqlParameter MODULECODE = new SqlParameter("@MODULECODE", EnumSubMenuCode.DP0001.ToString());

                
                _context.Database.ExecuteSqlRaw("Exec SPNONDAILYPLUCKINGDELETE @ID,@UNITID,@ENROLL,@MSGCODE,@MODULECODE,@MSG OUTPUT,@STATUSMSG OUTPUT", ID, UNITID, ENROLL, MSGCODE, MODULECODE, MSG, STATUSMSG);


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
        public Tuple<string, string> KamjariUpdate(int id, int kamjariid,int teamid)
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
                SqlParameter KAMJARIID = new SqlParameter("@KAMJARIID", kamjariid);
                SqlParameter TEAMID = new SqlParameter("@TEAMID", teamid);
                

                SqlParameter MSGCODE = new SqlParameter("@MSGCODE", EnumMessage.DEL.ToString());
                SqlParameter MODULECODE = new SqlParameter("@MODULECODE", EnumSubMenuCode.DP0001.ToString());

                
                _context.Database.ExecuteSqlRaw("Exec SPNONDAILYPLUCKINGKAMJARIUPDATE @ID,@KAMJARIID,@TEAMID,@MSG OUTPUT,@STATUSMSG OUTPUT", ID, KAMJARIID, TEAMID, MSGCODE, MODULECODE, MSG, STATUSMSG);


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

        public IQueryable<DailyNonPluckingAttendentEmp> GetDataCountDateWise(string pluckeddate, int unitid)
        {
            SqlParameter PLUCKEDDATE = new SqlParameter("@PLUCKEDDATE", pluckeddate == null ? "" : pluckeddate);
            SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
            return _context.DailyNonPluckingAttendentEmp.FromSqlRaw("Exec SPGETNONEMPCOUNT @PLUCKEDDATE,@UNITID", PLUCKEDDATE, UNITID);
        }

        IQueryable<DailyNonPluckingLineView> IDailyNonPlucking.GetDataByID(int id, string pluckeddate, int unitid, int enroll)
        {
            SqlParameter CODE = new SqlParameter("@CODE", id);
            SqlParameter PLUCKEDDATE = new SqlParameter("@PLUCKEDDATE", pluckeddate);
            SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
            SqlParameter ENROLL = new SqlParameter("@ENROLL", enroll);
            return _context.DailyNonPluckingLineView.FromSqlRaw("Exec SPGETNONPLUCKEDLINE @CODE,@PLUCKEDDATE,@UNITID, @ENROLL", CODE, PLUCKEDDATE, UNITID, ENROLL);
        } 
        
    }
}
