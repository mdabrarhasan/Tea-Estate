using TEALINK.GARDEN.Model.DailyNonPlucking;
using TEALINK.GARDEN.Model.DailyPlucking;

namespace TEALINK.GARDEN.Interface.DailyNonPlucking
{
    public interface IDailyNonPlucking
    {
        IQueryable<DailyNonPluckingView> GetData(string pluckingdate, int? kamjaritypeid, int? bookid, int? operatorid, int? deviceid, int? batchid, int? recordid, int? divisionid, int? unitid, int? enroll);
        IQueryable<DailyNonPluckingLineView> GetDataByID(int id, string pluckeddate, int unitid, int enroll);
        Task<Tuple<string, string>> Create(List<DailyNonPluckingView> master);
        Tuple<string, string> KamjariUpdate(int id, int kamjariid,int teamid);
        Tuple<string, string> Delete(int id, int unitid, int enroll);
        IQueryable<DailyNonPluckingAttendentEmp> GetDataCountDateWise(string pluckeddate, int unitid);
    }
}
