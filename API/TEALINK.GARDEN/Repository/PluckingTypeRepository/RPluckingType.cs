using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TEALINK.GARDEN.Interface.PluckingType;
using TEALINK.GARDEN.Model.PluckingType;
using static TEALINK.GARDEN.Model.Common.EnamProcess;
using TEALINK.GARDEN.Model.Section;

namespace TEALINK.GARDEN.Repository.PluckingTypeRepository
{
    public class RPluckingType : IPluckingType
    {
        private readonly Context _context;
        string strMsg = "";
        string strStatusMsg = "";

        public RPluckingType(Context context)
        {
            _context = context;
        }

        public Tuple<string, string> Create(PluckingTypeMaster master)
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

                SqlParameter ENROLL = new SqlParameter("@ENROLL", master.ENROLL);
                SqlParameter UNITID = new SqlParameter("@UNITID", master.UNITID);
                SqlParameter MSGCODE = new SqlParameter("@MSGCODE", EnumMessage.SAVE.ToString());
                SqlParameter MODULECODE = new SqlParameter("@MODULECODE", EnumSubMenuCode.PLK0001.ToString());


                //SqlParameter ENROLL = new SqlParameter("@ENROLL", 0);
                _context.Database.ExecuteSqlRaw("Exec SPPLUCKINGSAVE @ID,@NAME,@CODE,@ENROLL,@UNITID,@MSGCODE,@MODULECODE,@MSG OUTPUT,@STATUSMSG OUTPUT", ID, NAME, CODE, ENROLL, UNITID, MSGCODE, MODULECODE, MSG, STATUSMSG);

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
                SqlParameter MODULECODE = new SqlParameter("@MODULECODE", EnumSubMenuCode.PLK0001.ToString());
                SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
                SqlParameter ENROLL = new SqlParameter("@ENROLL", enroll);

                //SqlParameter ENROLL = new SqlParameter("@ENROLL", 0);
                _context.Database.ExecuteSqlRaw("Exec SPPLUCKINGDELETE @ID,@UNITID,@ENROLL,@MSGCODE,@MODULECODE,@MSG OUTPUT,@STATUSMSG OUTPUT", ID, UNITID, ENROLL, MSGCODE, MODULECODE, MSG, STATUSMSG);


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

        public IQueryable<PluckingTypeMaster> GetData(int unitid, int enroll)
        {
            SqlParameter TYPEID = new SqlParameter("@TYPEID", 1);
            SqlParameter ID = new SqlParameter("@ID", 1);
            SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
            SqlParameter ENROLL = new SqlParameter("@ENROLL", enroll);

            return _context.PluckingTypeMaster.FromSqlRaw("Exec SPGETPLUCKINGTYPE @TYPEID,@ID,@UNITID,@ENROLL", TYPEID, ID, UNITID, ENROLL);
        }

        public IQueryable<PluckingTypeMaster> GetDataByID(int id)
        {
            SqlParameter TYPEID = new SqlParameter("@TYPEID", 2);
            SqlParameter ID = new SqlParameter("@ID", id);
            SqlParameter UNITID = new SqlParameter("@UNITID", 1);
            SqlParameter ENROLL = new SqlParameter("@ENROLL", 1);

            return _context.PluckingTypeMaster.FromSqlRaw("Exec SPGETPLUCKINGTYPE @TYPEID,@ID,@UNITID,@ENROLL", TYPEID, ID, UNITID, ENROLL);
        }

        public Tuple<string, string> Update(int id, PluckingTypeMaster master)
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

                SqlParameter ENROLL = new SqlParameter("@ENROLL", master.ENROLL);
                SqlParameter UNITID = new SqlParameter("@UNITID", master.UNITID);
                SqlParameter MSGCODE = new SqlParameter("@MSGCODE", EnumMessage.EDIT.ToString());
                SqlParameter MODULECODE = new SqlParameter("@MODULECODE", EnumSubMenuCode.PLK0001.ToString());


                //SqlParameter ENROLL = new SqlParameter("@ENROLL", 0);
                _context.Database.ExecuteSqlRaw("Exec SPPLUCKINGUPDATE @ID,@NAME,@CODE,@ENROLL,@UNITID,@MSGCODE,@MODULECODE,@MSG OUTPUT,@STATUSMSG OUTPUT", ID, NAME, CODE, ENROLL, UNITID, MSGCODE, MODULECODE, MSG, STATUSMSG);


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

