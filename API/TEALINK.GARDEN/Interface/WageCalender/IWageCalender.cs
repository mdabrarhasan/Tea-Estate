using TEALINK.GARDEN.Model.WageCalender;

namespace TEALINK.GARDEN.Interface.WageCalender
{
    public interface IWageCalender
    {
        IQueryable<WageCalenderModel> GetData(int unitid, int enroll);
        IQueryable<WageCalenderModel> GetDataByID(int id, int unitid);
        Tuple<string, string> Create(WageCalenderModel master);
        Task<Tuple<string, string>> SalaryGenerate(WageCalenderModel master);
        Tuple<string, string> Update(int id, WageCalenderModel master);
        Tuple<string, string> Delete(int id, int unitid, int enroll);
        Tuple<string, string> SalaryCancel(int unitid,  int bookid);
    }
}
