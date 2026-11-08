using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TEALINK.GARDEN.Interface.Mobile;
using TEALINK.GARDEN.Model.Mobile.Models.Worker;
using TEALINK.Models.Securities;
using TEALINK.Models.Utilities;
using TEALINK.Models.Worker;
using TEALINK.Repositories.Securities;
using TEALINK.Repositories.Utilities;

namespace TeaForce.Controllers.Worker
{
    [Route("api/[controller]")]
    [ApiController]
  
    public class WorkerController : ControllerBase
    {
        private IConfiguration _configuration;

        public ILoginRepository _loginRepository;
        public ILogRepository _logRepository;
        public IAPIValidationRepository _apiValidateion;
        public IMessageRepository _messageRepository;
        public IWorkerRepo _workerRepo;

        ActivityLog activityLog = new ActivityLog();
        ErrorLog errorLog = new ErrorLog();
      

        public WorkerController(IWorkerRepo workerRepo, IConfiguration configuration, ILogRepository logRepository, IAPIValidationRepository apiValidateion, IMessageRepository messageRepository)
        {
            _configuration = configuration;
            _workerRepo = workerRepo;
            _logRepository = logRepository;
            _apiValidateion = apiValidateion;
            _messageRepository = messageRepository;
        }

        [HttpPost]
        [Route("WorkerRegistration")]

        public WorkerRegistrationResponse WorkerRegistration([FromBody] WorkerRegistrationRequest  workerReq)
        {
            WorkerRegistrationResponse workerRegistrationResponse = new WorkerRegistrationResponse();
            try
            {
                if (APIAuthenticationCheck())
                {
                    WorkerRegistrationResponseDetails RegistrationResponse = _workerRepo.Registration(workerReq);
                   

                    if (!string.IsNullOrEmpty(RegistrationResponse.CODE))
                    {
                        Message message = _messageRepository.GetMessage("SUCCESS");
                        workerRegistrationResponse.CODE = message.MESSAGECODE;
                        workerRegistrationResponse.STATUS = message.STATUS;
                        workerRegistrationResponse.MESSAGE = message.MESSAGE;
                        workerRegistrationResponse.RESPONSETIME = DateTime.Now;
                        workerRegistrationResponse.WorkerRegistrationResponseDetails = RegistrationResponse;
                    }
                    else
                    {
                        Message message = _messageRepository.GetMessage("FAIL");
                        workerRegistrationResponse.CODE = message.MESSAGECODE;
                        workerRegistrationResponse.STATUS = message.STATUS;
                        workerRegistrationResponse.MESSAGE = message.MESSAGE;
                        workerRegistrationResponse.RESPONSETIME = DateTime.Now;
                    }

                    //add activity log 
                    activityLog.ACTIVITYLOG = "login.EMAIL:= " + workerReq.NAME + ", login.APPLICATIONTYPE:= " + workerReq.CHANNEL + ", login.DEVICEINFO:= " + workerReq.DEVICE;
                    activityLog.PROCESSNAME = "LoginController";
                    activityLog.CHANNEL = workerReq.CHANNEL;
                    activityLog.DOCUMENTCREATEDDATE = DateTime.Now;
                    activityLog.DOCUMENTCREATEDBY = workerReq.NAME;
                   // _logRepository.SaveActivityLog(activityLog, "ACT");
                }
                else
                {
                    Message message = _messageRepository.GetMessage("APIACCESS"); //get message details
                    workerRegistrationResponse.CODE = message.MESSAGECODE;//api fail code if unauthorized access to the api. such as api calling username and password 
                    workerRegistrationResponse.STATUS = message.STATUS;
                    workerRegistrationResponse.RESPONSETIME = DateTime.Now;
                    workerRegistrationResponse.MESSAGE = message.MESSAGE;
                }
            }
            catch (Exception ex)
            {
               
            }
            return workerRegistrationResponse;
        }

        [HttpPost]
        [Route("GetWorker")]

        public WorkerFetchResponse GetWorker([FromBody] WorkerFetchRequest getworkerReq)
        {
            WorkerFetchResponse workerFetchResponse = new WorkerFetchResponse();
            try
            {
                if (APIAuthenticationCheck())
                {
                    List<WorkerFetchResponseDetails> workerResponse = _workerRepo.GetWorker(getworkerReq);


                    if (1==1)
                    {
                        Message message = _messageRepository.GetMessage("SUCCESS");
                        workerFetchResponse.CODE = message.MESSAGECODE;
                        workerFetchResponse.STATUS = message.STATUS;
                        workerFetchResponse.MESSAGE = message.MESSAGE;
                        workerFetchResponse.RESPONSETIME = DateTime.Now;
                        workerFetchResponse.WorkerDetails = workerResponse;
                    }
                    else
                    {
                        Message message = _messageRepository.GetMessage("FAIL");
                        workerFetchResponse.CODE = message.MESSAGECODE;
                        workerFetchResponse.STATUS = message.STATUS;
                        workerFetchResponse.MESSAGE = message.MESSAGE;
                        workerFetchResponse.RESPONSETIME = DateTime.Now;
                    }

                    //add activity log 
                    activityLog.ACTIVITYLOG = "login.EMAIL:= " + getworkerReq.Enroll + ", login.APPLICATIONTYPE:= " + getworkerReq.Enroll + ", login.DEVICEINFO:= " + getworkerReq.Enroll;
                    activityLog.PROCESSNAME = "LoginController";
                    activityLog.CHANNEL = getworkerReq.Enroll;
                    activityLog.DOCUMENTCREATEDDATE = DateTime.Now;
                    activityLog.DOCUMENTCREATEDBY = getworkerReq.Enroll;
                 //   _logRepository.SaveActivityLog(activityLog, "ACT");
                }
                else
                {
                    Message message = _messageRepository.GetMessage("APIACCESS"); //get message details
                    workerFetchResponse.CODE = message.MESSAGECODE;//api fail code if unauthorized access to the api. such as api calling username and password 
                    workerFetchResponse.STATUS = message.STATUS;
                    workerFetchResponse.RESPONSETIME = DateTime.Now;
                    workerFetchResponse.MESSAGE = message.MESSAGE;
                }
            }
            catch (Exception ex)
            {

            }
            return workerFetchResponse;
        }

        [HttpPost]
        [Route("GetWorkerByBook")]
        public List<dynamic> GetWorker([FromQuery] int Book)
        {
            List<dynamic> Response = new List<dynamic>();

            Response = _workerRepo.GetBookByWorker(Book);

            return Response;
        }


        [HttpPost]
        [Route("ImageManipulation")]
        public List<dynamic> WorkerImageManipulation(WorkerImageRequest workerImageRequest)
        {
            List<dynamic> Response = new List<dynamic>();

            Response = _workerRepo.WorkerImageManipulation(workerImageRequest);

            return Response;
        }


        [HttpPost]
        [Route("FetchImage")]
        public List<dynamic> FetchWorkerImage(int bookId)
        {
            List<dynamic> Response = new List<dynamic>();

            Response = _workerRepo.FetchWorkerImage(bookId);

            return Response;
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
