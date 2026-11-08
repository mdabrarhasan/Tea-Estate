using Microsoft.EntityFrameworkCore;
namespace TEALINK.GARDEN.Model.Permission
{
    [Keyless]
    public class OperatorPermissionListRow
    {
        public int SUBMENUID { get; set; }
        public string SUBMENUNAME { get; set; }
        public string ROUTERLINK { get; set; }
        public bool CANVIEW { get; set; }
        public bool CANINSERT { get; set; }
        public bool CANUPDATE { get; set; }
        public bool CANDELETE { get; set; }
        public bool CANPRINT { get; set; }
    }
}
