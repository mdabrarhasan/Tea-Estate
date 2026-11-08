namespace TEALINK.GARDEN.Model.WorkerType
{

    public class WorkerTypeModel
    {
        public int ID { get; set; }
        public string CODE { get; set; }
        public string NAME { get; set; }
        public Boolean EXCLUDEGARDENNAMEINCARD { get; set; }
        public Boolean PAIDWEEKLYOFF { get; set; }
        public Boolean PAIDHOLIDAYAPPLICABLE { get; set; }
        public Boolean PFAPPLICABLE { get; set; }
        public Boolean DEPTALLOWANCE { get; set; }
        public Boolean OTAPPLICABLE { get; set; }
        public Boolean ATTENDENCEINCENTIVE { get; set; }
        public string DESCRIPTION { get; set; }
        public Boolean ISACTIVE { get; set; }
        public int CREATEDBY { get; set; }
        public string CREATEDDATE { get; set; }
        public int UPDATEDBY { get; set; }
        public string UPDATEDDATE { get; set; }
        public int UNITID { get; set; }
    }
}
