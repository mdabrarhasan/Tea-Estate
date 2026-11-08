using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TEALINK.GARDEN.Interface.DashBoard;
using TEALINK.GARDEN.Model.DailyPlucking;
using TEALINK.GARDEN.Model.DashBoard;

namespace TEALINK.GARDEN.Repository.DashBoardRepository
{
    public class RDashBoard:IDashBoard
    {
        private readonly Context _context;

       public RDashBoard(Context context)
        {
            _context = context;
        }
        public IQueryable<DashBoardView> GetDataByID(int id, string pluckeddate, int unitid, int enroll)
        {
            SqlParameter CODE = new SqlParameter("@CODE", id);
            SqlParameter PLUCKEDDATE = new SqlParameter("@PLUCKEDDATE", pluckeddate == null ? "" : pluckeddate);
            SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
            SqlParameter ENROLL = new SqlParameter("@ENROLL", enroll);
            return _context.DashBoardView.FromSqlRaw("Exec SPGETDASHBOARD @CODE,@PLUCKEDDATE,@UNITID, @ENROLL", CODE, PLUCKEDDATE, UNITID, ENROLL);
        }

        // RDashBoard.cs — add this method
        // RDashBoard.cs
        public DashBoardStatsView? GetDashboardStats(string pluckeddate, int unitid)
        {
            SqlParameter PLUCKEDDATE = new SqlParameter("@PLUCKEDDATE", pluckeddate ?? "");
            SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
            return _context.DashBoardStatsView
                .FromSqlRaw("EXEC SPGETDASHBOARDV2 @PLUCKEDDATE, @UNITID", PLUCKEDDATE, UNITID)
                .AsEnumerable()
                .FirstOrDefault();
        }

        public List<SectionPluckingView> GetSectionPlucking(string pluckeddate, int unitid)
        {
            SqlParameter PLUCKEDDATE = new SqlParameter("@PLUCKEDDATE", pluckeddate ?? "");
            SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
            return _context.SectionPluckingView
                .FromSqlRaw("EXEC SPGETSECTIONPLUCKING @PLUCKEDDATE, @UNITID", PLUCKEDDATE, UNITID)
                .AsEnumerable()
                .ToList();
        }
        public List<PluckingTrendView> GetPluckingTrend(string pluckeddate, int unitid)
        {
            SqlParameter PLUCKEDDATE = new SqlParameter("@PLUCKEDDATE", pluckeddate ?? "");
            SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
            return _context.PluckingTrendView
                .FromSqlRaw("EXEC SPGETPLUCKINGTREND @PLUCKEDDATE, @UNITID", PLUCKEDDATE, UNITID)
                .AsEnumerable()   // EXEC is non-composable — materialize before ToList
                .ToList();
        }
    }
}
