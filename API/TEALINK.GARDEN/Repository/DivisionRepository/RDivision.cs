using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics.Metrics;
using TEALINK.GARDEN.Interface.Division;
using TEALINK.GARDEN.Model.Division;
using static TEALINK.GARDEN.Model.Common.EnamProcess;

namespace TEALINK.GARDEN.Repository.DivisionRepository
{
    public class RDivision : IDivision
    {
        private readonly Context _context;
        string strMsg = "";
        string strStatusMsg = "";

        public RDivision(Context context)
        {
            _context = context;
        }
        public Tuple<string, string> Create(DivisionMaster master)
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
                SqlParameter NAME = new SqlParameter("@NAME", master.Name);
                SqlParameter CODE = new SqlParameter("@CODE", master.Code);

                SqlParameter MSGCODE = new SqlParameter("@MSGCODE", EnumMessage.SAVE.ToString());
                SqlParameter MODULECODE = new SqlParameter("@MODULECODE", EnumSubMenuCode.DM0001.ToString());
                
                //SqlParameter ENROLL = new SqlParameter("@ENROLL", 0);
                _context.Database.ExecuteSqlRaw("Exec SPSPDIVISIONSAVE @ID,@NAME,@CODE,@MSGCODE,@MODULECODE,@MSG OUTPUT,@STATUSMSG OUTPUT", ID, NAME, CODE,MSGCODE,MODULECODE,MSG, STATUSMSG);


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

        public Tuple<string, string> Delete(int id)
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
                SqlParameter MODULECODE = new SqlParameter("@MODULECODE", EnumSubMenuCode.DM0001.ToString());

                //SqlParameter ENROLL = new SqlParameter("@ENROLL", 0);
                _context.Database.ExecuteSqlRaw("Exec SPSPDIVISIONDELETE @ID,@MSGCODE,@MODULECODE,@MSG OUTPUT,@STATUSMSG OUTPUT", ID, MSGCODE, MODULECODE, MSG, STATUSMSG);


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

        public IQueryable<DivisionMaster> GetDataByID(int id)
        {
            SqlParameter ID = new SqlParameter("@ID", id);
            return _context.DivisionMaster.FromSqlRaw("Exec SPGETDIVISIONDATABYID @ID", ID);
        }

        public IQueryable<DivisionMaster> GetDivisionInfo(int unitid)
        {
            SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
            return _context.DivisionMaster.FromSqlRaw("Exec SPGETDIVISION @UNITID", UNITID);
        }

        public Tuple<string, string> Update(int id,DivisionMaster master)
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
            SqlParameter NAME = new SqlParameter("@NAME", master.Name);
            SqlParameter CODE = new SqlParameter("@CODE", master.Code);
            SqlParameter MSGCODE = new SqlParameter("@MSGCODE", EnumMessage.EDIT.ToString());
            SqlParameter MODULECODE = new SqlParameter("@MODULECODE", EnumSubMenuCode.DM0001.ToString());

            _context.Database.ExecuteSqlRaw("Exec SPGETDIVISIONUPDATE @ID,@NAME,@CODE,@MSGCODE,@MODULECODE,@MSG OUTPUT,@STATUSMSG OUTPUT", ID, NAME, CODE, MSGCODE, MODULECODE, MSG, STATUSMSG);

            strMsg = MSG.Value.ToString();
            strStatusMsg = STATUSMSG.Value.ToString();
            return Tuple.Create(strMsg, strStatusMsg);
        }
    }
}
