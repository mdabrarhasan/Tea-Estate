// Model/Permission/OperatorPermission.cs
using Microsoft.EntityFrameworkCore;

namespace TEALINK.GARDEN.Model.Permission
{
    [Keyless]
    public class OperatorPermissionRow
    {
        public int MENUID { get; set; }
        public string MENUNAME { get; set; }
        public int MENUSEQ { get; set; }
        public int SUBMENUID { get; set; }
        public string SUBMENUNAME { get; set; }
        public string ROUTERLINK { get; set; }
        public int SUBMENUSEQ { get; set; }
        public bool CANVIEW { get; set; }
        public bool CANINSERT { get; set; }
        public bool CANUPDATE { get; set; }
        public bool CANDELETE { get; set; }
        public bool CANPRINT { get; set; }
    }

    [Keyless]
    public class SaveOperatorPermissionDto
    {
        public int MenuId { get; set; }
        public int SubMenuId { get; set; }
        public bool CanView { get; set; }
        public bool CanInsert { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }
        public bool CanPrint { get; set; }
        public int UpdatedBy { get; set; }
    }
}