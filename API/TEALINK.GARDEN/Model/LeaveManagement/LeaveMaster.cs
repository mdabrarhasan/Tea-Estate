namespace TEALINK.GARDEN.Model.LeaveManagement
{
    public class LeaveMaster
    {
        public int ID { get; set; }
        public int WORKER { get; set; }
        public int LEAVETYPEID { get; set; }
        public int DIVISIONID { get; set; }
        public int UNITID { get; set; }
        public int ENROLL { get; set; }
        public string FROMDATE { get; set; }
        public string TODATE { get; set; }

    }
}
