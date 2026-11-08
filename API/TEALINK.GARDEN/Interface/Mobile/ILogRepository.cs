using TEALINK.Models.Securities;

namespace TEALINK.Repositories.Securities
{
    public interface ILogRepository
    {
        public void SaveActivityLog(ActivityLog activityLog, string type);
       // void SaveActivityLog(ActivityLog activityLog, string v);
        public void SaveErrorLog(ErrorLog errorLog, string type);
    }
}
