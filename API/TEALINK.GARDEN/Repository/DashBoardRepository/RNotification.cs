// TEALINK.GARDEN.Repository.DashBoardRepository/RNotification.cs
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TEALINK.GARDEN.Interface.DashBoard;
using TEALINK.GARDEN.Model.DashBoard;

namespace TEALINK.GARDEN.Repository.DashBoardRepository
{
    public class RNotification : INotification
    {
        private readonly Context _context;

        public RNotification(Context context)
        {
            _context = context;
        }

        public IQueryable<NotificationSummary> GetSummary(int unitid, DateTime date)
        {
            SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
            SqlParameter DATE = new SqlParameter("@DATE", date);
            return _context.NotificationSummary
                           .FromSqlRaw("Exec SPGETNOTIFICATIONSUMMARY @UNITID, @DATE", UNITID, DATE);
        }
    }
}