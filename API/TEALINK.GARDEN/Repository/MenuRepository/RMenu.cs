using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TEALINK.GARDEN.Interface.Menu;
using TEALINK.GARDEN.Model.Menu;

namespace TEALINK.GARDEN.Repository.MenuRepository
{
    public class RMenu : IMenu
    {
        private readonly Context _context;
        public RMenu(Context context)
        {
            _context = context;
        }
        public IQueryable<Menus> GetMenu(string userid)
        {
            return _context.Menu.FromSqlRaw("Exec SPGETMENU @USERID", new SqlParameter("USERID", userid));
        }

        public IQueryable<SubMenu> GetSubMenu(string userid)
        {
            return _context.SubMenu.FromSqlRaw("Exec SPRGETSUBMENU @USERID", new SqlParameter("USERID", userid));
        }

        public IQueryable<MenuList> GetMenuList(string userid)
        {
            return _context.MenuList.FromSqlRaw("Exec SPGETMENULIST @USERID", new SqlParameter("USERID", userid));
        }
    }
}
