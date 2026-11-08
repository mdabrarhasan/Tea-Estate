using TEALINK.GARDEN.Model.PluckingType;
using TEALINK.GARDEN.Model.PruneingType;

namespace TEALINK.GARDEN.Interface.PruneType
{
    public interface IPruneingType
    {
        IQueryable<PruningTypeView> GetData(int unitid, int enroll);
        IQueryable<PruningTypeView> GetDataByID(int unitid);
        Tuple<string, string> Create(PruningTypeMaster master);
        Tuple<string, string> Update(int id, PruningTypeMaster master);
        Tuple<string, string> Delete(int id, int unitid, int enroll);
    }
}
