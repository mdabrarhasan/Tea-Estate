
using TEALINK.GARDEN.Model.DailyPlucking;


namespace TEALINK.GARDEN.Interface.DailyPlucking
{
    public interface IDailyPlucking
    {
        IQueryable<DailyPluckingView> GetData(string pluckingdate, int kamjaritypeid, int bookid, int operatorid, int deviceid, int batchid, int weighmentid, int divisionid, int unitid, int enroll);
        IQueryable<DailyPluckingLineView> GetDataByID(string id, string pluckeddate, int unitid, int enroll);
        
        Tuple<string, string> Create(List<DailyPluckingView> master);

        Tuple<string, string> UpdateWeighment(int id, int weighment, int unitid, int enroll);
        Tuple<string, string> Delete(int id, int unitid, int enroll);

        IQueryable<DailyPluckingByWeighment> GetQtyWeighmentWise(string pluckeddate,int bookid, int unitid);
        IQueryable<DailyPluckingAttendentEmp> GetDataCountDateWise(string pluckeddate,int bookid, int unitid);
    }
}
