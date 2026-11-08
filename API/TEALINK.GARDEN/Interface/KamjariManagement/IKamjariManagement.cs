using TEALINK.GARDEN.Model.Kamjari;
using TEALINK.GARDEN.Model.PluckingType;

namespace TEALINK.GARDEN.Interface.KamjariManagement
{
    public interface IKamjariManagement
    {
        IQueryable<KamjariMaster> GetData(int unitid, int enroll);
        IQueryable<KamjariMaster> GetDataByID(int id, int unitid, int enroll);
        Tuple<string, string> Create(KamjariMaster master);
        Tuple<string, string> Update(int id, KamjariMaster master);
        Tuple<string, string> Delete(int id, int unitid, int enroll);
    }
}
