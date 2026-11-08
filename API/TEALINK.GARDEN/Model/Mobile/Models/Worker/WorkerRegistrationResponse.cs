
namespace TEALINK.Models.Worker
{
    public class WorkerRegistrationResponse
    {
        public string CODE { get; set; }
        public bool STATUS { get; set; }
        public string MESSAGE { get; set; }
        public DateTime RESPONSETIME { get; set; }
        public virtual WorkerRegistrationResponseDetails WorkerRegistrationResponseDetails { get; set; }
    }
}
