using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TEALINK.GARDEN.Interface.SalaryGenerate;
using TEALINK.GARDEN.Model.DailyPlucking;
using TEALINK.GARDEN.Model.SalaryGenerate;
using static TEALINK.GARDEN.Model.Common.EnamProcess;

namespace TEALINK.GARDEN.Repository.SalaryGenerateRepository
{
    public class RSalaryGenerate : ISalaryGenerate
    {
        private readonly Context _context;
        string strMsg = "";
        string strStatusMsg = "";

        public RSalaryGenerate(Context context)
        {
            _context = context;
        }

        public Tuple<string, string> Get(string sdate, string edate, int unitid, int enroll)
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


                SqlParameter SDATE = new SqlParameter("@SDATE", sdate);
                SqlParameter EDATE = new SqlParameter("@EDATE", edate);
                SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
                SqlParameter ENROLL = new SqlParameter("@ENROLL", enroll);
                SqlParameter MSGCODE = new SqlParameter("@MSGCODE", EnumMessage.SAVE.ToString());
                SqlParameter MODULECODE = new SqlParameter("@MODULECODE", EnumSubMenuCode.SLGEN0001.ToString());

                _context.Database.ExecuteSqlRaw("Exec SPSALARYGENERATE @SDATE,@EDATE,@UNITID,@ENROLL,@MSGCODE,@MODULECODE,@MSG OUTPUT,@STATUSMSG OUTPUT", SDATE, EDATE, UNITID, ENROLL, MSGCODE, MODULECODE, MSG, STATUSMSG);


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

        //public IQueryable<SalaryGenerateModel> Create(string sdate, string edate, int unitid, int enroll)
        //{
        //    try
        //    {

        //        var MSG = new SqlParameter
        //        {
        //            ParameterName = "@MSG",
        //            Size = 100,
        //            SqlDbType = System.Data.SqlDbType.Char,
        //            Direction = System.Data.ParameterDirection.Output
        //        };


        //        var STATUSMSG = new SqlParameter
        //        {
        //            ParameterName = "@STATUSMSG",
        //            Size = 100,
        //            SqlDbType = System.Data.SqlDbType.Char,
        //            Direction = System.Data.ParameterDirection.Output
        //        };

        //        SqlParameter SDATE = new SqlParameter("@SDATE", sdate);
        //        SqlParameter EDATE = new SqlParameter("@EDATE", edate);
        //        SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
        //        SqlParameter ENROLL = new SqlParameter("@ENROLL", enroll);
        //        SqlParameter MSGCODE = new SqlParameter("@MSGCODE", EnumMessage.SAVE.ToString());
        //        SqlParameter MODULECODE = new SqlParameter("@MODULECODE", EnumSubMenuCode.SLGEN0001.ToString());

        //        _context.Database.ExecuteSqlRaw("Exec SPSALARYGENERATE @SDATE,@EDATE,@UNITID,@ENROLL,@MSGCODE,@MODULECODE,@MSG OUTPUT,@STATUSMSG OUTPUT", SDATE,EDATE,UNITID,ENROLL, MSGCODE, MODULECODE, MSG, STATUSMSG);


        //        strMsg = MSG.Value.ToString();
        //        strStatusMsg = STATUSMSG.Value.ToString();
        //        return Tuple.Create(strMsg, strStatusMsg);
        //    }
        //    catch (Exception ex)
        //    {
        //        // _common.ErrorLog("", ex.ToString(), enumIMPSubMenu.LCM00001.ToString(), enumIMPProcessName.LCM.ToString(), master.UNITID, master.DOCUMENTCREATEDBY, "");
        //    }
        //    return null;
        //}
    }
}

