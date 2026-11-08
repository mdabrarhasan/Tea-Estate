using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using TEALINK.GARDEN.Interface.Mobile;
using TEALINK.Models.Generic;
using TEALINK.Models.Utilities;
using TEALINK.Repositories.Securities;
using TEALINK.Repositories.Utilities;

namespace TeaForce.Controllers.Generic
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CommonController : ControllerBase
    {
        private IConfiguration _configuration;

        public ILogRepository _logRepository;
        public IAPIValidationRepository _apiValidateion;
        public IMessageRepository _messageRepository;
        public ICommonRepo _commonRepo;

        //ActivityLog activityLog = new ActivityLog();
        //ErrorLog errorLog = new ErrorLog();
        //JWTToken jWTToken = new JWTToken();

        public CommonController(ICommonRepo commonRepo,IWorkerRepo workerRepo, IConfiguration configuration, ILogRepository logRepository, IAPIValidationRepository apiValidateion, IMessageRepository messageRepository)
        {
            _configuration = configuration;
            _logRepository = logRepository;
            _apiValidateion = apiValidateion;
            _messageRepository = messageRepository;
            _commonRepo = commonRepo;
        }

        [HttpPost]
        [Route("CommonDropDown")]

        public CommonModelResponse CommonDropDown([FromBody] CommonModelRequest commonReq)
        {
            CommonModelResponse commonModelResponse = new CommonModelResponse();
            try
            {
                if (APIAuthenticationCheck())
                {
                    List<CommonModelResponseDetails> commondetails = _commonRepo.CommonDropDown(commonReq);

                    if (commonReq.type>0)
                    {
                        Message message = _messageRepository.GetMessage("SUCCESS");
                        commonModelResponse.CODE = message.MESSAGECODE;
                        commonModelResponse.STATUS = message.STATUS;
                        commonModelResponse.MESSAGE = message.MESSAGE;
                        commonModelResponse.RESPONSETIME = DateTime.Now;
                        commonModelResponse.ResponseDetails = commondetails;
                    }
                    else
                    {
                        Message message = _messageRepository.GetMessage("FAIL");
                        commonModelResponse.CODE = message.MESSAGECODE;
                        commonModelResponse.STATUS = message.STATUS;
                        commonModelResponse.MESSAGE = message.MESSAGE;
                        commonModelResponse.RESPONSETIME = DateTime.Now;
                    }

                    //add activity log 
                    //activityLog.ACTIVITYLOG = "login.EMAIL:= " + commonReq.enroll.ToString() + ", login.APPLICATIONTYPE:= " + commonReq.channel + ", login.DEVICEINFO:= " + commonReq.device;
                    //activityLog.PROCESSNAME = "LoginController";
                    //activityLog.CHANNEL = commonReq.channel;
                    //activityLog.DOCUMENTCREATEDDATE = DateTime.Now;
                    //activityLog.DOCUMENTCREATEDBY = commonReq.enroll.ToString();
                    //_logRepository.SaveActivityLog(activityLog, "ACT");
                }
                else
                {
                    Message message = _messageRepository.GetMessage("APIACCESS"); //get message details
                    commonModelResponse.CODE = message.MESSAGECODE;//api fail code if unauthorized access to the api. such as api calling username and password 
                    commonModelResponse.STATUS = message.STATUS;
                    commonModelResponse.RESPONSETIME = DateTime.Now;
                    commonModelResponse.MESSAGE = message.MESSAGE;
                }
            }
            catch (Exception ex)
            {

            }
            return commonModelResponse;
        }

        private Boolean APIAuthenticationCheck()
        {
            return true;
            //bool status = false;
            //try
            //{
            //    status = new APIValidation(_apiValidateion).APIValidationSatus(this.HttpContext.Request.Headers["Authorization"]);
            //}
            //catch { status = false; }
            //return status;
        }


    }
}
