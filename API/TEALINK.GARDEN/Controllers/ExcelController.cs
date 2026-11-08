using System.Globalization;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TEALINK.GARDEN.Filters;
using TEALINK.GARDEN.Interface.Excel;
using TEALINK.GARDEN.Model.ExeclData;

namespace TEALINK.GARDEN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExcelController : ControllerBase
    {
        private readonly IExcel excel;
        public ExcelController(IExcel excel)
        {
            this.excel = excel;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file, [FromForm] string date, [FromForm] string typeid, [FromForm] string unitid)
        {
            if (file == null || file.Length == 0)
                return UploadError("Please choose a non-empty Excel file.");

            var dataList = new List<ExcelData>();
            var errors = new List<string>();

            try
            {
                using var stream = new MemoryStream();
                await file.CopyToAsync(stream);
                using var workbook = new XLWorkbook(stream);
                var worksheet = workbook.Worksheet(1);

                var usedRange = worksheet.RangeUsed();
                if (usedRange == null)
                    return UploadError("The Excel sheet is empty.");

                var rows = usedRange.RowsUsed().Skip(1);

                // ---- File type check (header row) ----
                var headers = usedRange.FirstRow().Cells()
                    .Select(c => (c.GetValue<string>() ?? "").Trim().ToUpperInvariant())
                    .ToList();

                string HeaderAt(int col) => headers.Count >= col ? headers[col - 1] : "";

                bool isPluckingFile =
                    HeaderAt(7) == "W1" && HeaderAt(8) == "W2" &&
                    HeaderAt(9) == "W3" && HeaderAt(10) == "W4";

                bool hasWeighmentHeader = headers.Contains("W1") || headers.Contains("W2");

                if (typeid == "1" && !isPluckingFile)
                    return UploadError("You uploaded the wrong file");

                if (typeid == "2" && hasWeighmentHeader)
                    return UploadError("You uploaded the wrong file");

                if (typeid == "2")
                {
                    foreach (var row in rows)
                    {
                        int rowNo = row.RowNumber();

                        var data = new ExcelData
                        {
                            DATE = ReadDate(row.Cell(1), rowNo, errors),
                            EMPCODE = ReadText(row.Cell(2)),
                            BOOK = ReadText(row.Cell(3)),
                            SHFIT = ReadText(row.Cell(4)),
                            SECTION = ReadText(row.Cell(5)),
                            KAMJAI = ReadText(row.Cell(6)),
                            DIVISION = ReadText(row.Cell(7)),   // Division
                        };

                        dataList.Add(data);
                    }
                }
                if (typeid == "3")
                {
                    foreach (var row in rows)
                    {
                        var data = new ExcelData
                        {
                            NAME = row.Cell(1).GetValue<string>(),
                            BOOK = row.Cell(2).GetValue<string>(),
                            EMPCODE = row.Cell(3).GetValue<string>(),
                            DIVISION = row.Cell(4).GetValue<string>(),
                            WORKERTYPE = row.Cell(5).GetValue<string>()
                        };

                        dataList.Add(data);
                    }
                }
                if (typeid == "1")
                {
                    foreach (var row in rows)
                    {
                        int rowNo = row.RowNumber();

                        var data = new ExcelData
                        {
                            DATE = ReadDate(row.Cell(1), rowNo, errors),
                            EMPCODE = ReadText(row.Cell(2)),
                            BOOK = ReadText(row.Cell(3)),
                            SHFIT = ReadText(row.Cell(4)),
                            SECTION = ReadText(row.Cell(5)),
                            KAMJAI = ReadText(row.Cell(6)),
                            W1 = ReadDecimal(row.Cell(7), rowNo, "W1", errors),
                            W2 = ReadDecimal(row.Cell(8), rowNo, "W2", errors),
                            W3 = ReadDecimal(row.Cell(9), rowNo, "W3", errors),
                            W4 = ReadDecimal(row.Cell(10), rowNo, "W4", errors),
                            DIVISION = ReadText(row.Cell(11)),  // Division
                        };

                        dataList.Add(data);
                    }
                }
            }
            catch (Exception ex)
            {
                return UploadError("Could not read the Excel file: " + ex.Message);
            }

            if (errors.Count > 0)
            {
                var msg = string.Join(" | ", errors.Take(20));
                if (errors.Count > 20)
                    msg += $" ... and {errors.Count - 20} more";
                return UploadError(msg);
            }

            var result = excel.Create(dataList, date, typeid, unitid);

            return new JsonResult(result);
        }

        // ---------------- Helpers ----------------

        private static JsonResult UploadError(string message)
            => new JsonResult(Tuple.Create("error", message));

        private static string ReadText(IXLCell cell)
        {
            if (cell.IsEmpty()) return null;
            var text = cell.GetValue<string>()?.Trim();
            return string.IsNullOrEmpty(text) ? null : text;
        }

        // Always returns yyyy-MM-dd (culture-independent) or null
        private static string ReadDate(IXLCell cell, int rowNo, List<string> errors)
        {
            if (cell.IsEmpty()) return null; // SP reports "DATE is blank"

            if (cell.DataType == XLDataType.DateTime)
                return cell.GetDateTime().ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

            if (cell.DataType == XLDataType.Number)
            {
                var d = DateTime.FromOADate(cell.GetDouble());
                if (d.Year >= 2000)
                    return d.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

                errors.Add($"Row {rowNo}: DATE '{cell.GetValue<string>()}' is not a valid date");
                return null;
            }

            var text = cell.GetValue<string>()?.Trim();
            if (DateTime.TryParseExact(text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsed))
                return parsed.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

            errors.Add($"Row {rowNo}: DATE '{text}' is text, not a date. Format the DATE column as Date");
            return null;
        }

        // Blank → 0, number → value, anything else → row error
        private static decimal ReadDecimal(IXLCell cell, int rowNo, string column, List<string> errors)
        {
            if (cell.IsEmpty()) return 0;

            if (cell.DataType == XLDataType.Number)
                return Convert.ToDecimal(cell.GetDouble());

            var text = cell.GetValue<string>()?.Trim();
            if (string.IsNullOrEmpty(text)) return 0;

            if (decimal.TryParse(text, NumberStyles.Number, CultureInfo.InvariantCulture, out var value))
                return value;

            errors.Add($"Row {rowNo}: {column} '{text}' is not a number");
            return 0;
        }
    }
}