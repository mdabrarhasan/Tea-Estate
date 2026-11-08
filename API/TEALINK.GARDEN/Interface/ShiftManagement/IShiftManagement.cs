using TEALINK.GARDEN.Model.ShiftManagement;

namespace TEALINK.GARDEN.Interface.ShiftManagement
{
    public interface IShiftManagement
    {
        IQueryable<ShiftMasterView> GetData(int unitid, int enroll);
        IQueryable<ShiftLineView> GetDataByID(int unitid);
        //IQueryable<ShiftLineView> GetDataByLineID(int id);
        Tuple<string, string> Create(ShiftMaster master);
        Tuple<string, string> Update(int id, ShiftMaster master);
        Tuple<string, string> Delete(int id, int unitid, int enroll);
    }
}
