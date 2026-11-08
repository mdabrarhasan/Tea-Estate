using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TEALINK.GARDEN.Interface.Common;
using TEALINK.GARDEN.Interface.CompanyManagement;

namespace TEALINK.GARDEN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommonController : ControllerBase
    {

        private readonly ICommon _com;

        public CommonController(ICommon com)
        {
            _com = com;
        }

        [HttpGet]
        [Route("GetUnitAreaPlucked")]
        public JsonResult GetUnitAreaPlucked()
        {
            return new JsonResult(_com.GetUnitAreaPlucked());
        }

        [HttpGet]
        [Route("GetFaceRecognization")]
        public JsonResult GetFaceRecognization()
        {
            return new JsonResult(_com.GetFaceRecognization());
        }

        [HttpGet]
        [Route("GetRoundOffRule")]
        public JsonResult GetRoundOffRule()
        {
            return new JsonResult(_com.GetRoundOffRule());
        }

        [HttpGet]
        [Route("GetUnit/{unitid}")]
        public JsonResult GetUnit(int unitid)
        {
            return new JsonResult(_com.GetUnit(unitid));
        }

        [HttpGet]
        [Route("GetWeeklyHoliday")]
        public JsonResult GetWeeklyHoliday()
        {
            return new JsonResult(_com.GetWeeklyHoliday());
        }

        [HttpGet]
        [Route("GetPluckingInterval")]
        public JsonResult GetPluckingInterval()
        {
            return new JsonResult(_com.GetPluckingInterval());
        }

        [HttpGet]
        [Route("GetPruneType")]
        public JsonResult GetPruneType()
        {
            return new JsonResult(_com.GetPruneType());
        }

        [HttpGet]
        [Route("GetAttendance")]
        public JsonResult GetAttendance()
        {
            return new JsonResult(_com.GetAttendance());
        }

        [HttpGet]
        [Route("GetIsActive")]
        public JsonResult GetIsActive()
        {
            return new JsonResult(_com.GetIsActive());
        }

        [HttpGet]
        [Route("GetDivision/{unitid}")]
        public JsonResult GetDivision(int unitid)
        {
            return new JsonResult(_com.GetDivision(unitid));
        }
        [HttpGet]
        [Route("GetWorkerGrouplist/{unitid}")]
        public JsonResult GetWorkerGrouplist(int unitid)
        {
            return new JsonResult(_com.GetWorkerGrouplist(unitid));
        }

        [HttpGet]
        [Route("GetJobStation/{unitid}")]
        public JsonResult GetJobStation(int unitid)
        {
            return new JsonResult(_com.GetJobStation(unitid));
        }

        [HttpGet]
        [Route("GetAttendanceTypeForReport")]
        public JsonResult GetAttendanceTypeForReport()
        {
            return new JsonResult(_com.GetAttendanceTypeForReport());
        }


        [HttpGet]
        [Route("GetKamjariType/{unitid}")]
        public JsonResult GetKamjariType(int unitid)
        {
            return new JsonResult(_com.GetKamjariType(unitid));
        }

        [HttpGet]
        [Route("GetKamjari/{unitid}")]
        public JsonResult GetKamjari(int unitid)
        {
            return new JsonResult(_com.GetKamjari(unitid));
        }


        [HttpGet]
        [Route("GetKamjariforUnit/{unitid}")]
        public JsonResult GetKamjariforUnit(int unitid)
        {
            return new JsonResult(_com.GetKamjariforUnit(unitid));
        }


        [HttpGet]
        [Route("GetShift/{unitid}")]
        public JsonResult GetShift(int unitid)
        {
            return new JsonResult(_com.GetShit(unitid));
        }

        [HttpGet]
        [Route("GetMasterTypes")]
        public JsonResult GetMasterTypes()
        {
            return new JsonResult(_com.GetMasterTypes());
        }

        [HttpGet]
        [Route("GetGender")]
        public JsonResult GetGender()
        {
            return new JsonResult(_com.GetGender(0));
        }

        [HttpGet]
        [Route("GetBook/{unitid}")]
        public JsonResult GetBook(int unitid)
        {
            return new JsonResult(_com.GetBook(unitid));
        }
        [HttpGet]
        [Route("GetDate/{unitid}")]
        public JsonResult GetDate(int unitid)
        {
            return new JsonResult(_com.GetDate(unitid));
        }

        [HttpGet]
        [Route("GetEmployee/{unitid}")]
        public JsonResult GetEmployee(int unitid)
        {
            return new JsonResult(_com.GetEmployee(unitid));
        }

        [HttpGet]
        [Route("GetSection/{unitid}")]
        public JsonResult GetSection(int unitid)
        {
            return new JsonResult(_com.GetSection(unitid));
        }

        [HttpGet]
        [Route("GetWorkerType")]
        public JsonResult GetWorkerType()
        {
            return new JsonResult(_com.GetWorkerType(0));
        }

        [HttpGet]
        [Route("GetBatch/{unitid}")]
        public JsonResult GetBatch(int unitid)
        {
            return new JsonResult(_com.GetBatch(unitid));
        }

        [HttpGet]
        [Route("GetWeighment/{unitid}")]
        public JsonResult GetWeighment(int unitid)
        {
            return new JsonResult(_com.GetWeighment(unitid));
        }

        [HttpGet]
        [Route("GetDevice/{unitid}")]
        public JsonResult GetDevice(int unitid)
        {
            return new JsonResult(_com.GetDevice(unitid));
        }

        [HttpGet]
        [Route("GetOperator/{unitid}")]
        public JsonResult GetOperator(int unitid)
        {
            return new JsonResult(_com.GetOperator(unitid));
        }

        [HttpGet]
        [Route("GetWorkerlist")]
        public JsonResult GetWorkerlist()
        {
            return new JsonResult(_com.GetWorkerlist());
        }

        [HttpGet]
        [Route("GetLeaveTypelist")]
        public JsonResult GetLeaveTypelist()
        {
            return new JsonResult(_com.GetLeaveTypelist());
        }

        [HttpGet("GetLeaveTypeValuelist/{valueget}/{unitid}")]
        
        public JsonResult GetLeaveTypeValuelist(string valueget, int unitid)
        {
            return new JsonResult(_com.GetLeaveTypeValuelist(valueget, unitid));
        }

        [HttpGet("GetReportListMaster/{categoryid}")]
        public JsonResult GetReportListMaster(int categoryid)
        {
            return new JsonResult(_com.GetReportListMaster(categoryid));
        }

        [HttpGet("GetCategoryListTransaction")]
        public JsonResult GetCategoryListTransaction()
        {
            return new JsonResult(_com.GetCategoryListTransaction());
        }

        [HttpGet("GetReportListTransaction/{categoryid}")]
        public JsonResult GetReportListTransaction(int categoryid)
        {
            return new JsonResult(_com.GetReportListTransaction(categoryid));
        }

        [HttpGet("GetCategoryListMaster")]
        public JsonResult GetCategoryListMaster()
        {
            return new JsonResult(_com.GetCategoryListMaster());
        }

        [HttpGet("GetReportFilters")]
        public JsonResult GetReportFilters()
        {
            return new JsonResult(_com.GetReportFilters());
        }

        [HttpGet("GetReportAssignedFilters/{reportlistid}")]
        public JsonResult GetReportAssignedFilters(int reportlistid)
        {
            return new JsonResult(_com.GetReportAssignedFilters(reportlistid));
        }

        [HttpGet("GetLeftFilterData/{filterid}/{unitid}")]
        public JsonResult GetLeftFilterData(int filterid, int unitid)
        {
            return new JsonResult(_com.GetLeftFilterData(filterid, unitid));
        }

        [HttpGet]
        [Route("GetBookPermanent/{unitid}")]
        public JsonResult GetBookPermanent(int unitid)
        {
            return new JsonResult(_com.GetBookPermanent(unitid));
        }

        [HttpGet]
        [Route("GetEmployeePermanent/{unitid}")]
        public JsonResult GetEmployeePermanent(int unitid)
        {
            return new JsonResult(_com.GetEmployeePermanent(unitid));
        }

        [HttpGet]
        [Route("GetShiftForReport/{unitid}")]
        public JsonResult GetShiftForReport(int unitid)
        {
            return new JsonResult(_com.GetShiftForReport(unitid));
        }

        [HttpGet]
        [Route("GetLeaveTypeForReport")]
        public JsonResult GetLeaveTypeForReport()
        {
            return new JsonResult(_com.GetLeaveTypeForReport());
        }
    }
}
