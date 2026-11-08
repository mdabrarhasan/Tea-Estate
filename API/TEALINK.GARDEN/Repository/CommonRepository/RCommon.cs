using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TEALINK.GARDEN.Interface.Common;
using TEALINK.GARDEN.Model.Common;
using TEALINK.GARDEN.Model.CompanyManagement;
using TEALINK.GARDEN.Model.Report;

namespace TEALINK.GARDEN.Repository.CommonRepository
{
    public class RCommon : ICommon
    {
        string strMsg = "";
        string strStatusMsg = "";
        private Context _context;
        public RCommon(Context context)
        {
            _context = context;
        }

        public IQueryable<CommonModel> GetUnitAreaPlucked()
        {
            return _context.CommonModel.FromSqlRaw("Exec SPGETUNITAREAPLUCKED");
        }

        public IQueryable<CommonModel> GetFaceRecognization()
        {
            return _context.CommonModel.FromSqlRaw("Exec SPGETATTENDANCEFACERECOGNIZATION");
        }

        public IQueryable<CommonModel> GetRoundOffRule()
        {
            return _context.CommonModel.FromSqlRaw("Exec SPGETTBLROUNDOFFRULE");
        }

        public IQueryable<CommonModel> GetUnit(int unitid)
        {
            SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
            return _context.CommonModel.FromSqlRaw("Exec SPGETUNIT @UNITID", UNITID);
        }

        public IQueryable<CommonModel> GetWeeklyHoliday()
        {
            return _context.CommonModel.FromSqlRaw("Exec SPGETWEEKLYHOLIDAY");
        }

        public IQueryable<CommonModel> GetPluckingInterval()
        {
            return _context.CommonModel.FromSqlRaw("Exec SPGETPLKBUCKETINTERVAL");
        }

        public IQueryable<CommonModel> GetAttendanceTypeForReport()
        {
            return _context.CommonModel.FromSqlRaw("Exec SPGETATTENDANCETYPEFORREPORT");
        }

        public IQueryable<CommonModel> GetPruneType()
        {
            return _context.CommonModel.FromSqlRaw("Exec SPGETPRUNETYPE");
        }
          public IQueryable<CommonModel> GetAttendance()
        {
            return _context.CommonModel.FromSqlRaw("Exec SPGETATTENDENCETYPE");
        }

        public IQueryable<CommonModel> GetIsActive()
        {
            return _context.CommonModel.FromSqlRaw("Exec SPGETISACTIVE");
        }

        public IQueryable<CommonModel> GetDivision(int unitid)
        {
            SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
            return _context.CommonModel.FromSqlRaw("Exec SPGETDIVISION @UNITID", UNITID);
        }
        public IQueryable<CommonModel> GetWorkerGrouplist(int unitid)
        {
            SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
            return _context.CommonModel.FromSqlRaw("Exec SPGetWorkerGrouplist @UNITID", UNITID);
        }

        public IQueryable<CommonModel> GetJobStation(int unitid)
        {
            SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
            return _context.CommonModel.FromSqlRaw("Exec SPGetJobStation @UNITID", UNITID);
        }

        
        public IQueryable<CommonModel> GetKamjariType(int unitid)
        {
            SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
            return _context.CommonModel.FromSqlRaw("Exec SPGETKAMJARITYPE @UNITID", UNITID);
        }


        public IQueryable<CommonModel> GetKamjari(int unitid)
        {
            SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
            return _context.CommonModel.FromSqlRaw("Exec SPGETKAMJARI @UNITID", UNITID);
        }


        public IQueryable<CommonModel> GetKamjariforUnit(int unitid)
        {
            SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
            return _context.CommonModel.FromSqlRaw("Exec SPGETKAMJARIUNITWISE @UNITID",UNITID);
        }

        public IQueryable<CommonModel> GetShit(int unitid)
        {
            SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
            return _context.CommonModel.FromSqlRaw("Exec SPGETSHIT @UNITID", UNITID);
        }

        public IQueryable<CommonModel> GetGender(int unitid)
        {
            SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
            return _context.CommonModel.FromSqlRaw("Exec SPGETGENDER @UNITID", UNITID);
        }

        public IQueryable<CommonModel> GetWorkerType(int unitid)
        {
            SqlParameter TYPEID = new SqlParameter("@TYPEID", 1);
            SqlParameter ID = new SqlParameter("@ID", 1);
            SqlParameter ENROLL = new SqlParameter("@ENROLL", 1);
            SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
            return _context.CommonModel.FromSqlRaw("Exec SPGETWORKERTYPE @TYPEID,@ID,@UNITID,@ENROLL", TYPEID, ID, UNITID, ENROLL);
        }

        public IQueryable<CommonModel> GetBook(int unitid)
        {
            SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
            return _context.CommonModel.FromSqlRaw("Exec SPGETBOOK @UNITID", UNITID);
        }
        public IQueryable<CommonModel> GetDate(int unitid)
        {
            SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
            return _context.CommonModel.FromSqlRaw("Exec SPGETDATE @UNITID", UNITID);
        }

        public IQueryable<CommonModel> GetEmployee(int unitid)
        {
            SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
            return _context.CommonModel.FromSqlRaw("Exec SPGETEMPLOYEE @UNITID", UNITID);
        }

        public IQueryable<CommonModel> GetSection(int unitid)
        {
            SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
            return _context.CommonModel.FromSqlRaw("Exec SPGETTHESECTION @UNITID", UNITID);
        }

        public IQueryable<CommonModel> GetBatch(int unitid)
        {
            SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
            return _context.CommonModel.FromSqlRaw("Exec SPGETBATCH @UNITID", UNITID);   //Batch and Book same.

        }

        public IQueryable<CommonModel> GetMasterTypes()
        {
            return _context.CommonModel.FromSqlRaw("Exec SPGETMASTERTYPE");
        }


        public IQueryable<CommonModel> GetOperator(int unitid)
        {
            SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
            return _context.CommonModel.FromSqlRaw("Exec SPGETOPERATOR @UNITID", UNITID);
        }

        public IQueryable<CommonModel> GetDevice(int unitid)
        {
            SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
            return _context.CommonModel.FromSqlRaw("Exec SPGETDEVICE @UNITID", UNITID);
        }

        public IQueryable<CommonModel> GetWeighment(int unitid)
        {
            SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
            return _context.CommonModel.FromSqlRaw("Exec SPGETWEIGHTMENT @UNITID", UNITID);
        }

        public IQueryable<CommonModel> GetWorkerlist()
        {
            return _context.CommonModel.FromSqlRaw("Exec SPGetWorkerlist");
        } 

        public IQueryable<CommonModel> GetLeaveTypelist()
        {
            return _context.CommonModel.FromSqlRaw("Exec SPGetLeaveTypelist");
        }

        public IQueryable<CommonModel> GetLeaveTypeValuelist(string valueget,int unitid)
        {
            SqlParameter NAME = new SqlParameter("@NAME", valueget);
            SqlParameter UNITID = new SqlParameter("@UNITID", unitid);

            return _context.CommonModel.FromSqlRaw("Exec SPGetLeaveTypeValuelist @NAME,@UNITID", NAME, UNITID);
        }

        public IQueryable<CommonModel> GetCategoryListMaster()
        {
            return _context.CommonModel.FromSqlRaw("Exec SPGETCATEGORYLISTMASTER"); ;
        }

        public IQueryable<FilterDataModel> GetReportFilters()
        {
            return _context.FilterDataModel.FromSqlRaw("Exec SPGETREPORTFILTERS"); 
        }

        public IQueryable<NewReportModel> GetReportListMaster(int categoryid)
        {
            SqlParameter REPORTCATEGORYID = new SqlParameter("@REPORTCATEGORYID", categoryid);

            return _context.NewReportModel.FromSqlRaw("Exec SPGETREPORTLISTMASTER @REPORTCATEGORYID", REPORTCATEGORYID); ;
        }

        public IQueryable<CommonModel> GetCategoryListTransaction()
        {
            return _context.CommonModel.FromSqlRaw("Exec SPGETCATEGORYLISTTRANSACTION"); ;
        }

        public IQueryable<NewReportModel> GetReportListTransaction(int categoryid)
        {
            SqlParameter REPORTCATEGORYID = new SqlParameter("@REPORTCATEGORYID", categoryid);

            return _context.NewReportModel.FromSqlRaw("Exec SPGETREPORTLISTTRANSACTION @REPORTCATEGORYID", REPORTCATEGORYID); ;
        }

        public IQueryable<CommonModel> GetReportAssignedFilters(int reportlistid)
        {
            SqlParameter REPORTLISTID = new SqlParameter("@REPORTLISTID", reportlistid);

            return _context.CommonModel.FromSqlRaw("Exec SPGETREPORTASSIGNEDFILTERS @REPORTLISTID", REPORTLISTID); 
        }

        public IQueryable<CommonModel> GetLeftFilterData(int filterid, int unitid)
        {
            SqlParameter FILTERID = new SqlParameter("@FILTERID", filterid);
            SqlParameter UNITID = new SqlParameter("@UNITID", unitid);

            return _context.CommonModel.FromSqlRaw(
                "EXEC SPGETLEFTFILTERDATA @FILTERID, @UNITID",
                FILTERID,
                UNITID
            );
        }

        public IQueryable<CommonModel> GetBookPermanent(int unitid)
        {
            SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
            return _context.CommonModel.FromSqlRaw("Exec SPGETBOOKPERMANENT @UNITID", UNITID);
        }

        public IQueryable<CommonModel> GetEmployeePermanent(int unitid)
        {
            SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
            return _context.CommonModel.FromSqlRaw("Exec SPGETEMPLOYEEPERMANENT @UNITID", UNITID);
        }

        public IQueryable<CommonModel> GetShiftForReport(int unitid)
        {
            SqlParameter UNITID = new SqlParameter("@UNITID", unitid);
            return _context.CommonModel.FromSqlRaw("Exec SPGETSHIFTFORREPORT @UNITID", UNITID);
        }

        public IQueryable<CommonModel> GetLeaveTypeForReport()
        {
            return _context.CommonModel.FromSqlRaw("Exec SPGETLEAVETYPEFORREPORT");
        }
    }
}