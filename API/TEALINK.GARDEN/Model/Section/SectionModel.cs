using System.ComponentModel.DataAnnotations;

namespace TEALINK.GARDEN.Model.Section
{
    public class SectionModel
    {

    }

    public class SectionManagementView
    {
        [Key]
        public int ID { get; set; }
        public string NAME { get; set; }
        public string CODE { get; set; }
        public decimal AREA { get; set; }
        public int DIVISIONID { get; set; }
        public string DIVISIONNAME { get; set; }
        public string COSTCENTRETYPE { get; set; }
        public int SLNO { get; set; }
        public int PLUCKINGID { get; set; }
        public string PLUCKINGSEC { get; set; }
        public string IRRIGATIONSECTION { get; set; }
        public int IRRIGATIONID { get; set; }
        public int USUALCOMMANDAREA { get; set; }
        public string USUALSETTINGS { get; set; }
        public int PRUNETYPEID { get; set; }
        public string PRUNETYPE { get; set; }
    }

    public class SectionManagementMaster
    {
        [Key]
        public int ID { get; set; }
        public string NAME { get; set; }
        public string CODE { get; set; }
        public decimal AREA { get; set; }
        public int DIVISIONID { get; set; }
        public string COSTCENTRETYPE { get; set; }
        public int ISPLUCKINGID { get; set; }
        public int ISIRRIGATIONID { get; set; }
        public int USUALCOMMANDAREA { get; set; }
        public string USUALSETTINGS { get; set; }
        public int PRUNETYPEID { get; set; }

        public int ENROLL { get; set; }
        public int UNITID { get; set; }
    }
}
