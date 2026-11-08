using TEALINK.GARDEN.Model.DailyPlucking;
using TEALINK.GARDEN.Model.ExeclData;

namespace TEALINK.GARDEN.Interface.Excel
{
    public interface IExcel
    {
        Tuple<string, string> Create(List<ExcelData> master, string date, string typeid, string unitid);
    }
}
