namespace TEALINK.GARDEN.Model.ShiftManagement
{
    public class ShiftMaster
    {
        public int ID {  get; set; }
        public string CODE { get; set; }
        public string NAME { get; set; }
        public int KAMJID { get; set; }
        public Boolean DBLYSHIFT { get; set; }
        public Boolean GARDENSHIFT { get; set; }
        public int ENROLL { get; set; }
        public int UNITID { get; set; }
    }

    public class ShiftMasterView
    {
        public int ID { get; set; }
        public string CODE { get; set; }
        public string NAME { get; set; }
        public string KAMJNAME { get; set; }
        public Boolean DBLYSHIFT { get; set; }
        public Boolean GARDENSHIFT { get; set; }
        public Boolean ISACTIVE { get; set; }
    }

    public class ShiftLineView
    {
        public int ID { get; set; }
        public string NAME { get; set; }
        public string FIRSTSHIFTSTART { get; set; }
        public string FIRSTSHIFTEND { get; set; }


        public int MINFREQ { get; set; }
        public decimal MAXDELAY { get; set; }
        public decimal MINHOURFULLATTENED { get; set; }
        public decimal MINHOURHALFATTENED { get; set; }
        public decimal MINHOUROVERTIME { get; set; }
        public decimal MINQTYHALFATT { get; set; }
        public decimal MINQTYOVERTIME { get; set; }
    }
}
