

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Threading.Channels;


using TEALINK.GARDEN;
using TEALINK.Models.Securities;


namespace TEALINK.Repositories.Securities
{
    public class LogRepository : ILogRepository
    {
        public Context _context;
        SqlConnection _connection;
        IConfiguration _configuration;
       
        public LogRepository(Context contextt, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = contextt;
            _connection = (SqlConnection)_context.Database.GetDbConnection();
        }
        public void SaveActivityLog(ActivityLog activityLog, string type)
        {
            try
            {
                _connection.Open();
                using (SqlCommand cmd = new SqlCommand("SPAPILOG", _connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    _ = cmd.Parameters.AddWithValue("@TYPE", type.Trim());
                    _ = cmd.Parameters.AddWithValue("@LOG", activityLog.ACTIVITYLOG.Trim());
                    _ = cmd.Parameters.AddWithValue("@PROCESSNAME", activityLog.PROCESSNAME.Trim());
                    _ = cmd.Parameters.AddWithValue("@CHANNEL", activityLog.CHANNEL);
                    _ = cmd.Parameters.AddWithValue("@DOCUMENTCREATEDDATE", activityLog.DOCUMENTCREATEDDATE);
                    _ = cmd.Parameters.AddWithValue("@DOCUMENTCREATEDBY", activityLog.DOCUMENTCREATEDBY);
                    int logStatus = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { }
            _connection.Close();
        }

        public void SaveErrorLog(ErrorLog errorLog, string type)
        {
            try
            {
                _connection.Open();
                using (SqlCommand cmd = new SqlCommand("SPAPILOG", _connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    _ = cmd.Parameters.AddWithValue("@TYPE", type.Trim());
                    _ = cmd.Parameters.AddWithValue("@LOG", errorLog.ERRORLOG.Trim());
                    _ = cmd.Parameters.AddWithValue("@PROCESSNAME", errorLog.PROCESSNAME.Trim());
                    _ = cmd.Parameters.AddWithValue("@CHANNEL", errorLog.CHANNEL.Trim());
                    _ = cmd.Parameters.AddWithValue("@DOCUMENTCREATEDDATE", errorLog.DOCUMENTCREATEDDATE);
                    _ = cmd.Parameters.AddWithValue("@DOCUMENTCREATEDBY", errorLog.DOCUMENTCREATEDBY.Trim());
                    int logStatus = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { }
            _connection.Close();
        }
    }
}
