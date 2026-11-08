using TEALINK.GARDEN.Model.Common;
using TEALINK.GARDEN.Model.Report;

namespace TEALINK.GARDEN.Interface.Common
{
    public interface ICommon
    {
        IQueryable<CommonModel> GetMasterTypes();
        IQueryable<CommonModel> GetUnitAreaPlucked();
        IQueryable<CommonModel> GetFaceRecognization();
        IQueryable<CommonModel> GetRoundOffRule();
        IQueryable<CommonModel> GetUnit(int unitid);
        IQueryable<CommonModel> GetDivision(int unitid);
        IQueryable<CommonModel> GetJobStation(int unitid);
        IQueryable<CommonModel> GetWorkerGrouplist(int unitid);
        IQueryable<CommonModel> GetWeeklyHoliday();
        IQueryable<CommonModel> GetWorkerlist();
        IQueryable<CommonModel> GetLeaveTypelist();
        IQueryable<CommonModel> GetLeaveTypeValuelist(string valueget,int unitid);
        IQueryable<CommonModel> GetPluckingInterval();
        IQueryable<CommonModel> GetPruneType();
        IQueryable<CommonModel> GetAttendance();
        IQueryable<CommonModel> GetIsActive();
        IQueryable<CommonModel> GetKamjariType(int unitid);
        IQueryable<CommonModel> GetKamjari(int unitid);
        IQueryable<CommonModel> GetKamjariforUnit(int unitid);
        IQueryable<CommonModel> GetShit(int unitid);
        IQueryable<CommonModel> GetAttendanceTypeForReport();

        IQueryable<CommonModel> GetGender(int unitid);
        IQueryable<CommonModel> GetWorkerType(int unitid);
        IQueryable<CommonModel> GetBook(int unitid);
        IQueryable<CommonModel> GetDate(int unitid);
        IQueryable<CommonModel> GetBatch(int unitid);

        IQueryable<CommonModel> GetOperator(int unitid);
        IQueryable<CommonModel> GetDevice(int unitid);
        IQueryable<CommonModel> GetWeighment(int unitid);
        IQueryable<NewReportModel> GetReportListMaster(int categoryid);
        IQueryable<CommonModel> GetCategoryListMaster();
        IQueryable<CommonModel> GetCategoryListTransaction();
        IQueryable<NewReportModel> GetReportListTransaction(int categoryid);
        IQueryable<CommonModel> GetReportAssignedFilters(int reportlistid);
        IQueryable<FilterDataModel> GetReportFilters();
        IQueryable<CommonModel> GetLeftFilterData(int filterid, int unitid);
        IQueryable<CommonModel> GetEmployee(int unitid);
        IQueryable<CommonModel> GetSection(int unitid);
        IQueryable<CommonModel> GetBookPermanent(int unitid);
        IQueryable<CommonModel> GetEmployeePermanent(int unitid);
        IQueryable<CommonModel> GetShiftForReport(int unitid);
        IQueryable<CommonModel> GetLeaveTypeForReport();

    }
}