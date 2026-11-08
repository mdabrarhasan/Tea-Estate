using TEALINK.GARDEN.Model.DailyPlucking;
using TEALINK.GARDEN.Model.DashBoard;

namespace TEALINK.GARDEN.Interface.DashBoard
{
    public interface IDashBoard
    {
        IQueryable<DashBoardView> GetDataByID(int id, string pluckeddate, int unitid, int enroll);
        
        DashBoardStatsView? GetDashboardStats(string pluckeddate, int unitid);

        List<SectionPluckingView> GetSectionPlucking(string pluckeddate, int unitid);

        List<PluckingTrendView> GetPluckingTrend(string pluckeddate, int unitid);
    }
}
