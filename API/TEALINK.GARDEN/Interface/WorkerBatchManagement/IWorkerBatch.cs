using TEALINK.GARDEN.Model.WorkerBatch;

namespace TEALINK.GARDEN.Interface.WorkerBatchManagement
{
    public interface IWorkerBatch
    {
        IQueryable<WorkerBatchMasterView> GetData(int unitid, int enroll);
        IQueryable<WorkerBatchMasterView> GetDataByID(int unitid);
        Tuple<string, string> Create(WorkerBatchMaster master);
        Tuple<string, string> Update(int id, WorkerBatchMaster master);
        Tuple<string, string> Delete(int id, int unitid, int enroll);
    }
}
