namespace TEALINK.GARDEN.Model.WorkerManagement
{
    public class DailyAttendenceReport
    {
        public int ID { get; set; }
        public string NAME { get; set; }
        public string CODE { get; set; }

        public string BOOK { get; set; }

        public string WORKERTYPE { get; set; }
        public int ATTENDENCE { get; set; }
        public string BATCH { get; set; }
        public string KAMJARI { get; set; }
        public string INTIME { get; set; }
        public string OUTTIME { get; set; }
        public string LEAVECODE { get; set; }
    }
}
