using System.ComponentModel.DataAnnotations;

namespace TEALINK.GARDEN.Model.Menu
{
    public class MenuList
    {
        public int MENUID { get; set; }
        public string MENUNAME { get; set; }
        public string ICON { get; set; }
        public string CODE { get; set; }

      
        public int SUBMENUID { get; set; }
        public string SUBMENUNAME { get; set; }
        public string ROUTERLINK { get; set; }

        public int ID { get; set; }
        public int CANINSERT { get; set; }
        public int CANUPDATE { get; set; }
        public int CANDELETE { get; set; }
        public int CANVIEW { get; set; }
        public int CANPRINT { get; set; }

    }
}
