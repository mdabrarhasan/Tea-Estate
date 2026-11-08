// TEALINK.GARDEN.Model.DashBoard/NotificationSummary.cs  (or a new Notification folder)
using Microsoft.EntityFrameworkCore;

namespace TEALINK.GARDEN.Model.DashBoard
{
    [Keyless]
    public class NotificationSummary
    {
        public int SectionCount { get; set; }
        public int PresentCount { get; set; }
        public int AbsentCount { get; set; }
        public int LeaveCount { get; set; }
        public decimal PluckingQty { get; set; }
        public decimal PruningQty { get; set; }
    }
}