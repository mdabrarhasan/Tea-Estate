
using TEALINK.GARDEN.Model.ExeclData;
using TEALINK.GARDEN.Model.WorkerBatch;
using TEALINK.GARDEN.Model.WorkerManagement;

namespace TEALINK.GARDEN.Interface.WorkerManagement
{
    public interface IWorkerManagement
    {
        IQueryable<WorkerManagementView> GetData(int unitid, int enroll);
        IQueryable<WorkerManagementView> GetDataByID(int unitid);
        IQueryable<DailyAttendenceReport> GetDailyAttendence(int unitid,string date,int bookid);
        IQueryable<AttendenceDetails> GetAttendenceDetails(int unitid, int daterangeid, int bookid);
        Tuple<string, string> Create(WorkerManagementMaster master);
        Tuple<string, string> Update(int id, WorkerManagementMaster master);
        Tuple<string, string> Delete(int id, int unitid, int enroll);
        Tuple<string, string> GetDailyAttendeceDelete(int unitid, string date, int bookid);
        Tuple<string, string> Create(List<WorkerExcelData> master, int unitid);
    }
}
