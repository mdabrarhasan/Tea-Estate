using System.Security.AccessControl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TEALINK.GARDEN.Model.Mobile.Models.Attendance;
using TEALINK.Models.Securities;
using TEALINK.Models.Utilities;
using TEALINK.Repositories.Attendance;
using TEALINK.Repositories.Securities;
using TEALINK.Repositories.Utilities;

namespace TEALINK.Controllers.Attendance
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceRepo _attendanceRepo;
        public ILogRepository _logRepository;
        public IAPIValidationRepository _apiValidateion;
        public IMessageRepository _messageRepository;
        private IConfiguration _configuration;

        ActivityLog activityLog = new ActivityLog();
        ErrorLog errorLog = new ErrorLog();
      //  JWTToken jWTToken = new JWTToken();

        public AttendanceController(IConfiguration configuration, ILogRepository logRepository, IAPIValidationRepository apiValidateion, IMessageRepository messageRepository,IAttendanceRepo attendanceRepo)
        {
            _configuration = configuration;
            _logRepository = logRepository;
            _apiValidateion = apiValidateion;
            _messageRepository = messageRepository;
            _attendanceRepo = attendanceRepo;
        }


        [HttpPost]
        [Route("SubmitAttendance")]

        public AttendanceResponse SubmitAttendance([FromBody] AttendanceRequest attnRequest)
        {
            AttendanceResponse attnFetchResponse = new AttendanceResponse();
           
            try
            {
                    AttendanceResponse attnResponse = _attendanceRepo.SubmitAttendance(attnRequest);
                    if(attnResponse != null) 
                    {
                         Message message = _messageRepository.GetMessage("SUCCESS");
                         attnFetchResponse.Code = attnResponse.Code;
                         attnFetchResponse.Status = attnResponse.Status;
                         attnFetchResponse.Message = attnResponse.Message;
                         attnFetchResponse.ResponseTime = DateTime.Now;
                    }
                   else 
                    {
                        Message message = _messageRepository.GetMessage("FAIL");
                        attnFetchResponse.Code = message.MESSAGECODE;
                        attnFetchResponse.Status = "False";
                        attnFetchResponse.Message = message.MESSAGE;
                        attnFetchResponse.ResponseTime = DateTime.Now;
                      }
                    //add activity log 
                    activityLog.ACTIVITYLOG = "login.EMAIL:= " + attnRequest.EmpCode + ", login.APPLICATIONTYPE:= " + attnRequest.EmpCode + ", login.DEVICEINFO:= " + attnRequest.DeviceId;
                    activityLog.PROCESSNAME = "LoginController";
                    activityLog.CHANNEL = "";
                    activityLog.DOCUMENTCREATEDDATE = DateTime.Now;
                    activityLog.DOCUMENTCREATEDBY = "";
                    _logRepository.SaveActivityLog(activityLog, "ACT");
              
               
            }
            catch (Exception ex)
            {

            }
            return attnFetchResponse;
        }
    }
}
