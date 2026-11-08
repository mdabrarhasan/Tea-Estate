using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TEALINK.GARDEN.Model.Auth
{
    public class Login
    {
        [Key]
        public string UserID { get; set; }
        public string Emppass { get; set; }
    }

    public class LoginModel
    {
        [Key]
        public int ROWID { get; set; }
        public int Enroll { get; set; }
        
        public string Name { get; set; }
        public int Unit { get; set; }
        
        public string DesignationName { get; set; }
        public int TypeId { get; set; }

    }

}
