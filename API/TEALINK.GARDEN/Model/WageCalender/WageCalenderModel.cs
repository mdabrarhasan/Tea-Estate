namespace TEALINK.GARDEN.Model.WageCalender
{
    public class WageCalenderModel
    {
        public int ID {  get; set; }
        public string FROMDATE { get; set; }
        public string TODATE { get; set; }
        public string WEEKNUMBER { get; set; }
        public string WEEKLYHOLIDAY { get; set; }
        public Boolean DISABLEWEEKLYHOLIDAY { get; set; }
        public Boolean WAGEDISBURSED { get; set; }
        public int UNITID { get; set; }
        public int ENROLL { get; set; }
    }
}
