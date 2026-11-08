
using TEALINK.GARDEN.Model.Division;
using TEALINK.GARDEN.Model.GardenManagement;

namespace TEALINK.GARDEN.Interface.GardenManagement
{
    public interface IGardenManagement
    {
        IQueryable<GardenManagementView> GetData(int unitid);
        IQueryable<GardenManagementView> GetDataByID(int unitid);
        Tuple<string, string> Create(GardenManagementMaster master);
        Tuple<string, string> Update(int id, GardenManagementMaster master);
        Tuple<string, string> Delete(int id,int unitid);

    }
}
