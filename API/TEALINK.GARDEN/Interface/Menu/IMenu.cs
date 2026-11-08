using TEALINK.GARDEN.Model.Menu;

namespace TEALINK.GARDEN.Interface.Menu
{
    public interface IMenu
    {
        IQueryable<Menus> GetMenu(string userid);
        IQueryable<SubMenu> GetSubMenu(string userid);
        IQueryable<MenuList> GetMenuList(string userid);
    }
}
