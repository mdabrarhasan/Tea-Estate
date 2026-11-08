using System.ComponentModel.DataAnnotations;

namespace TEALINK.GARDEN.Model.DailyNonPlucking
{
    public class DailyNonPluckingModel
    {
        [Key]
        public string PLUCKDATE { get; set; }
        public string ID { get; set; }
        public int UNITID { get; set; }
        public int OPERATIONID { get; set; }
        public int DEVICEID { get; set; }
        public int BATCHID { get; set; }
        public int WEIGHMENTID { get; set; }
        public int DIVISIONID { get; set; }
    }

    public class DailyNonPluckingView
    {
        [Key]
        public string ID { get; set; }
        public string Name { get; set; }
        public string KAMJARI { get; set; }
        public string BATCH { get; set; }
        public string SECTION { get; set; }
        public string HAZRI { get; set; }
        public string DBLYBATCH { get; set; }
        public string DBLYKAMJARI { get; set; }
        public string DBLYSECTION { get; set; }
        public int DBLY { get; set; }
        public string DBLYTYPE { get; set; }
        public string BOOKEMPNO { get; set; }
        public string ATTENDDATE { get; set; }
        public bool ISSELECTED { get; set; }
        public int UNITID { get; set; }
        public int WID { get; set; } //as bookid
        public string PLUCKEDDATE { get; set; }
        public string TIME { get; set; }
        public string KAMJARITYPE { get; set; }
        public string SHIFT { get; set; }
        public decimal TOTAL { get; set; }
        public string ATTENDENT { get; set; }
        public int PresentLeaveStatus { get; set; }
        public string TotalStatus { get; set; }
    }


    public class DailyNonPluckingLineView
    {
        [Key]
        public int ID { get; set; }
        public int KAMJARIID { get; set; }
        public int TEAMID { get; set; }
       
        public string BATCHNAME { get; set; }
        public string TEAMNAME { get; set; }
        public string TIME { get; set; }
        public string DEVICE { get; set; }
        public string SECTION { get; set; }
    }


    public class DailyNonPluckingAttendentEmp
    {
        [Key]
        public int ID { get; set; }
        public int FULL { get; set; }
        public int HALF { get; set; }
        public int EXTRA { get; set; }
        public int FULLPLUS { get; set; }
        public int ABSENT { get; set; }
    }

}
