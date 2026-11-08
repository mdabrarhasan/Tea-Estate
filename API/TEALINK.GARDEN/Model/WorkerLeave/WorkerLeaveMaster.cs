namespace TEALINK.GARDEN.Model.WorkerLeave
{
    public class WorkerLeaveMaster
    {
        public int ID { get; set; }
        public string LEAVECODE { get; set; }
        public string ATTENDANCENAME { get; set; }
        public decimal MAXALLOWED  { get; set; }    
        public int ATTENDENCE { get; set; }
        public int ENROLL { get; set; }
        public int UNITID { get; set; }

    }
}
