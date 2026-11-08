using System.ComponentModel.DataAnnotations;

namespace TEALINK.GARDEN.Model.DailyPlucking
{
    public class DailyPluckingModel
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

    public class DailyPluckingView
    {
        [Key]
        public string ID { get; set; }
        public string Name { get; set; }
        public string KAMJARITYPE { get; set; }
        public string BOOKEMPNO { get; set; }
        public int BOOKID { get; set; }
        public string BATCH { get; set; }
        public string SHIFT { get; set; }
        public decimal TOTAL { get; set; }
        public string ATTENDENT { get; set; }
        public string PLUCKEDDATE { get; set; }
        public bool ISSELECTED { get; set; }
        public int UNITID { get; set; }
        public int COUNTID { get; set; }
        public int PresentLeaveStatus { get; set; }
        public string TotalStatus { get; set; }
    }


    public class DailyPluckingLineView
    {
        [Key]
        public int ID { get; set; }
        public int WEIGHMENTID { get; set; }
        public string TIME { get; set; }
        public string DEVICE { get; set; }
        public string SECTION { get; set; }
        public decimal QTY { get; set; }
    }

    public class DailyPluckingByWeighment
    {
        [Key]
        public int ID { get; set; }
        public decimal W1 { get; set; }
        public decimal W2 { get; set; }
        public decimal W3 { get; set; }
        public decimal W4 { get; set; }
    }

    public class DailyPluckingAttendentEmp
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
