using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TEALINK.GARDEN.Interface.PluckingAllocation;
using TEALINK.GARDEN.Model.PluckingAllocation;

namespace TEALINK.GARDEN.Repository.PluckingAllocationRepository
{
    public class RPluckingAllocation:IPluckingAllocation
    {
        private readonly Context _context;
        string strMsg = "";
        string strStatusMsg = "";
        public RPluckingAllocation(Context context)
        {
            _context = context;
        }


        public IQueryable<PluckingLanding> GetDataByID(int unit, int id, string date)
        {
            SqlParameter UNIT = new SqlParameter("@UNIT",unit); 
            SqlParameter ID = new SqlParameter("@ID", id); 
            SqlParameter DATE = new SqlParameter("@DATE", date);

            return _context.PluckingLanding.FromSqlRaw("Exec SPPLUCKINGALLOCATIONGetDataByID @UNIT,@ID,@DATE", UNIT, ID, DATE);
        }

        public Tuple<string, string> Create(PluckingMaster pm, PluckingLanding[] details)
        {
            try
            {
                DataTable dt = new DataTable { TableName = "Details" };

                dt.Columns.Add("AVGPLUCKQTY", typeof(decimal));
                dt.Columns.Add("MINPLUCKQTY", typeof(decimal));
                dt.Columns.Add("MAXPLUCKQTY", typeof(decimal));
                dt.Columns.Add("TARGETQTY", typeof(decimal));
                dt.Columns.Add("MINQTY", typeof(decimal));
                dt.Columns.Add("MAXQTY", typeof(decimal));
                dt.Columns.Add("BATCHID", typeof(int));
                dt.Columns.Add("BOOKID", typeof(int));
                dt.Columns.Add("OPERATORID", typeof(int));
                dt.Columns.Add("DEVICEID", typeof(int));
                dt.Columns.Add("KAMJARITYPEID", typeof(int));
                dt.Columns.Add("WEIGHMENTID", typeof(int));


                for (int i = 0; i < details.Length; i++)
                {
                    dt.Rows.Add(details[i].AVGPLUCKQTY, details[i].MINPLUCKQTY, details[i].MAXPLUCKQTY, details[i].TARGETQTY, details[i].MINQTY, details[i].MAXQTY, details[i].BATCHID, details[i].BOOKID, details[i].OPERATORID,  details[i].DEVICEID, details[i].KAMJARITYPEID, details[i].WEIGHMENTID);
                }

                StringWriter sw;
                using (sw = new StringWriter())
                {
                    dt.WriteXml(sw);
                }
                SqlParameter ENROLL = new SqlParameter("@ENROLL", pm.ENROLL);
                SqlParameter PLUCKINGTYPEID = new SqlParameter("@PLUCKINGTYPEID", pm.PLUCKINGTYPEID);
                SqlParameter PLUCKINGDATE = new SqlParameter("@PLUCKINGDATE", pm.PLUCKINGDATE);
                SqlParameter ELPDLPRATE = new SqlParameter("@ELPDLPRATE", pm.ELPDLPRATE);
                SqlParameter TARGETKG = new SqlParameter("@TARGETKG", pm.TARGETKG);
                SqlParameter MINTASKKG = new SqlParameter("@MINTASKKG", pm.MINTASKKG);
                SqlParameter MAXTASKKG = new SqlParameter("@MAXTASKKG", pm.MAXTASKKG);
                SqlParameter EXTRARATE = new SqlParameter("@EXTRARATE", pm.EXTRARATE);
                SqlParameter AVGPLUCKED = new SqlParameter("@AVGPLUCKED", pm.AVGPLUCKED);
                SqlParameter MINPLUCKED = new SqlParameter("@MINPLUCKED", pm.MINPLUCKED);
                SqlParameter MAXPLUCKED = new SqlParameter("@MAXPLUCKED", pm.MAXPLUCKED);
                SqlParameter DISABLEDISINCENTIVE = new SqlParameter("@DISABLEDISINCENTIVE", pm.DISABLEDISINCENTIVE);
                SqlParameter ENABLEPLUCKINGEXTRA = new SqlParameter("@ENABLEPLUCKINGEXTRA", pm.ENABLEPLUCKINGEXTRA);
                SqlParameter UNIT = new SqlParameter("@UNIT", pm.UNIT);

                SqlParameter detailsparam = new SqlParameter();
                detailsparam.ParameterName = "@TYPEPLALINE";
                detailsparam.SqlDbType = SqlDbType.Structured;
                detailsparam.TypeName = "TYPEPLALINE";
                detailsparam.SqlValue = dt;

                
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

                _context.Database.ExecuteSqlRaw("Exec SPPLASAVE @TYPEPLALINE,@ENROLL,@UNIT,@PLUCKINGTYPEID,@PLUCKINGDATE,@ELPDLPRATE,@TARGETKG,@MINTASKKG,@MAXTASKKG,@EXTRARATE,@AVGPLUCKED,@MINPLUCKED,@MAXPLUCKED,@DISABLEDISINCENTIVE,@ENABLEPLUCKINGEXTRA,@MSG OUTPUT,@STATUSMSG OUTPUT",
                    detailsparam,ENROLL,UNIT, PLUCKINGTYPEID, PLUCKINGDATE, ELPDLPRATE, TARGETKG, MINTASKKG, MAXTASKKG, EXTRARATE, AVGPLUCKED, MINPLUCKED, MAXPLUCKED, DISABLEDISINCENTIVE, ENABLEPLUCKINGEXTRA, MSG, STATUSMSG);

                strMsg = MSG.Value.ToString();
                strStatusMsg = STATUSMSG.Value.ToString();

                return Tuple.Create(strMsg, strStatusMsg);
            }
            catch (Exception ex)
            {
               
            }
            return null;
        }

    }
}
