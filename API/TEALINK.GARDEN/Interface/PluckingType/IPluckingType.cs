using TEALINK.GARDEN.Model;
using TEALINK.GARDEN.Model.PluckingType;
using TEALINK.GARDEN.Model.Section;

namespace TEALINK.GARDEN.Interface.PluckingType
{
    public interface IPluckingType
    {
        IQueryable<PluckingTypeMaster> GetData(int unitid, int enroll);
        IQueryable<PluckingTypeMaster> GetDataByID(int unitid);
        Tuple<string, string> Create(PluckingTypeMaster master);
        Tuple<string, string> Update(int id, PluckingTypeMaster master);
        Tuple<string, string> Delete(int id, int unitid, int enroll);
    }
}
