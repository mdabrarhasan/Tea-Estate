namespace TEALINK.GARDEN.Model.PluckingAllocation
{
    public class PluckingLanding
    {
        public int ID { get; set; }
        public decimal MAX { get; set; }
        public decimal MIN { get; set; }
        public decimal AVG { get; set; }
        public decimal ELP { get; set; }
        public decimal TARGET { get; set; }
        public decimal MINTASK { get; set; }
        public decimal MAXTASK { get; set; }
        public decimal EXTRARATE { get; set; }
        public decimal AVGPLUCKQTY { get; set; }
        public decimal MINPLUCKQTY { get; set; }
        public decimal MAXPLUCKQTY { get; set; }
        public decimal TARGETQTY { get; set; }
        public decimal MINQTY { get; set; }
        public decimal MAXQTY { get; set; }
        public string BATCH { get; set; }
        public int BATCHID { get; set; }
        public int BOOKID { get; set; }
        public int OPERATORID { get; set; }
        public int DEVICEID { get; set; }
        public int WEIGHMENTID { get; set; }
        public int KAMJARITYPEID { get; set; }
    }
    public class PluckingMaster
    {
        public int ID { get; set; }
        public int PLUCKINGTYPEID { get; set; }
        public string PLUCKINGDATE { get; set; }
        public decimal ELPDLPRATE { get; set; }
        public decimal TARGETKG { get; set; }
        public decimal MINTASKKG { get; set; }
        public decimal MAXTASKKG { get; set; }
        public decimal EXTRARATE { get; set; }
        public decimal AVGPLUCKED { get; set; }
        public decimal MINPLUCKED { get; set; }
        public decimal MAXPLUCKED { get; set; }
        public Boolean DISABLEDISINCENTIVE { get; set; }
        public Boolean ENABLEPLUCKINGEXTRA { get; set; }
        public int ENROLL { get; set; }

        public int UNIT { get; set; }
    }
    public class PluckingSaveRequest
    {
        public PluckingLanding[] master { get; set; }
        public PluckingMaster pl { get; set; }
       
    }
}
