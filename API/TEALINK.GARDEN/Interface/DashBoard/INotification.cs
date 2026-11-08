// TEALINK.GARDEN.Interface.DashBoard/INotification.cs
using TEALINK.GARDEN.Model.DashBoard;

namespace TEALINK.GARDEN.Interface.DashBoard
{
    public interface INotification
    {
        IQueryable<NotificationSummary> GetSummary(int unitid, DateTime date);
    }
}