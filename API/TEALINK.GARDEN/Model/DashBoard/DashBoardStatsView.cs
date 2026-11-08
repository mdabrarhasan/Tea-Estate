using Microsoft.EntityFrameworkCore;

namespace TEALINK.GARDEN.Model.DashBoard
{
    [Keyless]
    public class DashBoardStatsView
    {
        public decimal TODAYPLK { get; set; }
        public decimal PREVDAYPLK { get; set; }

        public int PERMPRESENTPLK { get; set; }
        public int PERMPRESENTNONPLK { get; set; }
        public int CASUALPRESENTPLK { get; set; }
        public int CASUALPRESENTNONPLK { get; set; }

        public int PERMABSENT { get; set; }
        public int CASUALABSENT { get; set; }

        public int PERMOT { get; set; }
        public int CASUALOT { get; set; }

        public decimal TOTALBASIC { get; set; }
        public decimal TOTALOT { get; set; }
        public int PIEPRESENT { get; set; }
        public int PIEABSENT { get; set; }
        public int PIELEAVE { get; set; }
    }

    [Keyless]
    public class SectionPluckingView
    {
        public string SECTIONCODE { get; set; } = "";
        public decimal PLUCKEDQTY { get; set; }
    }
}