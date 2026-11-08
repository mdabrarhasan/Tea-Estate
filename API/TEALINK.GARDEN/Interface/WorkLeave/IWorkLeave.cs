using TEALINK.GARDEN.Model.WorkerBook;
using TEALINK.GARDEN.Model.WorkerLeave;

namespace TEALINK.GARDEN.Interface.WorkLeave
{
    public interface IWorkLeave
    {
        public Tuple<string, string> Create(WorkerLeaveMaster master);
        public IQueryable<WorkerLeaveMaster> GetData(int unitid, int enroll);
        public IQueryable<WorkerLeaveMaster> GetDataByID(int unitid);
        public Tuple<string, string> Update(int id, WorkerLeaveMaster master);
        Tuple<string, string> Delete(int id, int unitid, int enroll);
    }
}
