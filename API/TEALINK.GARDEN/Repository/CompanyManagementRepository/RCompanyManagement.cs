using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TEALINK.GARDEN.Interface.CompanyManagement;
using TEALINK.GARDEN.Model.CompanyManagement;

namespace TEALINK.GARDEN.Repository.CompanyManagementRepository
{
    public class RCompanyManagement : ICompanyManagement
    {
        private readonly Context _context;
 
        public RCompanyManagement(Context context)
        {
            _context = context;
        }

        public IQueryable<CompanyManagementModel> GetCompanyInfo(int unitid)
        {
            SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
            return _context.CompanyManagementModel.FromSqlRaw("Exec SPGETCOMPANYINFO @UNITID", UNITID);
        }

    }
}


