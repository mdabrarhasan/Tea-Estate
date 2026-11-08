using TEALINK.GARDEN.Model.DailyPlucking;
using TEALINK.GARDEN.Model.SalaryGenerate;

namespace TEALINK.GARDEN.Interface.SalaryGenerate
{
    public interface ISalaryGenerate
    {
        public Tuple<string, string> Get(string sdate, string edate, int unitid, int enroll);
        //IQueryable<DailyPluckingLineView> GetDataByID(int id, string pluckeddate, int unitid, int enroll);
        //IQueryable<SalaryGenerateModel> Create(string sdate, string edate, int unitid, int enroll);
        //Tuple<string, string> Create(List<DailyPluckingView> master);
        //Tuple<string, string> Delete(int id, int unitid, int enroll);

        //IQueryable<DailyPluckingByWeighment> GetQtyWeighmentWise(string pluckeddate, int unitid);
        //IQueryable<DailyPluckingAttendentEmp> GetDataCountDateWise(string pluckeddate, int unitid);
    }
}
