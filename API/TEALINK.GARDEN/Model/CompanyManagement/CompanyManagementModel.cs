using System.ComponentModel.DataAnnotations;

namespace TEALINK.GARDEN.Model.CompanyManagement
{
    public class CompanyManagementModel
    {
        [Key]
        public int UNITID { get; set; }
        public string UNITNAME { get; set; }
        public string ADDRESS { get; set; }
        public string EMAIL { get; set; }
        public string CONTACTNUMBER { get; set; }
        public string REPLYTOEMAIL { get; set; }
        public string CONTACTPERSON { get; set; }

        public int ROUNDOFFRULEID { get; set; }
        public string ROUNDOFROULE { get; set; }

        public int UNITAREAPLUCKEDID { get; set; }
        public string UNITFORAREAPLUCKED { get; set; }

        public int UNITFORPLUCKERPERAREAID { get; set; }
        public string UNITFORPLUCKERPERAREA { get; set; }

        public int UNITFORLEAFAREAID { get; set; }
        public string UNITFORLEAFAREA { get; set; }


        public int FACERECOGNIZATIONRULEID { get; set; }
        public string FACERECOGNIZATION { get; set; }

    }
}
