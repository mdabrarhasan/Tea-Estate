using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TEALINK.GARDEN.Interface.Report;
using TEALINK.GARDEN.Model.Report;

namespace TEALINK.GARDEN.Repository.ReportRepository
{
    public class RReportLanding : IReportService
    {
        private Context _context;

        public RReportLanding(Context context)
        {
            _context = context;
        }


        
        public ReportLandingView GetReportCredentials(int id=0)
        {
            SqlParameter ID = new SqlParameter("@ID", id);
            return _context.ReportLandingViews.FromSqlRaw("Exec SprGetTeaReport @ID", ID).AsEnumerable().FirstOrDefault();
        }

       
        public ReportLandingView GetWageSheet(int id=0, int bookid=0, int modeid = 0, int reportid = 0,int enroll = 0,int unitid = 0)
        {
            SqlParameter ID = new SqlParameter("@ID", id);
            SqlParameter BOOKID = new SqlParameter("@BOOKID", bookid);
            SqlParameter MODEID = new SqlParameter("@MODEID", modeid);
            SqlParameter REPORTID = new SqlParameter("@REPORTID", "RP0002");
            SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
            SqlParameter ENROLL = new SqlParameter("@ENROLL", enroll);
            return _context.ReportLandingViews.FromSqlRaw("Exec SPGETWAGESHEETPLK @ID,@BOOKID,@MODEID,@REPORTID,@UNITID,@ENROLL", ID,BOOKID,MODEID,REPORTID,UNITID,ENROLL).AsEnumerable().FirstOrDefault();
        }
    }
}

