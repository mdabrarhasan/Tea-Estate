using TEALINK.GARDEN.Model.GardenManagement;
using TEALINK.GARDEN.Model.Section;

namespace TEALINK.GARDEN.Interface.SectionManagement
{
    public interface ISectionManagement
    {
        IQueryable<SectionManagementView> GetData(int unitid, int enroll);
        IQueryable<SectionManagementView> GetDataByID(int unitid);
        Tuple<string, string> Create(SectionManagementMaster master);
        Tuple<string, string> Update(int id, SectionManagementMaster master);
        Tuple<string, string> Delete(int id, int unitid, int enroll);
    }
}
