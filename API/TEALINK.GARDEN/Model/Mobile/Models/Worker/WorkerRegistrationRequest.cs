namespace TEALINK.Models.Worker
{
    public class WorkerRegistrationRequest
    {
        public int ID { get; set; }
        public string NAME { get; set; }
        public string CODE { get; set; }
        public int PFNUMBER { get; set; }
        public int DIVISIONID { get; set; }
        public string EMPNO { get; set; }
        public int GENDERID { get; set; }
        public int WORKERTYPEID { get; set; }
        public int WORKERSUBTYPEID { get; set; }
        public int BOOKID { get; set; }
        public int DISABLEDDISINCENTIVEID { get; set; }
        public int MONTHLYRATED { get; set; }
        public int ESSENTIALWORKER { get; set; }
        public int RETIREDID { get; set; }
        public int PLUCKINGBATCHID { get; set; }
        public int BATCHID { get; set; }
        public int KAMJARIID { get; set; }
        public string DATEOFBIRTH { get; set; }
        public string ADDRESS { get; set; }
        public string CONTACTNUMBER { get; set; }
        public int ENROLL { get; set; }
        public int UNITID { get; set; }

        public string CHANNEL { get; set; }
        public string DEVICE { get; set; }

    }
}
