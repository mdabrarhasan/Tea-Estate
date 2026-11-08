using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TEALINK.GARDEN.Interface.WorkerManagement;
using TEALINK.GARDEN.Model.WorkerBook;
using TEALINK.GARDEN.Model.WorkerManagement;
using static TEALINK.GARDEN.Model.Common.EnamProcess;

namespace TEALINK.GARDEN.Repository.WorkerManagementRepository
{
    public class RWorkerManagement : IWorkerManagement
    {
        private readonly Context _context;
        string strMsg = "";
        string strStatusMsg = "";

        public RWorkerManagement(Context context)
        {
            _context = context;
        }

        public Tuple<string, string> Create(WorkerManagementMaster master)
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
                SqlParameter NAME = new SqlParameter("@NAME", master.NAME == null ? "" : master.NAME);
                SqlParameter CODE = new SqlParameter("@CODE", master.CODE);
                SqlParameter PFNUMBER = new SqlParameter("@PFNUMBER", master.PFNUMBER == null ? "" : master.PFNUMBER);
                SqlParameter DIVISIONID = new SqlParameter("@DIVISIONID", master.DIVISIONID);
                SqlParameter EMPNO = new SqlParameter("@EMPNO", master.EMPNO);
                SqlParameter GENDERID = new SqlParameter("@GENDERID", master.GENDERID);
                SqlParameter WORKERTYPEID = new SqlParameter("@WORKERTYPEID", master.WORKERTYPEID);
                SqlParameter WORKERSUBTYPEID = new SqlParameter("@WORKERSUBTYPEID", master.WORKERSUBTYPEID);
                SqlParameter JOBSTATION = new SqlParameter("@JOBSTATION", master.JOBSTATION);
                SqlParameter WORKGROUP = new SqlParameter("@WORKGROUP", master.WORKGROUP);


                SqlParameter BOOKID = new SqlParameter("@BOOKID", master.BOOKID);
                SqlParameter DISABLEDDISINCENTIVEID = new SqlParameter("@DISABLEDDISINCENTIVEID", master.DISABLEDDISINCENTIVEID);
                SqlParameter MONTHLYRATED = new SqlParameter("@MONTHLYRATED", master.MONTHLYRATED);
                SqlParameter ESSENTIALWORKER = new SqlParameter("@ESSENTIALWORKER", master.ESSENTIALWORKER);
                SqlParameter RETIREDID = new SqlParameter("@RETIREDID", master.RETIREDID);
                SqlParameter PLUCKINGBATCHID = new SqlParameter("@PLUCKINGBATCHID", master.PLUCKINGBATCHID);
                SqlParameter BATCHID = new SqlParameter("@BATCHID", master.BATCHID);



                SqlParameter KAMJARIID = new SqlParameter("@KAMJARIID", master.KAMJARIID);
                SqlParameter DATEOFBIRTH = new SqlParameter("@DATEOFBIRTH", master.DATEOFBIRTH);
                SqlParameter ADDRESS = new SqlParameter("@ADDRESS", master.ADDRESS);
                SqlParameter CONTACTNUMBER = new SqlParameter("@CONTACTNUMBER", master.CONTACTNUMBER);

                SqlParameter ENROLL = new SqlParameter("@ENROLL", master.ENROLL);
                SqlParameter UNITID = new SqlParameter("@UNITID", master.UNITID);

                SqlParameter MSGCODE = new SqlParameter("@MSGCODE", EnumMessage.SAVE.ToString());
                SqlParameter MODULECODE = new SqlParameter("@MODULECODE", EnumSubMenuCode.WB0001.ToString());


                //SqlParameter ENROLL = new SqlParameter("@ENROLL", 0);
                _context.Database.ExecuteSqlRaw("Exec SPWORKERMANAGEMENTSAVE @ID,@WORKGROUP,@JOBSTATION,@NAME,@CODE,@PFNUMBER,@DIVISIONID,@EMPNO,@GENDERID,@WORKERTYPEID,@WORKERSUBTYPEID,@BOOKID,@DISABLEDDISINCENTIVEID,@MONTHLYRATED,@ESSENTIALWORKER,@RETIREDID,@PLUCKINGBATCHID,@BATCHID,@KAMJARIID,@DATEOFBIRTH,@ADDRESS,@CONTACTNUMBER,@ENROLL,@UNITID,@MSGCODE,@MODULECODE,@MSG OUTPUT,@STATUSMSG OUTPUT", ID, WORKGROUP, JOBSTATION, NAME, CODE, PFNUMBER, DIVISIONID, EMPNO, GENDERID, WORKERTYPEID, WORKERSUBTYPEID, BOOKID, DISABLEDDISINCENTIVEID, MONTHLYRATED, ESSENTIALWORKER, RETIREDID, PLUCKINGBATCHID, BATCHID, KAMJARIID, DATEOFBIRTH, ADDRESS, CONTACTNUMBER,ENROLL,UNITID, MSGCODE, MODULECODE, MSG, STATUSMSG);


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

        public Tuple<string, string> Create(List<WorkerExcelData> obj, int unitid)
        {
            try
            {
                DataTable dt = new DataTable { TableName = "WorkerDetails" };
                dt.Columns.Add("NAME", typeof(string));
                dt.Columns.Add("CODE", typeof(string));
                dt.Columns.Add("PFNUMBER", typeof(string));
                dt.Columns.Add("DIVISION", typeof(string));
                dt.Columns.Add("GENDER", typeof(string));
                dt.Columns.Add("WORKERTYPE", typeof(string));
                dt.Columns.Add("BOOK", typeof(string));
                dt.Columns.Add("RETIREDID", typeof(bool));
                dt.Columns.Add("DATEOFBIRTH", typeof(string));
                dt.Columns.Add("ADDRESS", typeof(string));
                dt.Columns.Add("CONTACTNUMBER", typeof(string));
                dt.Columns.Add("ISACTIVE", typeof(bool));

                for (int i = 0; i < obj.Count(); i++)
                {
                    dt.Rows.Add(
                        obj[i].NAME,
                        obj[i].CODE,
                        obj[i].PFNUMBER,
                        obj[i].DIVISION,
                        obj[i].GENDER,
                        obj[i].WORKERTYPE,
                        obj[i].BOOK,
                        obj[i].RETIREDID,
                        obj[i].DATEOFBIRTH,
                        obj[i].ADDRESS,
                        obj[i].CONTACTNUMBER,
                        obj[i].ISACTIVE
                    );
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
                detailsparam.ParameterName = "@TYPEWORKEREXCEL";
                detailsparam.SqlDbType = SqlDbType.Structured;
                detailsparam.TypeName = "TYPEWORKEREXCEL";  
                detailsparam.SqlValue = dt;
                SqlParameter UNIT = new SqlParameter("@UNIT", unitid);

                SqlParameter MSGCODE = new SqlParameter("@MSGCODE", EnumMessage.SAVE.ToString());
                SqlParameter MODULECODE = new SqlParameter("@MODULECODE", EnumSubMenuCode.WB0001.ToString());

                _context.Database.ExecuteSqlRaw("Exec SPWORKEREXCELSAVE @UNIT, @TYPEWORKEREXCEL,@MSGCODE,@MODULECODE,@MSG OUTPUT,@STATUSMSG OUTPUT",
                   UNIT, detailsparam, MSGCODE, MODULECODE, MSG, STATUSMSG);

                strMsg = MSG.Value.ToString();
                strStatusMsg = STATUSMSG.Value.ToString();
                return Tuple.Create(strMsg, strStatusMsg);
            }
            catch (Exception ex)
            {
                strMsg = "error";
                strStatusMsg = ex.ToString();
            }
            return Tuple.Create(strMsg, strStatusMsg);
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
                SqlParameter MODULECODE = new SqlParameter("@MODULECODE", EnumSubMenuCode.WM0001.ToString());
                SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
                SqlParameter ENROLL = new SqlParameter("@ENROLL", enroll);

                //SqlParameter ENROLL = new SqlParameter("@ENROLL", 0);
                _context.Database.ExecuteSqlRaw("Exec SPWORKERMANAGEMENTDELETE @ID,@UNITID,@ENROLL,@MSGCODE,@MODULECODE,@MSG OUTPUT,@STATUSMSG OUTPUT", ID, UNITID, ENROLL, MSGCODE, MODULECODE, MSG, STATUSMSG);


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

        public Tuple<string, string> GetDailyAttendeceDelete(int unitid, string date, int bookid)
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
                SqlParameter DATE = new SqlParameter("@DATE", date);
                SqlParameter BOOKID = new SqlParameter("@BOOKID", bookid);

                //SqlParameter ENROLL = new SqlParameter("@ENROLL", 0);
                _context.Database.ExecuteSqlRaw("Exec SPGETDAILYATTENDENCEDETAILSDELETE @UNITID,@DATE,@BOOKID,@MSG OUTPUT,@STATUSMSG OUTPUT", UNITID, DATE, BOOKID, MSG, STATUSMSG);


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

        public IQueryable<WorkerManagementView> GetData(int unitid, int enroll)
        {
            SqlParameter TYPEID = new SqlParameter("@TYPEID", 1);
            SqlParameter ID = new SqlParameter("@ID", 1);
            SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
            SqlParameter ENROLL = new SqlParameter("@ENROLL", enroll);

            return _context.WorkerManagementView.FromSqlRaw("Exec SPGETEMPDATA @TYPEID,@ID,@UNITID,@ENROLL", TYPEID, ID, UNITID, ENROLL);
        }

        public IQueryable<DailyAttendenceReport> GetDailyAttendence(int unitid, string date, int bookid)
        {
            SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
            SqlParameter DATE = new SqlParameter("@DATE", date);
            SqlParameter BOOKID = new SqlParameter("@BOOKID", bookid);
           // SqlParameter ENROLL = new SqlParameter("@ENROLL", enroll);

            return _context.DailyAttendenceReport.FromSqlRaw("Exec SPGETDAILYATTENDENCEDETAILS @UNITID,@DATE,@BOOKID", UNITID, DATE, BOOKID);
        }
        public IQueryable<AttendenceDetails> GetAttendenceDetails(int unitid, int daterangeid, int bookid)
        {
            SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
            SqlParameter DATARANGEID = new SqlParameter("@DATARANGEID", daterangeid);
            SqlParameter BOOKID = new SqlParameter("@BOOKID", bookid);
           // SqlParameter ENROLL = new SqlParameter("@ENROLL", enroll);

            return _context.AttendenceDetails.FromSqlRaw("Exec SPGETATTENDENCEDETAILS  @BOOKID,@DATARANGEID,@UNITID", BOOKID, DATARANGEID, UNITID);
        }

        public IQueryable<WorkerManagementView> GetDataByID(int id)
        {
            SqlParameter TYPEID = new SqlParameter("@TYPEID", 2);
            SqlParameter ID = new SqlParameter("@ID", id);
            SqlParameter UNITID = new SqlParameter("@UNITID", 1);
            SqlParameter ENROLL = new SqlParameter("@ENROLL", 1);

            return _context.WorkerManagementView.FromSqlRaw("Exec SPGETEMPDATA @TYPEID,@ID,@UNITID,@ENROLL", TYPEID, ID, UNITID, ENROLL);
        }

        public Tuple<string, string> Update(int id, WorkerManagementMaster master)
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
                SqlParameter NAME = new SqlParameter("@NAME", master.NAME == null ? "" : master.NAME);
                SqlParameter CODE = new SqlParameter("@CODE", master.CODE);
                SqlParameter PFNUMBER = new SqlParameter("@PFNUMBER", master.PFNUMBER == null ? "" : master.PFNUMBER);
                SqlParameter DIVISIONID = new SqlParameter("@DIVISIONID", master.DIVISIONID);
                SqlParameter EMPNO = new SqlParameter("@EMPNO", master.EMPNO);
                SqlParameter GENDERID = new SqlParameter("@GENDERID", master.GENDERID);
                SqlParameter WORKERTYPEID = new SqlParameter("@WORKERTYPEID", master.WORKERTYPEID);
                SqlParameter WORKERSUBTYPEID = new SqlParameter("@WORKERSUBTYPEID", master.WORKERSUBTYPEID);
                SqlParameter JOBSTATION = new SqlParameter("@JOBSTATION", master.JOBSTATION);
                SqlParameter WORKGROUP = new SqlParameter("@WORKGROUP", master.WORKGROUP);

                SqlParameter BOOKID = new SqlParameter("@BOOKID", master.BOOKID);
                SqlParameter DISABLEDDISINCENTIVEID = new SqlParameter("@DISABLEDDISINCENTIVEID", master.DISABLEDDISINCENTIVEID);
                SqlParameter MONTHLYRATED = new SqlParameter("@MONTHLYRATED", master.MONTHLYRATED);
                SqlParameter ESSENTIALWORKER = new SqlParameter("@ESSENTIALWORKER", master.ESSENTIALWORKER);
                SqlParameter RETIREDID = new SqlParameter("@RETIREDID", master.RETIREDID);
                SqlParameter PLUCKINGBATCHID = new SqlParameter("@PLUCKINGBATCHID", master.PLUCKINGBATCHID);
                SqlParameter BATCHID = new SqlParameter("@BATCHID", master.BATCHID);



                SqlParameter KAMJARIID = new SqlParameter("@KAMJARIID", master.KAMJARIID);
                SqlParameter DATEOFBIRTH = new SqlParameter("@DATEOFBIRTH", master.DATEOFBIRTH == null ? "" : master.DATEOFBIRTH);
                SqlParameter ADDRESS = new SqlParameter("@ADDRESS", master.ADDRESS);
                SqlParameter CONTACTNUMBER = new SqlParameter("@CONTACTNUMBER", master.CONTACTNUMBER);

                SqlParameter ENROLL = new SqlParameter("@ENROLL", master.ENROLL);
                SqlParameter UNITID = new SqlParameter("@UNITID", master.UNITID);

                SqlParameter MSGCODE = new SqlParameter("@MSGCODE", EnumMessage.EDIT.ToString());
                SqlParameter MODULECODE = new SqlParameter("@MODULECODE", EnumSubMenuCode.WM0001.ToString());


                //SqlParameter ENROLL = new SqlParameter("@ENROLL", 0);
                _context.Database.ExecuteSqlRaw("Exec SPWORKERMANAGEMENTUPDATE @ID,@WORKGROUP,@JOBSTATION,@NAME,@CODE,@PFNUMBER,@DIVISIONID,@EMPNO,@GENDERID,@WORKERTYPEID,@WORKERSUBTYPEID,@BOOKID,@DISABLEDDISINCENTIVEID,@MONTHLYRATED,@ESSENTIALWORKER,@RETIREDID,@PLUCKINGBATCHID,@BATCHID,@KAMJARIID,@DATEOFBIRTH,@ADDRESS,@CONTACTNUMBER,@ENROLL,@UNITID,@MSGCODE,@MODULECODE,@MSG OUTPUT,@STATUSMSG OUTPUT", ID, WORKGROUP, JOBSTATION, NAME, CODE, PFNUMBER, DIVISIONID, EMPNO, GENDERID, WORKERTYPEID, WORKERSUBTYPEID, BOOKID, DISABLEDDISINCENTIVEID, MONTHLYRATED, ESSENTIALWORKER, RETIREDID, PLUCKINGBATCHID, BATCHID, KAMJARIID, DATEOFBIRTH, ADDRESS, CONTACTNUMBER,ENROLL,UNITID, MSGCODE, MODULECODE, MSG, STATUSMSG);


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
