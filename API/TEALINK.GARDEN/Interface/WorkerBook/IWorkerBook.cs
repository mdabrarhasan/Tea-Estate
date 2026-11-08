

using TEALINK.GARDEN.Model.WorkerBook;

namespace TEALINK.GARDEN.Interface.WorkerBook
{
    public interface IWorkerBook
    {
        IQueryable<WorkerBookView> GetData(int unitid, int enroll);
        IQueryable<WorkerBookView> GetDataByID(int unitid);
        Tuple<string, string> Create(WorkerBookMaster master);
        Tuple<string, string> Update(int id, WorkerBookMaster master);
        Tuple<string, string> Delete(int id, int unitid, int enroll);
    }
}
