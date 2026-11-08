using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TEALINK.GARDEN.Interface.WageCalender;
using static TEALINK.GARDEN.Model.Common.EnamProcess;
using TEALINK.GARDEN.Model.ShiftManagement;
using TEALINK.GARDEN.Model.WageCalender;
using DocumentFormat.OpenXml.InkML;

namespace TEALINK.GARDEN.Repository.WageCalenderRepository
{
    public class RWageCalender : IWageCalender
    {
        private readonly Context _context;
        string strMsg = "";
        string strStatusMsg = "";


        public RWageCalender(Context context)
        {
            _context = context;
        }

        public Tuple<string, string> Create(WageCalenderModel master)
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
                SqlParameter FROMDATE = new SqlParameter("@FROMDATE", master.FROMDATE == null ? "" : master.FROMDATE);
                SqlParameter TODATE = new SqlParameter("@TODATE", master.TODATE == null ? "" : master.TODATE);
                
                SqlParameter WEEKNUMBER = new SqlParameter("@WEEKNUMBER", master.WEEKNUMBER ?? "");
                SqlParameter WEEKLYHOLIDAY = new SqlParameter("@WEEKLYHOLIDAY", master.WEEKLYHOLIDAY == null ? "" : master.WEEKLYHOLIDAY);
                SqlParameter DISABLEWEEKLYHOLIDAY = new SqlParameter("@DISABLEWEEKLYHOLIDAY", master.DISABLEWEEKLYHOLIDAY);
                SqlParameter WAGEDISBURSED = new SqlParameter("@WAGEDISBURSED", master.WAGEDISBURSED);
                SqlParameter ENROLL = new SqlParameter("@ENROLL", master.ENROLL);
                SqlParameter UNITID = new SqlParameter("@UNITID", master.UNITID);
                SqlParameter MSGCODE = new SqlParameter("@MSGCODE", EnumMessage.SAVE.ToString());
                SqlParameter MODULECODE = new SqlParameter("@MODULECODE", EnumSubMenuCode.WC0001.ToString());


                //SqlParameter ENROLL = new SqlParameter("@ENROLL", 0);
                _context.Database.ExecuteSqlRaw("Exec SPWAGECALENDERSAVE @ID,@FROMDATE,@TODATE,@WEEKNUMBER,@WEEKLYHOLIDAY,@DISABLEWEEKLYHOLIDAY,@WAGEDISBURSED,@ENROLL, @UNITID,@MSGCODE,@MODULECODE,@MSG OUTPUT,@STATUSMSG OUTPUT", ID, FROMDATE, TODATE, WEEKNUMBER, WEEKLYHOLIDAY, DISABLEWEEKLYHOLIDAY, WAGEDISBURSED, ENROLL, UNITID, MSGCODE, MODULECODE, MSG, STATUSMSG);


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
                _context.Database.ExecuteSqlRaw("Exec SPWORKERBOOKDELETE @ID,@UNITID,@ENROLL,@MSGCODE,@MODULECODE,@MSG OUTPUT,@STATUSMSG OUTPUT", ID, UNITID, ENROLL, MSGCODE, MODULECODE, MSG, STATUSMSG);


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

        public IQueryable<WageCalenderModel> GetData(int unitid, int enroll)
        {
            SqlParameter TYPEID = new SqlParameter("@TYPEID", 1);
            SqlParameter ID = new SqlParameter("@ID", 1);
            SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
            SqlParameter ENROLL = new SqlParameter("@ENROLL", enroll);

            return _context.WageCalenderModel.FromSqlRaw("Exec SPGETWAGECALENDER @TYPEID,@ID,@UNITID,@ENROLL", TYPEID, ID, UNITID, ENROLL);
        }

        public IQueryable<WageCalenderModel> GetDataByID(int id, int unitid)
        {
            SqlParameter TYPEID = new SqlParameter("@TYPEID", 2);
            SqlParameter ID = new SqlParameter("@ID", id);
            SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
            SqlParameter ENROLL = new SqlParameter("@ENROLL", 1);

            return _context.WageCalenderModel.FromSqlRaw("Exec SPGETWAGECALENDER @TYPEID,@ID,@UNITID,@ENROLL", TYPEID, ID, UNITID, ENROLL);
        }

        public Tuple<string, string> Update(int id, WageCalenderModel master)
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
                SqlParameter FROMDATE = new SqlParameter("@FROMDATE", master.FROMDATE == null ? "" : master.FROMDATE);
                SqlParameter TODATE = new SqlParameter("@TODATE", master.TODATE == null ? "" : master.TODATE);
                
                SqlParameter WEEKNUMBER = new SqlParameter("@WEEKNUMBER", master.WEEKNUMBER ?? "");
                SqlParameter WEEKLYHOLIDAY = new SqlParameter("@WEEKLYHOLIDAY", master.WEEKLYHOLIDAY);
                SqlParameter DISABLEWEEKLYHOLIDAY = new SqlParameter("@DISABLEWEEKLYHOLIDAY", master.DISABLEWEEKLYHOLIDAY);
                SqlParameter WAGEDISBURSED = new SqlParameter("@WAGEDISBURSED", master.WAGEDISBURSED);
                SqlParameter ENROLL = new SqlParameter("@ENROLL", master.ENROLL);
                SqlParameter UNITID = new SqlParameter("@UNITID", master.UNITID);
                SqlParameter MSGCODE = new SqlParameter("@MSGCODE", EnumMessage.EDIT.ToString());
                SqlParameter MODULECODE = new SqlParameter("@MODULECODE", EnumSubMenuCode.WC0001.ToString());


                //SqlParameter ENROLL = new SqlParameter("@ENROLL", 0);
                _context.Database.ExecuteSqlRaw("Exec SPWAGECALENDEUPDATE @ID,@FROMDATE,@TODATE,@WEEKNUMBER,@WEEKLYHOLIDAY,@DISABLEWEEKLYHOLIDAY,@WAGEDISBURSED,@ENROLL, @UNITID,@MSGCODE,@MODULECODE,@MSG OUTPUT,@STATUSMSG OUTPUT", ID, FROMDATE, TODATE, WEEKNUMBER, WEEKLYHOLIDAY, DISABLEWEEKLYHOLIDAY, WAGEDISBURSED, ENROLL, UNITID, MSGCODE, MODULECODE, MSG, STATUSMSG);


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


        public async Task<Tuple<string, string>> SalaryGenerate(WageCalenderModel master)
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
                SqlParameter FROMDATE = new SqlParameter("@FROMDATE", master.FROMDATE == null ? "" : master.FROMDATE);
                SqlParameter TODATE = new SqlParameter("@TODATE", master.TODATE == null ? "" : master.TODATE);
                
                SqlParameter WEEKNUMBER = new SqlParameter("@WEEKNUMBER", master.WEEKNUMBER ?? "");
                SqlParameter WEEKLYHOLIDAY = new SqlParameter("@WEEKLYHOLIDAY", master.WEEKLYHOLIDAY);
                SqlParameter DISABLEWEEKLYHOLIDAY = new SqlParameter("@DISABLEWEEKLYHOLIDAY", master.DISABLEWEEKLYHOLIDAY);
                SqlParameter WAGEDISBURSED = new SqlParameter("@WAGEDISBURSED", master.WAGEDISBURSED);
                SqlParameter ENROLL = new SqlParameter("@ENROLL", master.ENROLL);
                SqlParameter UNITID = new SqlParameter("@UNITID", master.UNITID);
                SqlParameter MSGCODE = new SqlParameter("@MSGCODE", EnumMessage.SAVE.ToString());
                SqlParameter MODULECODE = new SqlParameter("@MODULECODE", EnumSubMenuCode.WC0001.ToString());

                _context.Database.SetCommandTimeout(3000000);
                //SqlParameter ENROLL = new SqlParameter("@ENROLL", 0);
                await _context.Database.ExecuteSqlRawAsync("Exec SPWEEKLYSALARYGENERATE @ID,@FROMDATE,@TODATE,@WEEKNUMBER,@WEEKLYHOLIDAY,@DISABLEWEEKLYHOLIDAY,@WAGEDISBURSED,@ENROLL, @UNITID,@MSGCODE,@MODULECODE,@MSG OUTPUT,@STATUSMSG OUTPUT", ID, FROMDATE, TODATE, WEEKNUMBER, WEEKLYHOLIDAY, DISABLEWEEKLYHOLIDAY, WAGEDISBURSED, ENROLL, UNITID, MSGCODE, MODULECODE, MSG, STATUSMSG);
               

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

        public Tuple<string, string> SalaryCancel(int unitid,  int bookid)
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

                SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
                
                SqlParameter BOOKID = new SqlParameter("@BOOKID", bookid);

                //SqlParameter ENROLL = new SqlParameter("@ENROLL", 0);
                _context.Database.ExecuteSqlRaw("Exec SPGETSALARYCANCEL @UNITID,@BOOKID,@MSG OUTPUT,@STATUSMSG OUTPUT", UNITID,  BOOKID, MSG, STATUSMSG);


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
