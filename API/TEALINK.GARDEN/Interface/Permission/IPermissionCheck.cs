namespace TEALINK.GARDEN.Interface.Permission
{
    public interface IPermissionCheck
    {
        bool IsAllowed(int subMenuId, string action);
    }
}