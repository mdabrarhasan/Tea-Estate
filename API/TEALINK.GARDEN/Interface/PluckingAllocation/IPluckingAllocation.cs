using TEALINK.GARDEN.Model.Division;
using TEALINK.GARDEN.Model.PluckingAllocation;

namespace TEALINK.GARDEN.Interface.PluckingAllocation
{
    public interface IPluckingAllocation
    {
        public IQueryable <PluckingLanding> GetDataByID(int unit, int id,string date);
       public Tuple<string, string> Create(PluckingMaster pm,PluckingLanding [] pl);
    }
}
