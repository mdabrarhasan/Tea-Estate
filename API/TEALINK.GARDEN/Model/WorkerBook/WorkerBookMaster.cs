namespace TEALINK.GARDEN.Model.WorkerBook
{
    public class WorkerBookMaster
    {
        public int ID { get; set; }
        public string NAME { get; set; }
        public string CODE { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean ISRETIREMENT { get; set; }
        public int ENROLL { get; set; }
        public int UNITID { get; set; }
    }

    public class WorkerBookView
    {
        public int ID { get; set; }
        public string NAME { get; set; }
        public string Description { get; set; }
        public string CODE { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean ISRETIREMENT { get; set; }
    }
}
