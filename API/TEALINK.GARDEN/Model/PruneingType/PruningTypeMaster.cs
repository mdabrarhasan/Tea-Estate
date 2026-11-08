namespace TEALINK.GARDEN.Model.PruneingType
{
    public class PruningTypeMaster
    {
        public int ID { get; set; }
        public string NAME { get; set; }
        public string CODE { get; set; }
        public int TASK { get; set; }
        public int UNTASKDAYS { get; set; }
        public Boolean IsActive { get; set; }
        public int ENROLL { get; set; }
        public int UNITID { get; set; }
    }

    public class PruningTypeView
    {
        public int ID { get; set; }
        public string NAME { get; set; }
        public int TASK { get; set; }
        public int UNTASKDAYS { get; set; }
        public string Description { get; set; }
        public string CODE { get; set; }
        public int SLNO { get; set; }
        public Boolean IsActive { get; set; }
    }
}
