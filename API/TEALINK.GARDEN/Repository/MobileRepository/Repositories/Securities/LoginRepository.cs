
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

using TEALINK.GARDEN.Interface.Mobile;
using TEALINK.GARDEN;
using TEALINK.Models.Securities;
using TEALINK.Models.Generic;
using TEALINK.GARDEN.Model.Mobile.Models.Generic;
using static Azure.Core.HttpHeader;
using System.Collections.Generic;

namespace TEALINK.Repositories.Securities
{
    public class LoginRepository : ILoginRepository
    {
        public Context _context;
        SqlConnection _connection;
        IConfiguration _configuration;
        public LoginRepository(Context contextt, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = contextt;
            _connection = (SqlConnection)_context.Database.GetDbConnection();
        }

        public List<CommonModelResponseDetails> CommonDropDown(int type)
        {
            List<CommonModelResponseDetails> commondetails = new List<CommonModelResponseDetails>();
            try
            {
               
                using (SqlCommand cmd = new SqlCommand("sprCommonAllWithOutUnit", _connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    _ = cmd.Parameters.AddWithValue("@type", type);
                 

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
            return commondetails;
        }

        public LoginResponseDetails GetUserLogin(LoginRequest loginRequest)
        {
            LoginResponseDetails loginDetails = new LoginResponseDetails();
            try
            {
                _connection.Open();
                using (SqlCommand cmd = new SqlCommand("sprLoginMobile", _connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    _ = cmd.Parameters.AddWithValue("@TYPE", 0);
                    _ = cmd.Parameters.AddWithValue("@USERNAME", loginRequest.USERNAME.Trim());
                    _ = cmd.Parameters.AddWithValue("@PASSWORD", loginRequest.PASSWORD.Trim());

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            try
                            {
                                string message = reader.GetString("_MESSAGE");
                                if (message == "GO")
                                {
                                    loginDetails.ENROLL = reader.GetInt32("ENROLL");
                                  //  loginDetails.CODE = reader.GetString("CODE");
                                    loginDetails.NAME = reader.GetString("NAME");
                                    loginDetails.UNIT = reader.GetInt32("UNIT");
                                    loginDetails.MESSAGE = message;
                                   
                                }
                                else
                                {
                                    loginDetails.MESSAGE = message;
                                }
                            }catch(Exception ex)
                            {

                            }
                        }
                    }
                }
            }
            catch (Exception ex) { }
            _connection.Close();
            return loginDetails;
        }

        public PasswordResetResponseDetails PasswordReset(PasswordResetRequest PasswordReset)
        {
            PasswordResetResponseDetails passwordResetResponse = new PasswordResetResponseDetails();
            try
            {
                _connection.Open();
                using (SqlCommand cmd = new SqlCommand("sprChangeUserPassword", _connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@strtype", PasswordReset.TYPE.Trim());
                    cmd.Parameters.AddWithValue("@account", PasswordReset.USERID.Trim());
                    cmd.Parameters.AddWithValue("@passwordOld", PasswordReset.PASSWORDOLD.Trim());
                    cmd.Parameters.AddWithValue("@passwordnew", PasswordReset.PASSWORDNEW.Trim());

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            passwordResetResponse.MobileNumber = reader.GetString("MOBILE");
                            passwordResetResponse.Message = reader.GetString("Message_");
                            passwordResetResponse.Otp = reader.GetString("OTP");
                        }
                    }
                }
            }
            catch (Exception ex) { }
            _connection.Close();
            return passwordResetResponse;
        }




        public OTPValidationResponseDetails OTPValidation(OTPValidationRequest oTPRequest)
        {
            OTPValidationResponseDetails OTPDetails = new OTPValidationResponseDetails();
            try
            {
                _connection.Open();
                using (SqlCommand cmd = new SqlCommand("sprSignUpOTPValidate", _connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@account", oTPRequest.Email.Trim());

                    cmd.Parameters.AddWithValue("@strPhone", oTPRequest.MOBILENO.Trim());
                    cmd.Parameters.AddWithValue("@OTP", oTPRequest.OTP.Trim());

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            OTPDetails.Message = reader.GetString("Message_");

                        }
                    }
                }
            }
            catch (Exception ex) { }
            _connection.Close();
            return OTPDetails;
        }
    }
}
