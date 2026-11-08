using TEALINK.GARDEN.Model.LeaveManagement;

namespace TEALINK.GARDEN.Interface.LeaveMnagement
{
    public interface ILeaveMangement
    {
        public Tuple<string, string> Create(LeaveMaster master);
      
        public IQueryable<LeaveGetdata> LeaveGet(int workerid, int leavetypeid);
        public IQueryable<LeaveDetails> Getdata(int unitid);
       public Tuple<string, string> Delete(int id, int unitid, int enroll);

    }
}
