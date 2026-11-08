using System.ComponentModel.DataAnnotations;

namespace TEALINK.GARDEN.Model.Auth
{
    public class AccessViewModel
    {
        [Key]
        public int PID { get; set; }
        public string EmpName { get; set; }
        public int MenuID { get; set; }
        public string Label { get; set; }
        public int SubMenuID { get; set; }
        public string SubLabel { get; set; }
        public int CanView { get; set; }
        public int CanInsert { get; set; }
        public int CanUpdate { get; set; }
        public int CanDelete { get; set; }
        public int CanPrint { get; set; }
        public string UserID { get; set; }
    }
}
