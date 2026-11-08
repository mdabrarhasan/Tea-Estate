using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net;
using TEALINK.GARDEN.Interface.Excel;
using static TEALINK.GARDEN.Model.Common.EnamProcess;
using TEALINK.GARDEN.Model.DailyPlucking;
using TEALINK.GARDEN.Model.ExeclData;

namespace TEALINK.GARDEN.Repository.ExcelRepository
{
    public class RExcel : IExcel
    {
        private Context _context;
        string strMsg = "";
        string strStatusMsg = "";
        public RExcel(Context context)
        {
            _context = context;
        }
        public Tuple<string, string> Create(List<ExcelData> obj, string date, string typeid, string unitid)
        {
            try
            {
                DataTable dt = new DataTable { TableName = "Details" };
                dt.Columns.Add("EMPCODE", typeof(string));
                dt.Columns.Add("SHIFT", typeof(string));
                dt.Columns.Add("KAMJAI", typeof(string));
                dt.Columns.Add("BOOK", typeof(string));
                dt.Columns.Add("SECTION", typeof(string));
                dt.Columns.Add("DATE", typeof(string));
                dt.Columns.Add("NAME", typeof(string));
                dt.Columns.Add("DIVISION", typeof(string));
                dt.Columns.Add("WORKERTYPE", typeof(string));
                dt.Columns.Add("W1", typeof(decimal));
                dt.Columns.Add("W2", typeof(decimal));
                dt.Columns.Add("W3", typeof(decimal));
                dt.Columns.Add("W4", typeof(decimal));

                for (int i = 0; i < obj.Count(); i++)
                {
                    dt.Rows.Add(
                             obj[i].EMPCODE,
                             obj[i].SHFIT,
                             obj[i].KAMJAI,
                             obj[i].BOOK,
                             obj[i].SECTION,
                             obj[i].DATE,
                             obj[i].NAME,
                             obj[i].DIVISION,
                             obj[i].WORKERTYPE,
                             obj[i].W1,
                             obj[i].W2,
                             obj[i].W3,
                             obj[i].W4
                         );
                }

                var MSG = new SqlParameter
                {
                    ParameterName = "@MSG",
                    Size = 100,
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Output
                };

                var STATUSMSG = new SqlParameter
                {
                    ParameterName = "@STATUSMSG",
                    Size = -1,                       // NVARCHAR(MAX)
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Output
                };

                SqlParameter detailsparam = new SqlParameter();
                detailsparam.ParameterName = "@TYPEMANUALENTRY";
                detailsparam.SqlDbType = SqlDbType.Structured;
                detailsparam.TypeName = "TYPEMANUALENTRY";
                detailsparam.SqlValue = dt;

                SqlParameter PLUCKINGDATE = new SqlParameter("@PLUCKINGDATE", (object)date ?? DBNull.Value);
                SqlParameter UNIT = new SqlParameter("@UNIT", (object)unitid ?? DBNull.Value);
                SqlParameter TYPEID = new SqlParameter("@TYPEID", (object)typeid ?? DBNull.Value);

                _context.Database.ExecuteSqlRaw("Exec SPMANUALENTRYSAVE @PLUCKINGDATE,@UNIT,@TYPEID,@TYPEMANUALENTRY,@MSG OUTPUT,@STATUSMSG OUTPUT", PLUCKINGDATE, UNIT, TYPEID, detailsparam, MSG, STATUSMSG);

                strMsg = MSG.Value?.ToString()?.Trim() ?? "error";
                strStatusMsg = STATUSMSG.Value?.ToString()?.Trim() ?? "";
                return Tuple.Create(strMsg, strStatusMsg);
            }
            catch (Exception ex)
            {
                return Tuple.Create("error", ex.Message);
            }
        }
    }
}