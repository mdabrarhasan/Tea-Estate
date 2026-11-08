using TEALINK.GARDEN.Model.SalaryConfiguration;

namespace TEALINK.GARDEN.Interface.SalaryConfiguration
{
    public interface ISalaryConfiguration
    {
        IQueryable<SalaryConfigurationMasterView> GetData(int unitid, int enroll);
        IQueryable<SalaryConfigurationMaster> GetDataByID(int id, int unitid, int enroll);
        Tuple<string, string> Create(SalaryConfigurationMaster master);
        Tuple<string, string> Update(int id, SalaryConfigurationMaster master);
        Tuple<string, string> Delete(int id, int unitid, int enroll);
    }
}
