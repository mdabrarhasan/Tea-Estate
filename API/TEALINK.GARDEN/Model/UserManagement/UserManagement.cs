using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TEALINK.GARDEN.Model.UserManagement
{
    [Keyless]
    public class UserManagementMaster
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Emailid { get; set; }
        public string Contactnumber { get; set; }
        public int Usertypeid { get; set; }
        public string Usertypename { get; set; }   // grid display only
        public string Loginid { get; set; }
        public string Passwords { get; set; }
        public string Division { get; set; }
        public string Book { get; set; }
        public int UnitId { get; set; }
    }

    [Keyless]
    public class UserTypeList
    {
        public int Usertypeid { get; set; }
        public string Username { get; set; }
    }
}