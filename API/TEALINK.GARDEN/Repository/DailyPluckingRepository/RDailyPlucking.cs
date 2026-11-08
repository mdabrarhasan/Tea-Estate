using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TEALINK.GARDEN.Interface.DailyPlucking;

using TEALINK.GARDEN.Model.DailyPlucking;
using TEALINK.GARDEN.Model.Kamjari;
using static TEALINK.GARDEN.Model.Common.EnamProcess;
using static TEALINK.GARDEN.Model.DailyPlucking.DailyPluckingModel;

namespace TEALINK.GARDEN.Repository.DailyPluckingRepository
{
    public class RDailyPlucking : IDailyPlucking
    {
        private readonly Context _context;
        string strMsg = "";
        string strStatusMsg = "";
        int bookid = 0;

        public RDailyPlucking(Context context)
        {
            _context = context;
        }

        public Tuple<string, string> Create(List<DailyPluckingView> obj)
        {
            string pluckeddate = "";
            int unitid = 0;

            try
            {
                DataTable dt = new DataTable { TableName = "Details" };
                dt.Columns.Add("CODE", typeof(string));
                dt.Columns.Add("PLUCKEDDATE", typeof(string));
                dt.Columns.Add("UNITID", typeof(int));

                for (int i = 0; i < obj.Count(); i++)
                {
                    pluckeddate = obj[i].PLUCKEDDATE;
                    unitid = obj[i].UNITID;
                    bookid= obj[i].BOOKID;
                    dt.Rows.Add(obj[i].ID, obj[i].PLUCKEDDATE, obj[i].UNITID);

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
                detailsparam.ParameterName = "@TYPEDAILYPLUCKING";
                detailsparam.SqlDbType = SqlDbType.Structured;
                detailsparam.TypeName = "TYPEDAILYPLUCKING";
                detailsparam.SqlValue = dt;

                SqlParameter PLUCKINGDATE = new SqlParameter("@PLUCKINGDATE", pluckeddate);
                SqlParameter UNIT = new SqlParameter("@UNIT", unitid);
                SqlParameter BOOKID = new SqlParameter("@BOOKID", bookid);

                SqlParameter MSGCODE = new SqlParameter("@MSGCODE", EnumMessage.SAVE.ToString());
                SqlParameter MODULECODE = new SqlParameter("@MODULECODE", EnumSubMenuCode.DM0001.ToString());

                _context.Database.ExecuteSqlRaw("Exec SPDAILYPLUCKINGSAVE @PLUCKINGDATE,@UNIT,@BOOKID,@TYPEDAILYPLUCKING,@MSGCODE,@MODULECODE,@MSG OUTPUT,@STATUSMSG OUTPUT", PLUCKINGDATE, UNIT,BOOKID, detailsparam, MSGCODE, MODULECODE, MSG, STATUSMSG);

                strMsg = MSG.Value.ToString();
                strStatusMsg = STATUSMSG.Value.ToString();
                return Tuple.Create(strMsg, strStatusMsg);
            }
            catch (Exception ex)
            {
                
            }
            return null;
        }

        public Tuple<string, string> UpdateWeighment(int id, int weighment, int unitid, int enroll)
        {
            try
            {
                var MSG = new SqlParameter("@MSG", SqlDbType.VarChar, 200)
                { Direction = ParameterDirection.Output };

                var STATUSMSG = new SqlParameter("@STATUSMSG", SqlDbType.VarChar, 50)
                { Direction = ParameterDirection.Output };

                SqlParameter ID = new SqlParameter("@ID", id);
                SqlParameter WEIGHMENT = new SqlParameter("@WEIGHMENT", weighment);
                SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
                SqlParameter ENROLL = new SqlParameter("@ENROLL", enroll);

                SqlParameter MSGCODE = new SqlParameter("@MSGCODE", EnumMessage.EDIT.ToString());
                SqlParameter MODULECODE = new SqlParameter("@MODULECODE", EnumSubMenuCode.DP0001.ToString());

                _context.Database.ExecuteSqlRaw(
                    "Exec SPUPDATEWEIGHMENT @ID,@WEIGHMENT,@UNITID,@ENROLL,@MSGCODE,@MODULECODE,@MSG OUTPUT,@STATUSMSG OUTPUT",
                    ID, WEIGHMENT, UNITID, ENROLL, MSGCODE, MODULECODE, MSG, STATUSMSG
                );

                strMsg = MSG.Value?.ToString() ?? "";
                strStatusMsg = STATUSMSG.Value?.ToString() ?? "";

                return Tuple.Create(strMsg, strStatusMsg);
            }
            catch
            {
                return Tuple.Create("Unexpected error occurred", "error");
            }
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
                _context.Database.ExecuteSqlRaw("Exec SPDAILYPLUCKINGDELETE @ID,@UNITID,@ENROLL,@MSGCODE,@MODULECODE,@MSG OUTPUT,@STATUSMSG OUTPUT", ID, UNITID, ENROLL, MSGCODE, MODULECODE, MSG, STATUSMSG);


                strMsg = MSG.Value.ToString();
                strStatusMsg = STATUSMSG.Value.ToString();
                return Tuple.Create(strMsg, strStatusMsg);
            }
            catch (Exception ex)
            {
                
            }
            return null;
        }
        public IQueryable<DailyPluckingView> GetData(string pluckingdate, int kamjaritypeid, int bookid, int operatorid, int deviceid, int batchid, int weighmentid, int divisionid, int unitid, int enroll)
        {
            SqlParameter TYPEID = new SqlParameter("@TYPEID", 1);
            SqlParameter PLUCKINGDATE = new SqlParameter("@PLUCKINGDATE", pluckingdate);
            SqlParameter KAMJARITYPEID = new SqlParameter("@KAMJARITYPEID", kamjaritypeid);
            SqlParameter BOOKID = new SqlParameter("@BOOKID", bookid);
            SqlParameter OPERATORID = new SqlParameter("@OPERATORID", operatorid);
            SqlParameter DEVICEID = new SqlParameter("@DEVICEID", deviceid);
            SqlParameter BATCHID = new SqlParameter("@BATCHID", batchid);
            SqlParameter WEIGHMENTID = new SqlParameter("@WEIGHMENTID", weighmentid);
            SqlParameter DIVISIONID = new SqlParameter("@DIVISIONID", divisionid);
            SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
            SqlParameter ENROLL = new SqlParameter("@ENROLL", enroll);
            return _context.DailyPluckingView.FromSqlRaw("Exec SPGETDAILYPLUCKING @TYPEID,@PLUCKINGDATE, @KAMJARITYPEID, @BOOKID, @OPERATORID, @DEVICEID, @BATCHID, @WEIGHMENTID, @DIVISIONID, @UNITID, @ENROLL", TYPEID,PLUCKINGDATE, KAMJARITYPEID, BOOKID, OPERATORID, DEVICEID, BATCHID, WEIGHMENTID, DIVISIONID, UNITID, ENROLL);
        }

        public IQueryable<DailyPluckingLineView> GetDataByID(string id, string pluckeddate, int unitid, int enroll)
        {
            SqlParameter CODE = new SqlParameter("@CODE", id);
            SqlParameter PLUCKEDDATE = new SqlParameter("@PLUCKEDDATE", pluckeddate == null ? "" : pluckeddate);
            SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
            SqlParameter ENROLL = new SqlParameter("@ENROLL", enroll);
            return _context.DailyPluckingLineView.FromSqlRaw("Exec SPGETPLUCKEDLINE @CODE,@PLUCKEDDATE,@UNITID, @ENROLL", CODE, PLUCKEDDATE, UNITID, ENROLL);
        }

        public IQueryable<DailyPluckingByWeighment> GetQtyWeighmentWise(string pluckeddate,int bookid, int unitid)
        {
            SqlParameter BOOKID = new SqlParameter("@BOOKID", bookid);
            SqlParameter PLUCKEDDATE = new SqlParameter("@PLUCKEDDATE", pluckeddate == null ? "" : pluckeddate);
            SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
            return _context.DailyPluckingByWeighment.FromSqlRaw("Exec SPGETWEIGHMNT @PLUCKEDDATE,@BOOKID,@UNITID", PLUCKEDDATE,BOOKID, UNITID);
        }

        public IQueryable<DailyPluckingAttendentEmp> GetDataCountDateWise(string pluckeddate, int bookid, int unitid)
        {
            SqlParameter BOOKID = new SqlParameter("@BOOKID", bookid);
            SqlParameter PLUCKEDDATE = new SqlParameter("@PLUCKEDDATE", pluckeddate == null ? "" : pluckeddate);
            SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
            return _context.DailyPluckingAttendentEmp.FromSqlRaw("Exec SPGETEMPCOUNT @PLUCKEDDATE,@BOOKID,@UNITID", PLUCKEDDATE, BOOKID, UNITID);
        }
    }
}
