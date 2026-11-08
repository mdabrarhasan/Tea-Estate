namespace TEALINK.GARDEN.Model.WorkerBatch
{
    public class WorkerBatchMaster
    {
        public int ID { get; set; }
        public string BATCHNAME { get; set; }
        public int KAMJARITYPEID { get; set; }
        public int SHIFTID { get; set; }
        public int DIVISIONID { get; set; }
        public Boolean ISOUTSIDEWORKER { get; set; }
        public int UNITID { get; set; }
        public int ENROLL { get; set; }
    }

    public class WorkerBatchMasterView
    {
        public int ID { get; set; }
        public string NAME { get; set; }
        public int KAMJARITYPEID { get; set; }
        public string KAMJARITYPE { get; set; }
        public int SHIFTID { get; set; }
        public string SHIFT { get; set; }

        public int DIVISIONID { get; set; }
        public string DIVISIONNAME { get; set; }

        public Boolean ISOUTSIDEWORKER { get; set; }
        public int SLNO { get; set; }
    }
}
