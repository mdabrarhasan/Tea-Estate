using Microsoft.EntityFrameworkCore;

namespace TEALINK.GARDEN.Model.DashBoard
{
    [Keyless]
    public class PluckingTrendView
    {
        public DateTime PLUCKDATE { get; set; }
        public decimal TOTQTY { get; set; }
    }
}