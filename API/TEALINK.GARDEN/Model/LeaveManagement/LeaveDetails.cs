namespace TEALINK.GARDEN.Model.LeaveManagement
{
    public class LeaveDetails
    {
        public int ID { get; set; }
        public string NAME { get; set; }
        public string CODE { get; set; }
        public string LTNAME { get; set; }
        public string TOTALDAYS { get; set; }
        public string REMDAYS { get; set; }


        public string APPROVEDFROMDATE { get; set; }
        public string APPROVEDTODATE { get; set; }
        public string APPROVEDDATE { get; set; }

        //public string AL { get; set; }
        //public string CL { get; set; }
        //public string SL { get; set; }
        //public string LWP { get; set; }
        //public string LL { get; set; }
    }
}
