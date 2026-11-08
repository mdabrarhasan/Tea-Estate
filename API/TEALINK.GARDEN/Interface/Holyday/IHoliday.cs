using TEALINK.GARDEN.Model.Holiday;
using TEALINK.GARDEN.Model.LeaveManagement;
using TEALINK.GARDEN.Model.WageCalender;

namespace TEALINK.GARDEN.Interface.Holyday
{
    public interface IHoliday
    {
        IQueryable<HolidayModel> GetData(int unitid, int enroll);
        public Tuple<string, string> Create(HolidayModel master);
        public Tuple<string, string> Delete(int id, int unitid, int enroll);
    }
}
