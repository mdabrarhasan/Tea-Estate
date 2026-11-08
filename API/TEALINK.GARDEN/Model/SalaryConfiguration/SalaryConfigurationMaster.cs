namespace TEALINK.GARDEN.Model.SalaryConfiguration
{
    public class SalaryConfigurationMaster
    {
        public int ID { get; set; }
        public int UNITID { get; set; }
        public int ENROLL {  get; set; }
        public decimal WAGES { get; set; }
        public decimal OT {  get; set; }
        public int WORKERTYPEID { get; set; }
        
        public bool ALLOWANCEAPPLICABLE { get; set; }
        public bool OTALLOWANCEAPPLICABLE { get; set; }
    }
    public class SalaryConfigurationMasterView
    {
        public int ID { get; set; }
        public decimal WAGES { get; set; }
        public decimal OT { get; set; }
        public int WORKERTYPEID { get; set; }
        public string WORKERTYPE { get; set; }
        public bool ALLOWANCEAPPLICABLE { get; set; }
        public bool OTALLOWANCEAPPLICABLE { get; set; }
    }
}
