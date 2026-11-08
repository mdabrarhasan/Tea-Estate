using TEALINK.Models.Generic;
using TEALINK.Models.Worker;

namespace TEALINK.Models.Securities
{
    public class LoginResponse
    {
        public string CODE { get; set; }
        public bool STATUS { get; set; }
        public string MESSAGE { get; set; }
        public DateTime RESPONSETIME { get; set; }
        public virtual LoginResponseDetails LoginResponseDetails { get; set; }
        public List<CommonModelResponseDetails> Division { get; set; }
        public List<CommonModelResponseDetails> Gender { get; set; }
        public List<CommonModelResponseDetails> WorkerType { get; set; }
        public List<CommonModelResponseDetails> Book { get; set; }
        public List<CommonModelResponseDetails> Batch { get; set; }
        public List<CommonModelResponseDetails> PluckingInterval { get; set; }
        public List<CommonModelResponseDetails> KamjariType { get; set; }
        public List<CommonModelResponseDetails> Section { get; set; }
        public List<CommonModelResponseDetails> Device { get; set; }
        public List<CommonModelResponseDetails> Weightment { get; set; }

        public List<dynamic> Employee { get; set; }
        public List<CommonModelResponseDetails> Hazari { get; set; }
        public List<CommonModelResponseDetails> EmployeeTeamShift { get; set; }

        public string TOKEN { get; set; }
    }
}

