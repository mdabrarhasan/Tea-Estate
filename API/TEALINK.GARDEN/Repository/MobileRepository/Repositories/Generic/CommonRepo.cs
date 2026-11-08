using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TEALINK.GARDEN;
using TEALINK.GARDEN.Interface.Mobile;
using TEALINK.Models.Generic;

namespace TEALINK.Repositories.Generic
{
    public class CommonRepo:ICommonRepo
    {
        public Context _context;
        SqlConnection _connection;
        IConfiguration _configuration;

        public CommonRepo(Context contextt, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = contextt;
            _connection = (SqlConnection)_context.Database.GetDbConnection();
        }

        public List<CommonModelResponseDetails>CommonDropDown(CommonModelRequest commonreq)
        {
            List<CommonModelResponseDetails> commondetails = new List<CommonModelResponseDetails>();
            try
            {
                _connection.Open();
                using (SqlCommand cmd = new SqlCommand("sprCommonAll", _connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    _ = cmd.Parameters.AddWithValue("@type", commonreq.type);
                    _ = cmd.Parameters.AddWithValue("@enroll", commonreq.enroll);
                    _ = cmd.Parameters.AddWithValue("@unit", commonreq.unit);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            try
                            {
                                CommonModelResponseDetails commonModelResponseDetails = new CommonModelResponseDetails();
                                commonModelResponseDetails.id = reader.GetInt32("ID");
                                commonModelResponseDetails.name = reader.GetString("NAME");
                                commondetails.Add(commonModelResponseDetails);
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }
                }
            }
            catch (Exception ex) { }
            _connection.Close();
            return commondetails;
        }

    }
}
