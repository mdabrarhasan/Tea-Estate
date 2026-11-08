using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

using TEALINK.GARDEN.Interface.Mobile;
using TEALINK.Models.Generic;
using TEALINK.Models.Securities;
using TEALINK.Models.Utilities;
using TEALINK.Models.Worker;
using TEALINK.Repositories.Generic;
using TEALINK.Repositories.Securities;
using TEALINK.Repositories.Utilities;
using TEALINK.Repositories.Worker;

namespace TeaForce.Controllers.Securities
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _configuration;
        public ILoginRepository _loginRepository;
        public ILogRepository _logRepository;
        public IAPIValidationRepository _apiValidateion;
        public IMessageRepository _messageRepository;
        public ICommonRepo _commonRepo;
        public IWorkerRepo _workerRepo;


        public LoginController(IWorkerRepo workerRepo, ICommonRepo commonRepo, ILoginRepository loginRepository, IConfiguration configuration, ILogRepository logRepository, IAPIValidationRepository apiValidateion, IMessageRepository messageRepository)
        {
            _configuration = configuration;
            _loginRepository = loginRepository;
            _logRepository = logRepository;
            _apiValidateion = apiValidateion;
            _messageRepository = messageRepository;
            _commonRepo = commonRepo;
            _workerRepo = workerRepo;
        }


        private bool RequestValidationPasswordReset(PasswordResetRequest Password)
        {
            bool status = false;
            try
            {
                if (!string.IsNullOrEmpty(Password.TYPE) && !string.IsNullOrEmpty(Password.USERID) && !string.IsNullOrEmpty(Password.PASSWORDNEW) && !string.IsNullOrEmpty(Password.PASSWORDOLD) && !string.IsNullOrEmpty(Password.CHANNEL) && !string.IsNullOrEmpty(Password.DEVICEINFO))
                {
                    status = true;
                }
            }
            catch
            {
                status = false;
            }
            return status;
        }


        [HttpPost]
        [Route("GetUserLogin")]
        public LoginResponse GetUserLogin([FromBody] LoginRequest login)
        {
            LoginResponse loginResponse = new LoginResponse();
            try
            {
                 LoginResponseDetails loginResponseDetails = _loginRepository.GetUserLogin(login);
                loginResponse.LoginResponseDetails = loginResponseDetails;


                if (RequestVaidation(login) && !string.IsNullOrEmpty(loginResponseDetails.MESSAGE))
                    {
                        
                            var token = generateTokennew();
                            Message message = _messageRepository.GetMessage("SUCCESS"); //get message details
                            loginResponse.CODE = message.MESSAGECODE; //api succss code if data found
                            loginResponse.STATUS = message.STATUS;
                            loginResponse.MESSAGE = message.MESSAGE;
                            loginResponse.RESPONSETIME = DateTime.Now;
                            loginResponse.LoginResponseDetails = loginResponseDetails;

        
                            CommonModelRequest common = new CommonModelRequest();
                            List<CommonModelResponseDetails> commondetails = new List<CommonModelResponseDetails>();
                          
                            common.unit = loginResponseDetails.UNIT;
                            common.enroll = loginResponseDetails.ENROLL;
                            common.type = 1;
                            commondetails = _commonRepo.CommonDropDown(common);
                            loginResponse.Division = commondetails;

                            common.unit = loginResponseDetails.UNIT;
                            common.enroll = loginResponseDetails.ENROLL;
                            common.type = 2;
                            commondetails = _commonRepo.CommonDropDown(common);
                            loginResponse.Gender = commondetails;

                            common.unit = loginResponseDetails.UNIT;
                            common.enroll = loginResponseDetails.ENROLL;
                            common.type = 3;
                            commondetails = _commonRepo.CommonDropDown(common);
                            loginResponse.WorkerType = commondetails;

                            common.unit = loginResponseDetails.UNIT;
                            common.enroll = loginResponseDetails.ENROLL;
                            common.type = 4;
                            commondetails = _commonRepo.CommonDropDown(common);
                            loginResponse.Book = commondetails;

                            common.unit = loginResponseDetails.UNIT;
                            common.enroll = loginResponseDetails.ENROLL;
                            common.type = 5;
                            commondetails = _commonRepo.CommonDropDown(common);
                            loginResponse.PluckingInterval = commondetails;

                            common.unit = loginResponseDetails.UNIT;
                            common.enroll = loginResponseDetails.ENROLL;
                            common.type = 6;
                            commondetails = _commonRepo.CommonDropDown(common);
                            loginResponse.Batch = commondetails;

                            common.unit = loginResponseDetails.UNIT;
                            common.enroll = loginResponseDetails.ENROLL;
                            common.type = 7;
                            commondetails = _commonRepo.CommonDropDown(common);
                            loginResponse.KamjariType = commondetails;

                            common.unit = loginResponseDetails.UNIT;
                            common.enroll = loginResponseDetails.ENROLL;
                            common.type = 8;
                            commondetails = _commonRepo.CommonDropDown(common);
                            loginResponse.Section = commondetails;

                            common.unit = loginResponseDetails.UNIT;
                            common.enroll = loginResponseDetails.ENROLL;
                            common.type = 9;
                            commondetails = _commonRepo.CommonDropDown(common);
                            loginResponse.Device = commondetails;

                            common.unit = loginResponseDetails.UNIT;
                            common.enroll = loginResponseDetails.ENROLL;
                            common.type = 10;
                            commondetails = _commonRepo.CommonDropDown(common);
                            loginResponse.Weightment = commondetails;


                            WorkerFetchRequest req = new WorkerFetchRequest();
                            req.TYPE = 3;
                            req.WorkerID = loginResponseDetails.UNIT;
            
                            loginResponse.Employee = _workerRepo.GetWorkerListDynamic(req);

                            common.unit = loginResponseDetails.UNIT;
                            common.enroll = loginResponseDetails.ENROLL;
                            common.type = 13;
                            commondetails = _commonRepo.CommonDropDown(common);
                            loginResponse.Hazari = commondetails;

                            common.unit = loginResponseDetails.UNIT;
                            common.enroll = loginResponseDetails.ENROLL;
                            common.type = 14;
                            commondetails = _commonRepo.CommonDropDown(common);
                            loginResponse.EmployeeTeamShift = commondetails;

                           

                }
                    else
                    {
                        Message message = _messageRepository.GetMessage("FAIL"); //get message details
                        loginResponse.CODE = message.MESSAGECODE; ////api fail code if data not found
                        loginResponse.STATUS = message.STATUS;
                        loginResponse.RESPONSETIME = DateTime.Now;
                        loginResponse.MESSAGE = message.MESSAGE;
                    }

                    //add activity log 
                    //activityLog.ACTIVITYLOG = "login.EMAIL:= " + login.USERNAME + ", login.APPLICATIONTYPE:= " + login.CHANNEL + ", login.DEVICEINFO:= " + login.DEVICEINFO;
                    //activityLog.PROCESSNAME = "LoginController";
                    //activityLog.CHANNEL = login.CHANNEL;
                    //activityLog.DOCUMENTCREATEDDATE = DateTime.Now;
                    //activityLog.DOCUMENTCREATEDBY = login.USERNAME;
                    //_logRepository.SaveActivityLog(activityLog, "ACT");
                
              
            }
            catch (Exception ex)
            {
                Message message = _messageRepository.GetMessage("ERROR"); //get message details
                loginResponse.CODE = message.MESSAGECODE; //api fail code due to exception happen
                loginResponse.STATUS = message.STATUS;
                loginResponse.RESPONSETIME = DateTime.Now;
                loginResponse.MESSAGE = message.MESSAGE;

                //errorLog.ERRORLOG = "login.EMAIL:= " + login.USERNAME + ", login.APPLICATIONTYPE:= " + login.CHANNEL + ", login.DEVICEINFO:= " + login.DEVICEINFO + ", ex.StackTrace:= " + ex.StackTrace;
                //errorLog.PROCESSNAME = "LoginController";
                //errorLog.CHANNEL = login.CHANNEL;
                //errorLog.DOCUMENTCREATEDDATE = DateTime.Now;
                //errorLog.DOCUMENTCREATEDBY = login.USERNAME;
                //_logRepository.SaveErrorLog(errorLog, "ERR");
            }
            return loginResponse;
        }

        //[HttpPost]
        //[Route("GETOTP")]
        //public OTPValidationResponse OTPValidation([FromBody] OTPValidationRequest OTP)
        //{
        //    OTPValidationResponse Response = new OTPValidationResponse();
        //    try
        //    {
        //        OTPValidationResponseDetails lOTPResponseDetails = _loginRepository.OTPValidation(OTP);
        //        if (!string.IsNullOrEmpty(lOTPResponseDetails.Message))
        //        {

        //            Response.CODE = "200";
        //            Response.STATUS = true;
        //            Response.MESSAGE = "SUCCESS";
        //            Response.OTPResponseDetails = lOTPResponseDetails;

        //            //.Session.SetString("iduserid", loginResponseDetails.ID + loginResponseDetails.USERID);

        //            activityLog.ACTIVITYLOG = "login.Mobile: " + OTP.MOBILENO + ", login.LOGINMOBINFO: " + OTP.LOGINMOBINFO;
        //            activityLog.PROCESSNAME = "OTPController";
        //            activityLog.CHANNEL = "MOBILE";
        //            activityLog.DOCUMENTCREATEDDATE = DateTime.Now;

        //            _logRepository.SaveActivityLog(activityLog, "ACT");
        //        }
        //        else
        //        {
        //            Response.CODE = "250";
        //            Response.STATUS = false;
        //            Response.MESSAGE = "FAIL";

        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        activityLog.ACTIVITYLOG = "login.Mobile: " + OTP.MOBILENO + ", login.LOGINMOBINFO: " + OTP.LOGINMOBINFO;
        //        activityLog.PROCESSNAME = "OTPController";
        //        activityLog.CHANNEL = "MOBILE";
        //        activityLog.DOCUMENTCREATEDDATE = DateTime.Now;
        //        _logRepository.SaveErrorLog(errorLog, "ERR");
        //    }
        //    return Response;
        //}

        private bool RequestVaidation(LoginRequest login)
        {
            bool status = false;
            try
            {
                if (!string.IsNullOrEmpty(login.USERNAME) && !string.IsNullOrEmpty(login.PASSWORD) && !string.IsNullOrEmpty(login.CHANNEL) && !string.IsNullOrEmpty(login.DEVICEINFO))
                {
                    status = true;
                }
            }
            catch
            {
                status = false;
            }
            return status;
        }

        


        private string generateTokennew()
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], null,
                    expires: DateTime.Now.AddMinutes(180), signingCredentials: credentials);
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
    }
}
