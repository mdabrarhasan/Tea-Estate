using TEALINK.GARDEN.Model.CompanyManagement;

namespace TEALINK.GARDEN.Interface.CompanyManagement
{
    public interface ICompanyManagement
    {
        IQueryable<CompanyManagementModel> GetCompanyInfo(int unitid);
    }
}
