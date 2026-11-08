namespace TEALINK.Models.Worker
{
    public class WorkerFetchResponse
    {
        public string CODE { get; set; }
        public bool STATUS { get; set; }
        public string MESSAGE { get; set; }
        public DateTime RESPONSETIME { get; set; }
        public List<WorkerFetchResponseDetails> WorkerDetails { get; set; }
    }
}
