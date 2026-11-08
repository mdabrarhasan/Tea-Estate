using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TEALINK.GARDEN;
using TEALINK.GARDEN.Interface.Mobile;
using TEALINK.Models.Utilities;

namespace TEALINK.Repositories.Utilities
{
    public class MessageRepository : IMessageRepository
    {
        public Context _context;
        SqlConnection _connection;
        IConfiguration _configuration;
        public MessageRepository(Context contextt, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = contextt;
            _connection = (SqlConnection)_context.Database.GetDbConnection();
        }
        public Message GetMessage(string code)
        {
            Message message = new Message();
            try
            {
                _connection.Open();
                using (SqlCommand cmd = new SqlCommand("SPGetMessage", _connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    _ = cmd.Parameters.AddWithValue("@CODE", code.Trim());
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            message.CODE = reader.GetString("CODE");
                            message.MESSAGECODE = reader.GetString("MESSAGECODE");
                            message.MESSAGE = reader.GetString("MESSAGE");
                            message.STATUS = reader.GetBoolean("STATUS");
                        }
                    }
                }
            }
            catch (Exception ex) { }
            _connection.Close();
            return message;
        }
    }
}
