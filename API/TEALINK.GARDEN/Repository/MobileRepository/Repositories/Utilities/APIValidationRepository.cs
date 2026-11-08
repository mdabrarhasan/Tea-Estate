
using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using TEALINK.GARDEN;
using TEALINK.Models.Securities;

namespace TEALINK.Repositories.Securities
{
    public class APIValidationRepository : IAPIValidationRepository
    {
       public Context _context;
       SqlConnection _connection;

        IConfiguration _configuration;
        public APIValidationRepository(Context context, IConfiguration configuration)
        {
            _configuration = configuration;

            _context = context;
            _connection = (SqlConnection)_context.Database.GetDbConnection();
        }
        public APIValidationResponse apiValidation(APIValidationRequest aPIValidationRequest)
        {
            APIValidationResponse aPIValidationResponse = new APIValidationResponse();
            try
            {
                _connection.Open();
                using (SqlCommand cmd = new SqlCommand("SPGetAPIValidation", _connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    _ = cmd.Parameters.AddWithValue("@USERNAME", aPIValidationRequest.USERNAME.Trim());
                    _ = cmd.Parameters.AddWithValue("@PASSWORD", aPIValidationRequest.PASSWORD.Trim());
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            aPIValidationResponse.USERNAME = reader.GetString("USERNAME");
                            aPIValidationResponse.ASSIGNEEPARTY = reader.GetString("ASSIGNEEPARTY");
                        }
                    }
                }
            }
            catch (Exception ex) { }
            _connection.Close();
            return aPIValidationResponse;
        }
    }
}
