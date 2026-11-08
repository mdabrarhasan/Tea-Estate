using System.ComponentModel.DataAnnotations;

namespace TEALINK.GARDEN.Model.Division
{
    public class DivisionMaster
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int UnitID { get; set; }
    }
}

