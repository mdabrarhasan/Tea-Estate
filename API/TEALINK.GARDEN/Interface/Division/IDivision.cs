using TEALINK.GARDEN.Model.CompanyManagement;
using TEALINK.GARDEN.Model.Division;
using TEALINK.GARDEN.Model.GardenManagement;

namespace TEALINK.GARDEN.Interface.Division
{
    public interface IDivision
    {
        IQueryable<DivisionMaster> GetDivisionInfo(int unitid);
        IQueryable<DivisionMaster> GetDataByID(int unitid);
        Tuple<string, string> Create(DivisionMaster master);
        Tuple<string, string> Update(int id,DivisionMaster master);
        Tuple<string, string> Delete(int id);

    }
}
