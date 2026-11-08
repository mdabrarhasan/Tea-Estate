namespace TEALINK.GARDEN.Model.DashBoard
{
    public class DashBoardView
    {
        public int ID { get; set; }

        public decimal SECTIONVALUE { get; set; }
     

        public string  SECTIONNAME { get; set; }

        public int LEAVE { get; set; }
        public int PRESENT { get; set; }
        public int ABSENT { get; set; }
        public int WORKER { get; set; }
        public int PLKER { get; set; }
        public decimal PLKVALUE { get; set; }
        public decimal PREVDAYPLKVALUE { get; set; }

    }
}
