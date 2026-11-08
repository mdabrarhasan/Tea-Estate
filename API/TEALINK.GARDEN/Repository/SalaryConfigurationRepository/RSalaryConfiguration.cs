using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TEALINK.GARDEN.Interface.SalaryConfiguration;
using TEALINK.GARDEN.Model.SalaryConfiguration;
using TEALINK.GARDEN.Model.ShiftManagement;
using static TEALINK.GARDEN.Model.Common.EnamProcess;

namespace TEALINK.GARDEN.Repository.SalaryConfigurationRepository
{
    public class RSalaryConfiguration : ISalaryConfiguration
    {
        private readonly Context _context;
        string strMsg = "";
        string strStatusMsg = "";

        public RSalaryConfiguration(Context context)
        {
            _context = context;
        }
        public IQueryable<SalaryConfigurationMasterView> GetData(int unitid, int enroll)
        {
            
            SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
            

            return _context.SalaryConfigurationMasterView.FromSqlRaw("Exec SPGETSALARYCONFIGURATION @UNITID", UNITID);
        }
        public IQueryable<SalaryConfigurationMaster> GetDataByID(int id, int unitid, int enroll)
        {
            SqlParameter ID = new SqlParameter("@ID", id);
            SqlParameter UNITID = new SqlParameter("@UNITID", unitid);

            return _context.SalaryConfigurationMaster.FromSqlRaw("Exec SPGETSALARYCONFIGURATIONBYID @ID, @UNITID", ID, UNITID);
        }
        public Tuple<string, string> Create(SalaryConfigurationMaster master)
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
                SqlParameter UNITID = new SqlParameter("@UNITID", master.UNITID);
                SqlParameter ENROLL = new SqlParameter("@ENROLL", master.ENROLL);
                SqlParameter WAGES = new SqlParameter("@WAGES", master.WAGES);
                SqlParameter OT = new SqlParameter("@OT", master.OT);
                SqlParameter WORKERTYPEID = new SqlParameter("@WORKERTYPEID", master.WORKERTYPEID);
                SqlParameter ALLOWANCEAPPLICABLE = new SqlParameter("@ALLOWANCEAPPLICABLE", master.ALLOWANCEAPPLICABLE);
                SqlParameter OTALLOWANCEAPPLICABLE = new SqlParameter("@OTALLOWANCEAPPLICABLE", master.OTALLOWANCEAPPLICABLE);
                SqlParameter MSGCODE = new SqlParameter("@MSGCODE", EnumMessage.SAVE.ToString());
                SqlParameter MODULECODE = new SqlParameter("@MODULECODE", EnumSubMenuCode.SHIFT0001.ToString());


                _context.Database.ExecuteSqlRaw("Exec SPSALARYCONFIGURATIONSAVE @ID,@UNITID,@ENROLL,@WAGES,@OT,@WORKERTYPEID,@ALLOWANCEAPPLICABLE,@OTALLOWANCEAPPLICABLE,@MSGCODE,@MODULECODE,@MSG OUTPUT,@STATUSMSG OUTPUT", ID, UNITID, ENROLL, WAGES, OT, WORKERTYPEID, ALLOWANCEAPPLICABLE, OTALLOWANCEAPPLICABLE, MSGCODE, MODULECODE, MSG, STATUSMSG);


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
        public Tuple<string, string> Update(int id, SalaryConfigurationMaster master)
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
                SqlParameter UNITID = new SqlParameter("@UNITID", master.UNITID);
                SqlParameter ENROLL = new SqlParameter("@ENROLL", master.ENROLL);
                SqlParameter WAGES = new SqlParameter("@WAGES", master.WAGES);
                SqlParameter OT = new SqlParameter("@OT", master.OT);
                SqlParameter WORKERTYPEID = new SqlParameter("@WORKERTYPEID", master.WORKERTYPEID);
                SqlParameter ALLOWANCEAPPLICABLE = new SqlParameter("@ALLOWANCEAPPLICABLE", master.ALLOWANCEAPPLICABLE);
                SqlParameter OTALLOWANCEAPPLICABLE = new SqlParameter("@OTALLOWANCEAPPLICABLE", master.OTALLOWANCEAPPLICABLE);
                SqlParameter MSGCODE = new SqlParameter("@MSGCODE", EnumMessage.EDIT.ToString());
                SqlParameter MODULECODE = new SqlParameter("@MODULECODE", EnumSubMenuCode.KMJM0001.ToString());


                _context.Database.ExecuteSqlRaw("Exec SPSALARYCONFIGURATIONUPDATE @ID,@UNITID,@ENROLL,@WAGES,@OT,@WORKERTYPEID,@ALLOWANCEAPPLICABLE,@OTALLOWANCEAPPLICABLE,@MSGCODE,@MODULECODE,@MSG OUTPUT,@STATUSMSG OUTPUT", ID, UNITID, ENROLL, WAGES, OT, WORKERTYPEID, ALLOWANCEAPPLICABLE, OTALLOWANCEAPPLICABLE, MSGCODE, MODULECODE, MSG, STATUSMSG);


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
                SqlParameter MODULECODE = new SqlParameter("@MODULECODE", EnumSubMenuCode.PRUNE0001.ToString());

                _context.Database.ExecuteSqlRaw("Exec SPSALARYCONFIGURATIONDELETE @ID,@UNITID,@ENROLL,@MSGCODE,@MODULECODE,@MSG OUTPUT,@STATUSMSG OUTPUT", ID, UNITID, ENROLL, MSGCODE, MODULECODE, MSG, STATUSMSG);


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
