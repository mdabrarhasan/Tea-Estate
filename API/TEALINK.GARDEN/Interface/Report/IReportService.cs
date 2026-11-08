using TEALINK.GARDEN.Model.Report;

namespace TEALINK.GARDEN.Interface.Report
{
    public interface IReportService
    {
        public ReportLandingView GetReportCredentials(int id);
    }
}
