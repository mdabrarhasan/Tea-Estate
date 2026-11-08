using Microsoft.EntityFrameworkCore;
using TEALINK.GARDEN.Model.Auth;
using TEALINK.GARDEN.Model.Common;
using TEALINK.GARDEN.Model.CompanyManagement;
using TEALINK.GARDEN.Model.DailyNonPlucking;
using TEALINK.GARDEN.Model.DailyPlucking;
using TEALINK.GARDEN.Model.DashBoard;
using TEALINK.GARDEN.Model.Device;
using TEALINK.GARDEN.Model.Division;
using TEALINK.GARDEN.Model.GardenManagement;
using TEALINK.GARDEN.Model.Holiday;
using TEALINK.GARDEN.Model.Kamjari;
using TEALINK.GARDEN.Model.LeaveManagement;
using TEALINK.GARDEN.Model.Menu;
using TEALINK.GARDEN.Model.Permission;
using TEALINK.GARDEN.Model.PluckingAllocation;
using TEALINK.GARDEN.Model.PluckingType;
using TEALINK.GARDEN.Model.PruneingType;
using TEALINK.GARDEN.Model.Report;
using TEALINK.GARDEN.Model.SalaryConfiguration;
using TEALINK.GARDEN.Model.SalaryGenerate;
using TEALINK.GARDEN.Model.Section;
using TEALINK.GARDEN.Model.ShiftManagement;
using TEALINK.GARDEN.Model.UserManagement;
using TEALINK.GARDEN.Model.WageCalender;
using TEALINK.GARDEN.Model.WorkerBatch;
using TEALINK.GARDEN.Model.WorkerBook;
using TEALINK.GARDEN.Model.WorkerLeave;
using TEALINK.GARDEN.Model.WorkerManagement;
using TEALINK.GARDEN.Model.WorkerType;
using static TEALINK.GARDEN.Model.DailyPlucking.DailyPluckingModel;

namespace TEALINK.GARDEN
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        public DbSet<Login> Login { get; set; }
        public DbSet<LoginModel> LoginModel { get; set; }

        public DbSet<Menus> Menu { get; set; }
        public DbSet<SubMenu> SubMenu { get; set; }
        public DbSet<MenuList> MenuList { get; set; }
        public DbSet<CommonModel> CommonModel { get; set; }

        public DbSet<CompanyManagementModel> CompanyManagementModel { get; set; }
        public DbSet<DivisionMaster> DivisionMaster { get; set; }

        public DbSet<GardenManagementView> GardenManagementView { get; set; }
        public DbSet<GardenManagementMaster> GardenManagementMaster { get; set; }

        public DbSet<SectionManagementView> SectionManagementView { get; set; }
        public DbSet<SectionManagementMaster> SectionManagementMaster { get; set; }

        public DbSet<PluckingTypeMaster> PluckingTypeMaster { get; set; }
        public DbSet<KamjariMaster> KamjariMaster { get; set; }

        public DbSet<PruningTypeMaster> PruningTypeMaster { get; set; }
        public DbSet<PruningTypeView> PruningTypeView { get; set; }

        public DbSet<WorkerBookMaster> WorkerBookMaster { get; set; }
        public DbSet<WorkerBookView> WorkerBookView { get; set; }

        public DbSet<WorkerBatchMaster> WorkerBatchMaster { get; set; }
        public DbSet<WorkerBatchMasterView> WorkerBatchMasterView { get; set; }

        public DbSet<WorkerManagementView> WorkerManagementView { get; set; }
        public DbSet<DailyAttendenceReport> DailyAttendenceReport { get; set; }
        public DbSet<AttendenceDetails> AttendenceDetails { get; set; }
        public DbSet<WorkerManagementMaster> WorkerManagementMaster { get; set; }

        public DbSet<DailyPluckingView> DailyPluckingView { get; set; }
        public DbSet<DailyPluckingLineView> DailyPluckingLineView { get; set; }
        public DbSet<DailyPluckingModel> DailyPlucking { get; set; }
        //MAHAMUD
        public DbSet<PluckingLanding> PluckingLanding { get; set; }
        public DbSet<DashBoardView> DashBoardView { get; set; }

        public DbSet<DailyPluckingByWeighment> DailyPluckingByWeighment { get; set; }

        public DbSet<DailyPluckingAttendentEmp> DailyPluckingAttendentEmp { get; set; }


        public DbSet<ReportLandingView>ReportLandingViews { get; set; }
        public DbSet<NewReportModel> NewReportModel { get; set; }
        public DbSet<FilterDataModel> FilterDataModel { get; set; }


        public DbSet<DailyNonPluckingView> DailyNonPluckingView { get; set; }
        public DbSet<DailyNonPluckingLineView> DailyNonPluckingLineView { get; set; }
        public DbSet<GetEditKamjari> GetEditKamjari { get; set; }
        public DbSet<DailyNonPluckingAttendentEmp> DailyNonPluckingAttendentEmp { get; set; }
        public DbSet<DailyNonPluckingModel> DailyNonPluckingModel { get; set; }
        public DbSet<WorkerTypeModel> WorkerTypeModel { get; set; }


        public DbSet<ShiftMasterView> ShiftMasterView { get; set; }
        public DbSet<ShiftMaster> ShiftMaster { get; set; }
        public DbSet<ShiftLineView> ShiftLineView { get; set; }
        public DbSet<WageCalenderModel> WageCalenderModel { get; set; }
        public DbSet<SalaryGenerateModel> SalaryGenerateModel { get; set; }

        public DbSet<WorkerLeaveMaster> WorkerLeaveMaster { get; set; }
        public DbSet<LeaveGetdata> LeaveGetdata { get; set; }
        public DbSet<LeaveDetails> LeaveDetails { get; set; }


        public DbSet<HolidayModel> HolidayModel { get; set; }

        public DbSet<DeviceModel> Devices { get; set; }

        public DbSet<OperatorPermissionRow> OperatorPermissions { get; set; }
        public DbSet<OperatorPermissionListRow> OperatorPermissionList { get; set; }
        public DbSet<PermissionCheckResult> PermissionChecks { get; set; }
        public DbSet<SalaryConfigurationMaster> SalaryConfigurationMaster { get; set; }
        public DbSet<SalaryConfigurationMasterView> SalaryConfigurationMasterView { get; set; }
        public DbSet<UserManagementMaster> UserManagementMaster { get; set; }
        public DbSet<UserTypeList> UserTypeList { get; set; }
        public DbSet<NotificationSummary> NotificationSummary { get; set; }
        public DbSet<DashBoardStatsView> DashBoardStatsView { get; set; }
        public DbSet<SectionPluckingView> SectionPluckingView { get; set; }
        public DbSet<PluckingTrendView> PluckingTrendView { get; set; }
    }
}