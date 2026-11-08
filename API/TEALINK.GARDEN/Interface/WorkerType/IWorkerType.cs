

using TEALINK.GARDEN.Model.WorkerType;

namespace TEALINK.GARDEN.Interface.WorkerType
{
    public interface IWorkerType
    {
        IQueryable<WorkerTypeModel> GetData(int unitid, int enroll);
        IQueryable<WorkerTypeModel> GetDataByID(int id,int unitid,int enroll);
        Tuple<string, string> Create(WorkerTypeModel master);
        Tuple<string, string> Update(int id, WorkerTypeModel master);
        Tuple<string, string> Delete(int id, int unitid, int enroll);
    }
}
