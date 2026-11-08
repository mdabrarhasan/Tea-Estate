namespace TEALINK.GARDEN.Model.Mobile.Models.Attendance
{
    public class AttendanceRequest
    {
        public int Type { get; set; }
        public int ShiftId { get; set; }
        public int EmpCode { get; set; }
        public int Operator { get; set; }
        public int SectionId { get; set; }
        public int DeviceId { get; set; }
        public int HazariCode { get; set; }
        public int UnitId { get; set; }            // Added missing UnitId
        public int KamjariId { get; set; }         // Added missing KamjariId
        public int WeightmentId { get; set; }
        public decimal PluckedQty { get; set; }
        public decimal PictureMatched { get; set; }
        public DateTime AttendanceDATE { get; set; }
        public decimal Qty { get; set; }
    }
}
