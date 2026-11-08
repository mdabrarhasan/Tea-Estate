using System.ComponentModel.DataAnnotations;

namespace TEALINK.GARDEN.Model.GardenManagement
{
    public class GardenManagementMaster
    {
        [Key]
        public int ID {  get; set; }
        public string NAME { get; set; }
        public string CODE { get; set; }
        public int HOLIDAYID { get; set; }
        public int INTERVALID { get; set; }
        public int UNITID { get; set; }
        public int ENROLL { get; set; }
    }

    public class GardenManagementView
    {
        [Key]
        public int ID { get; set; }
        public string NAME { get; set; }
        public string CODE { get; set; }
        public int HOLIDAYID { get; set; }
        public string HOLIDAY { get; set; }
        public int INTERVALID { get; set; }
        public string INTERVAL { get; set; }
        public string DIVISION { get; set; }
    }
}
