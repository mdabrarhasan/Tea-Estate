using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Reflection.Emit;
using TEALINK.GARDEN.Interface.Common;
using TEALINK.GARDEN.Interface.WorkerBook;
using TEALINK.GARDEN.Interface.WorkerManagement;
using TEALINK.GARDEN.Model.ExeclData;
using TEALINK.GARDEN.Model.WorkerBook;
using TEALINK.GARDEN.Model.WorkerManagement;
using TEALINK.GARDEN.Repository.KamjariRepository;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TEALINK.GARDEN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerManagementController : ControllerBase
    {
        private readonly IWorkerManagement _cm;
        private readonly ICommon _com;

        public WorkerManagementController(IWorkerManagement cm, ICommon com)
        {
            _cm = cm;
            _com = com;
        }

        [HttpGet]
        public JsonResult Get(int unitid, int enroll)
        {
            return new JsonResult(_cm.GetData(unitid, enroll));
        }

        [HttpGet]
        [Route("GetDataByID/{id}")]
        public JsonResult GetDataByID(int id)
        {
            var obj = _cm.GetDataByID(id);
            return new JsonResult(obj);
        }

        [HttpGet]
        [Route("GetAttendenceDetails/{unitid}/{daterangeid}/{bookid}")]
        public JsonResult GetAttendenceDetails(int unitid, int daterangeid, int bookid)
        {
            var obj = _cm.GetAttendenceDetails(unitid, daterangeid, bookid);
            return new JsonResult(obj);
        }

        [HttpGet]
        [Route("GetDailyAttendence/{unitid}/{date}/{bookid}")]
        public JsonResult GetDailyAttendence(int unitid, string date, int bookid)
        {
            var obj = _cm.GetDailyAttendence(unitid, date, bookid);
            return new JsonResult(obj);
        }

        [HttpDelete]
        [Route("GetDailyAttendeceDelete/{unitid}/{date}/{bookid}")]
        public JsonResult GetDailyAttendeceDelete(int unitid, string date, int bookid)
        {
            var obj = _cm.GetDailyAttendeceDelete(unitid, date, bookid);
            return new JsonResult(obj);
        }

        [HttpPost]
        public JsonResult Post(WorkerManagementMaster master)
        {
            return new JsonResult(_cm.Create(master));
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file, [FromForm] int unitid)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File is empty");

            var dataList = new List<WorkerExcelData>();

            try
            {
                using var stream = new MemoryStream();
                await file.CopyToAsync(stream);
                using var workbook = new XLWorkbook(stream);
                var worksheet = workbook.Worksheet(1);

                // Get the header row
                var headerRow = worksheet.Row(1);

                // Create a dictionary to map column names to column numbers
                var columnMap = new Dictionary<string, int>();

                foreach (var cell in headerRow.CellsUsed())
                {
                    var headerName = cell.GetValue<string>().Trim().ToUpper();
                    columnMap[headerName] = cell.Address.ColumnNumber;
                }

                // Skip the header row and process data rows
                var rows = worksheet.RangeUsed().RowsUsed().Skip(1);

                // Dictionary to track validation errors
                var validationErrors = new Dictionary<string, List<string>>();

                foreach (var row in rows)
                {
                    var code = columnMap.ContainsKey("CODE") ? row.Cell(columnMap["CODE"]).GetValue<string>().Trim() : "";
                    var name = columnMap.ContainsKey("NAME") ? row.Cell(columnMap["NAME"]).GetValue<string>().Trim() : "";
                    var division = columnMap.ContainsKey("DIVISION") ? row.Cell(columnMap["DIVISION"]).GetValue<string>().Trim() : "";
                    var gender = columnMap.ContainsKey("GENDER") ? row.Cell(columnMap["GENDER"]).GetValue<string>().Trim() : "";
                    var workertype = columnMap.ContainsKey("WORKERTYPE") ? row.Cell(columnMap["WORKERTYPE"]).GetValue<string>().Trim() : "";
                    var book = columnMap.ContainsKey("BOOK") ? row.Cell(columnMap["BOOK"]).GetValue<string>().Trim() : "";
                    var retiredidStr = columnMap.ContainsKey("RETIREDID") ? row.Cell(columnMap["RETIREDID"]).GetValue<string>().Trim() : "";
                    var isactiveStr = columnMap.ContainsKey("ISACTIVE") ? row.Cell(columnMap["ISACTIVE"]).GetValue<string>().Trim() : "";

                    // Validate mandatory fields
                    if (string.IsNullOrWhiteSpace(name))
                    {
                        if (!validationErrors.ContainsKey("NAME"))
                            validationErrors["NAME"] = new List<string>();
                        validationErrors["NAME"].Add(code);
                    }

                    if (string.IsNullOrWhiteSpace(division))
                    {
                        if (!validationErrors.ContainsKey("DIVISION"))
                            validationErrors["DIVISION"] = new List<string>();
                        validationErrors["DIVISION"].Add(code);
                    }

                    if (string.IsNullOrWhiteSpace(gender))
                    {
                        if (!validationErrors.ContainsKey("GENDER"))
                            validationErrors["GENDER"] = new List<string>();
                        validationErrors["GENDER"].Add(code);
                    }

                    if (string.IsNullOrWhiteSpace(workertype))
                    {
                        if (!validationErrors.ContainsKey("WORKERTYPE"))
                            validationErrors["WORKERTYPE"] = new List<string>();
                        validationErrors["WORKERTYPE"].Add(code);
                    }

                    if (string.IsNullOrWhiteSpace(book))
                    {
                        if (!validationErrors.ContainsKey("BOOK"))
                            validationErrors["BOOK"] = new List<string>();
                        validationErrors["BOOK"].Add(code);
                    }

                    if (string.IsNullOrWhiteSpace(retiredidStr))
                    {
                        if (!validationErrors.ContainsKey("RETIREDID"))
                            validationErrors["RETIREDID"] = new List<string>();
                        validationErrors["RETIREDID"].Add(code);
                    }

                    if (string.IsNullOrWhiteSpace(isactiveStr))
                    {
                        if (!validationErrors.ContainsKey("ISACTIVE"))
                            validationErrors["ISACTIVE"] = new List<string>();
                        validationErrors["ISACTIVE"].Add(code);
                    }

                    var data = new WorkerExcelData
                    {
                        NAME = name,
                        CODE = code,
                        PFNUMBER = columnMap.ContainsKey("PFNUMBER") ? row.Cell(columnMap["PFNUMBER"]).GetValue<string>().Trim() : "",
                        DIVISION = division,
                        GENDER = gender,
                        WORKERTYPE = workertype,
                        BOOK = book,
                        RETIREDID = ParseBoolean(retiredidStr),
                        DATEOFBIRTH = columnMap.ContainsKey("DATEOFBIRTH") ? row.Cell(columnMap["DATEOFBIRTH"]).GetValue<string>().Trim() : "",
                        ADDRESS = columnMap.ContainsKey("ADDRESS") ? row.Cell(columnMap["ADDRESS"]).GetValue<string>().Trim() : "",
                        CONTACTNUMBER = columnMap.ContainsKey("CONTACTNUMBER") ? row.Cell(columnMap["CONTACTNUMBER"]).GetValue<string>().Trim() : "",
                        ISACTIVE = ParseBoolean(isactiveStr),
                    };

                    dataList.Add(data);
                }

                // If there are validation errors, return them
                if (validationErrors.Any())
                {
                    var errorMessages = new List<string>();

                    foreach (var error in validationErrors)
                    {
                        var fieldName = error.Key;
                        var codes = string.Join(", ", error.Value.Where(c => !string.IsNullOrWhiteSpace(c)));

                        if (string.IsNullOrWhiteSpace(codes))
                        {
                            errorMessages.Add($"Submission failed because {fieldName} is not provided for some rows without CODE.");
                        }
                        else
                        {
                            errorMessages.Add($"Submission failed because {fieldName} is not provided for the code {codes}.");
                        }
                    }

                    return new JsonResult(new
                    {
                        item1 = "error",
                        item2 = string.Join(" | ", errorMessages)
                    });
                }

                var result = _cm.Create(dataList, unitid);

                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                return new JsonResult(new { item1 = "error", item2 = ex.Message });
            }
        }

        // Helper method to parse boolean values
        private bool ParseBoolean(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;

            value = value.Trim().ToLower();

            // Handle common boolean representations
            if (value == "1" || value == "true" || value == "yes" || value == "y")
                return true;

            if (value == "0" || value == "false" || value == "no" || value == "n")
                return false;

            // Try to parse as boolean
            if (bool.TryParse(value, out bool result))
                return result;

            return false; // Default to false if unable to parse
        }

        [HttpPut]
        public JsonResult Put(int id, WorkerManagementMaster master)
        {
            var li = _cm.Update(id, master);
            return new JsonResult(li);
        }

        [HttpDelete]
        public JsonResult Delete(int id, int unitid, int enroll)
        {
            return new JsonResult(_cm.Delete(id, unitid, enroll));
        }
    }
}
