using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using static TEALINK.GARDEN.Model.Common.EnamProcess;
using TEALINK.GARDEN.Model.GardenManagement;
using TEALINK.GARDEN.Interface.SectionManagement;
using TEALINK.GARDEN.Model.Section;

namespace TEALINK.GARDEN.Repository.SectionManagementRepository
{
    public class RSectionManagement : ISectionManagement
    {

        private readonly Context _context;
        string strMsg = "";
        string strStatusMsg = "";

        public RSectionManagement(Context context)
        {
            _context = context;
        }

        public Tuple<string, string> Create(SectionManagementMaster master)
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
                SqlParameter NAME = new SqlParameter("@NAME", master.NAME==null ? "" : master.NAME);
                SqlParameter CODE = new SqlParameter("@CODE", master.CODE);
                SqlParameter DIVISIONID = new SqlParameter("@DIVISIONID", master.DIVISIONID);
                SqlParameter COSTCENTRETYPE = new SqlParameter("@COSTCENTRETYPE", master.COSTCENTRETYPE==null ? "" : master.COSTCENTRETYPE);
                SqlParameter PLUCKINGID = new SqlParameter("@PLUCKINGID", master.ISPLUCKINGID);
                SqlParameter IRRIGATIONID = new SqlParameter("@IRRIGATIONID", master.ISIRRIGATIONID);

                SqlParameter USUALCOMMANDAREA = new SqlParameter("@USUALCOMMANDAREA", master.USUALCOMMANDAREA);
                SqlParameter AREA = new SqlParameter("@AREA", master.AREA);

                SqlParameter PRUNETYPEID = new SqlParameter("@PRUNETYPEID", master.PRUNETYPEID);
                SqlParameter USUALSETTINGS = new SqlParameter("@USUALSETTINGS", master.USUALSETTINGS==null ? "" : master.USUALSETTINGS);

                SqlParameter ENROLL = new SqlParameter("@ENROLL", master.ENROLL);
                SqlParameter UNITID = new SqlParameter("@UNITID", master.UNITID);
                SqlParameter MSGCODE = new SqlParameter("@MSGCODE", EnumMessage.SAVE.ToString());
                SqlParameter MODULECODE = new SqlParameter("@MODULECODE", EnumSubMenuCode.SECM0001.ToString());
                

                //SqlParameter ENROLL = new SqlParameter("@ENROLL", 0);
                _context.Database.ExecuteSqlRaw("Exec SPSECTIONSAVE @ID,@NAME,@CODE,@DIVISIONID,@COSTCENTRETYPE,@PLUCKINGID,@IRRIGATIONID,@USUALCOMMANDAREA,@AREA,@PRUNETYPEID,@USUALSETTINGS,@ENROLL,@UNITID,@MSGCODE,@MODULECODE,@MSG OUTPUT,@STATUSMSG OUTPUT", ID, NAME, CODE, DIVISIONID, COSTCENTRETYPE, PLUCKINGID, IRRIGATIONID, USUALCOMMANDAREA, AREA, PRUNETYPEID, USUALSETTINGS, ENROLL,UNITID, MSGCODE, MODULECODE, MSG, STATUSMSG);


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

        public Tuple<string, string> Delete(int id, int unitid,int enroll)
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
                SqlParameter MODULECODE = new SqlParameter("@MODULECODE", EnumSubMenuCode.SECM0001.ToString());
                SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
                SqlParameter ENROLL = new SqlParameter("@ENROLL", enroll);

                //SqlParameter ENROLL = new SqlParameter("@ENROLL", 0);
                _context.Database.ExecuteSqlRaw("Exec SPSECTIONDELETE @ID,@UNITID,@ENROLL,@MSGCODE,@MODULECODE,@MSG OUTPUT,@STATUSMSG OUTPUT", ID, UNITID, ENROLL, MSGCODE, MODULECODE, MSG, STATUSMSG);


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

        public IQueryable<SectionManagementView> GetData(int unitid,int enroll)
        {
            SqlParameter TYPEID = new SqlParameter("@TYPEID", 1);
            SqlParameter ID = new SqlParameter("@ID", 1);
            SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
            SqlParameter ENROLL = new SqlParameter("@ENROLL", enroll);

            return _context.SectionManagementView.FromSqlRaw("Exec SPGETSECTION @TYPEID,@ID,@UNITID,@ENROLL", TYPEID, ID, UNITID, ENROLL);
        }

        public IQueryable<SectionManagementView> GetDataByID(int id)
        {
            SqlParameter TYPEID = new SqlParameter("@TYPEID", 2);
            SqlParameter ID = new SqlParameter("@ID", id);
            SqlParameter UNITID = new SqlParameter("@UNITID", 1);
            SqlParameter ENROLL = new SqlParameter("@ENROLL", 1);

            return _context.SectionManagementView.FromSqlRaw("Exec SPGETSECTION @TYPEID,@ID,@UNITID,@ENROLL", TYPEID, ID, UNITID, ENROLL);
        }

        public Tuple<string, string> Update(int id, SectionManagementMaster master)
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
                SqlParameter DIVISIONID = new SqlParameter("@DIVISIONID", master.DIVISIONID);
                SqlParameter COSTCENTRETYPE = new SqlParameter("@COSTCENTRETYPE", master.COSTCENTRETYPE == null ? "" : master.COSTCENTRETYPE);
                SqlParameter PLUCKINGID = new SqlParameter("@PLUCKINGID", master.ISPLUCKINGID);
                SqlParameter IRRIGATIONID = new SqlParameter("@IRRIGATIONID", master.ISIRRIGATIONID);

                SqlParameter USUALCOMMANDAREA = new SqlParameter("@USUALCOMMANDAREA", master.USUALCOMMANDAREA);
                SqlParameter AREA = new SqlParameter("@AREA", master.AREA);

                SqlParameter PRUNETYPEID = new SqlParameter("@PRUNETYPEID", master.PRUNETYPEID);
                SqlParameter USUALSETTINGS = new SqlParameter("@USUALSETTINGS", master.USUALSETTINGS == null ? "" : master.USUALSETTINGS);

                SqlParameter ENROLL = new SqlParameter("@ENROLL", master.ENROLL);
                SqlParameter UNITID = new SqlParameter("@UNITID", master.UNITID);
                SqlParameter MSGCODE = new SqlParameter("@MSGCODE", EnumMessage.EDIT.ToString());
                SqlParameter MODULECODE = new SqlParameter("@MODULECODE", EnumSubMenuCode.GM0001.ToString());
               

                //SqlParameter ENROLL = new SqlParameter("@ENROLL", 0);
                _context.Database.ExecuteSqlRaw("Exec SPSECTIONUPDATE @ID,@NAME,@CODE,@DIVISIONID,@COSTCENTRETYPE,@PLUCKINGID,@IRRIGATIONID,@USUALCOMMANDAREA,@AREA,@PRUNETYPEID,@USUALSETTINGS,@ENROLL,@UNITID,@MSGCODE,@MODULECODE,@MSG OUTPUT,@STATUSMSG OUTPUT", ID, NAME, CODE, DIVISIONID, COSTCENTRETYPE, PLUCKINGID, IRRIGATIONID, USUALCOMMANDAREA, AREA, PRUNETYPEID, USUALSETTINGS, ENROLL, UNITID, MSGCODE, MODULECODE, MSG, STATUSMSG);


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
