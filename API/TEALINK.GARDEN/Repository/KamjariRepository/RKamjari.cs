using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using static TEALINK.GARDEN.Model.Common.EnamProcess;
using TEALINK.GARDEN.Model.PluckingType;
using TEALINK.GARDEN.Interface.KamjariManagement;
using TEALINK.GARDEN.Model.Kamjari;

namespace TEALINK.GARDEN.Repository.KamjariRepository
{
    public class RKamjari : IKamjariManagement
    {
        private readonly Context _context;
        string strMsg = "";
        string strStatusMsg = "";

        public RKamjari(Context context)
        {
            _context = context;
        }

        public Tuple<string, string> Create(KamjariMaster master)
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
                SqlParameter NAME = new SqlParameter("@NAME", master.NAME);
                SqlParameter CODE = new SqlParameter("@CODE", master.CODE);

                SqlParameter KAMJARITYPEID = new SqlParameter("@KAMJARITYPEID", master.KAMJARITYPEID == null ? "" : master.KAMJARITYPEID);
                SqlParameter MARKASDOUBLY = new SqlParameter("@MARKASDOUBLY", master.MARKASDOUBLY == null ? "" : master.MARKASDOUBLY);
                SqlParameter EXCLUDEINWAGES = new SqlParameter("@EXCLUDEINWAGES", master.EXCLUDEINWAGES == null ? "" : master.EXCLUDEINWAGES);

                SqlParameter ACCOUNTID = new SqlParameter("@ACCOUNTID", master.ACCOUNTID == null ? "" : master.ACCOUNTID);
                SqlParameter TASKUNIT = new SqlParameter("@TASKUNIT", master.TASKUNIT == null ? "" : master.TASKUNIT);
                SqlParameter TASKMEN = new SqlParameter("@TASKMEN", master.TASKMEN == null ? "" : master.TASKMEN);
                SqlParameter TASKWOMEN = new SqlParameter("@TASKWOMEN", master.TASKWOMEN == null ? "" : master.TASKWOMEN);

                SqlParameter PAYOFPOST = new SqlParameter("@PAYOFPOST", master.PAYOFPOST == null ? "" : master.PAYOFPOST);
                SqlParameter MASTERTYPEID = new SqlParameter("@MASTERTYPEID", master.MASTERTYPEID == null ? "" : master.MASTERTYPEID);
                SqlParameter ISMATERIALACCOUNT = new SqlParameter("@ISMATERIALACCOUNT", master.ISMATERIALACCOUNT);
                SqlParameter DOUBLYRATE = new SqlParameter("@DOUBLYRATE", master.DOUBLYRATE);
                SqlParameter FIXEDDOUBLYRATE = new SqlParameter("@FIXEDDOUBLYRATE", master.FIXEDDOUBLYRATE);

                SqlParameter ENROLL = new SqlParameter("@ENROLL", master.ENROLL);
                SqlParameter UNITID = new SqlParameter("@UNITID", master.UNITID);
                SqlParameter MSGCODE = new SqlParameter("@MSGCODE", EnumMessage.SAVE.ToString());
                SqlParameter MODULECODE = new SqlParameter("@MODULECODE", EnumSubMenuCode.PLK0001.ToString());


                //SqlParameter ENROLL = new SqlParameter("@ENROLL", 0);
                _context.Database.ExecuteSqlRaw("Exec SPKAMJARISAVE @ID,@NAME,@CODE,@KAMJARITYPEID,@MARKASDOUBLY,@EXCLUDEINWAGES,@ACCOUNTID,@TASKUNIT,@TASKMEN,@TASKWOMEN,@PAYOFPOST,@MASTERTYPEID,@ISMATERIALACCOUNT,@DOUBLYRATE,@FIXEDDOUBLYRATE,@ENROLL,@UNITID,@MSGCODE,@MODULECODE,@MSG OUTPUT,@STATUSMSG OUTPUT", ID, NAME, CODE, KAMJARITYPEID, MARKASDOUBLY, EXCLUDEINWAGES, ACCOUNTID, TASKUNIT, TASKMEN, TASKWOMEN, PAYOFPOST, MASTERTYPEID, ISMATERIALACCOUNT, DOUBLYRATE, FIXEDDOUBLYRATE, ENROLL, UNITID, MSGCODE, MODULECODE, MSG, STATUSMSG);

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
                SqlParameter MODULECODE = new SqlParameter("@MODULECODE", EnumSubMenuCode.KMJM0001.ToString());
                SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
                SqlParameter ENROLL = new SqlParameter("@ENROLL", enroll);

                //SqlParameter ENROLL = new SqlParameter("@ENROLL", 0);
                _context.Database.ExecuteSqlRaw("Exec SPKAMJARIDELETE @ID,@UNITID,@ENROLL,@MSGCODE,@MODULECODE,@MSG OUTPUT,@STATUSMSG OUTPUT", ID, UNITID, ENROLL, MSGCODE, MODULECODE, MSG, STATUSMSG);


                strMsg = MSG.Value.ToString() ?? "";
                strStatusMsg = STATUSMSG.Value?.ToString() ?? "";
                return Tuple.Create(strMsg, strStatusMsg);
            }
            catch (Exception ex)
            {

                // _common.ErrorLog("", ex.ToString(), enumIMPSubMenu.LCM00001.ToString(), enumIMPProcessName.LCM.ToString(), master.UNITID, master.DOCUMENTCREATEDBY, "");
            }
            return null;
        }

        public IQueryable<KamjariMaster> GetData(int unitid, int enroll)
        {
            SqlParameter TYPEID = new SqlParameter("@TYPEID", 1);
            SqlParameter ID = new SqlParameter("@ID", 1);
            SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
            SqlParameter ENROLL = new SqlParameter("@ENROLL", enroll);

            return _context.KamjariMaster.FromSqlRaw("Exec SPGETKAMJARIDATA @TYPEID,@ID,@UNITID,@ENROLL", TYPEID, ID, UNITID, ENROLL);
        }

        public IQueryable<KamjariMaster> GetDataByID(int id, int unitid, int enroll)
        {
            SqlParameter TYPEID = new SqlParameter("@TYPEID", 2);
            SqlParameter ID = new SqlParameter("@ID", id);
            SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
            SqlParameter ENROLL = new SqlParameter("@ENROLL", enroll);

            return _context.KamjariMaster.FromSqlRaw("Exec SPGETKAMJARIDATA @TYPEID,@ID,@UNITID,@ENROLL", TYPEID, ID, UNITID, ENROLL);
        }

        public Tuple<string, string> Update(int id, KamjariMaster master)
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
                SqlParameter NAME = new SqlParameter("@NAME", master.NAME);
                SqlParameter CODE = new SqlParameter("@CODE", master.CODE);

                SqlParameter KAMJARITYPEID = new SqlParameter("@KAMJARITYPEID", master.KAMJARITYPEID == null ? "" : master.KAMJARITYPEID);
                SqlParameter MARKASDOUBLY = new SqlParameter("@MARKASDOUBLY", master.MARKASDOUBLY == null ? "" : master.MARKASDOUBLY);
                SqlParameter EXCLUDEINWAGES = new SqlParameter("@EXCLUDEINWAGES", master.EXCLUDEINWAGES == null ? "" : master.EXCLUDEINWAGES);

                SqlParameter ACCOUNTID = new SqlParameter("@ACCOUNTID", master.ACCOUNTID == null ? "" : master.ACCOUNTID);
                SqlParameter TASKUNIT = new SqlParameter("@TASKUNIT", master.TASKUNIT == null ? "" : master.TASKUNIT);
                SqlParameter TASKMEN = new SqlParameter("@TASKMEN", master.TASKMEN == null ? "" : master.TASKMEN);
                SqlParameter TASKWOMEN = new SqlParameter("@TASKWOMEN", master.TASKWOMEN == null ? "" : master.TASKWOMEN);

                SqlParameter PAYOFPOST = new SqlParameter("@PAYOFPOST", master.PAYOFPOST == null ? "" : master.PAYOFPOST);
                SqlParameter MASTERTYPEID = new SqlParameter("@MASTERTYPEID", master.MASTERTYPEID == null ? "" : master.MASTERTYPEID);
                SqlParameter ISMATERIALACCOUNT = new SqlParameter("@ISMATERIALACCOUNT", master.ISMATERIALACCOUNT);
                SqlParameter DOUBLYRATE = new SqlParameter("@DOUBLYRATE", master.DOUBLYRATE);
                SqlParameter FIXEDDOUBLYRATE = new SqlParameter("@FIXEDDOUBLYRATE", master.FIXEDDOUBLYRATE);

                SqlParameter ENROLL = new SqlParameter("@ENROLL", master.ENROLL);
                SqlParameter UNITID = new SqlParameter("@UNITID", master.UNITID);
                SqlParameter MSGCODE = new SqlParameter("@MSGCODE", EnumMessage.EDIT.ToString());
                SqlParameter MODULECODE = new SqlParameter("@MODULECODE", EnumSubMenuCode.KMJM0001.ToString());


                //SqlParameter ENROLL = new SqlParameter("@ENROLL", 0);
                _context.Database.ExecuteSqlRaw("Exec SPKAMJARIUPDATE @ID,@NAME,@CODE,@KAMJARITYPEID,@MARKASDOUBLY,@EXCLUDEINWAGES,@ACCOUNTID,@TASKUNIT,@TASKMEN,@TASKWOMEN,@PAYOFPOST,@MASTERTYPEID,@ISMATERIALACCOUNT,@DOUBLYRATE,@FIXEDDOUBLYRATE,@ENROLL,@UNITID,@MSGCODE,@MODULECODE,@MSG OUTPUT,@STATUSMSG OUTPUT", ID, NAME, CODE, KAMJARITYPEID, MARKASDOUBLY, EXCLUDEINWAGES, ACCOUNTID, TASKUNIT, TASKMEN, TASKWOMEN, PAYOFPOST, MASTERTYPEID, ISMATERIALACCOUNT, DOUBLYRATE, FIXEDDOUBLYRATE, ENROLL, UNITID, MSGCODE, MODULECODE, MSG, STATUSMSG);


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
