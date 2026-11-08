using System.ComponentModel.DataAnnotations;

namespace TEALINK.GARDEN.Model.Kamjari
{
    public class KamjariMaster
    {
        [Key]
        public int? ID { get; set; }
        public string NAME { get; set; }
        public string CODE { get; set; }
        public int? KAMJARITYPEID { get; set; }
        public int? MARKASDOUBLY { get; set; }

        public int? EXCLUDEINWAGES { get; set; }
        public int? ACCOUNTID { get; set; }

        public string TASKUNIT { get; set; }
        public string TASKMEN { get; set; }
        public string TASKWOMEN { get; set; }



        public string PAYOFPOST { get; set; }
        public int? MASTERTYPEID { get; set; }
        public int? ISMATERIALACCOUNT { get; set; }

        public decimal DOUBLYRATE { get; set; }
        public decimal FIXEDDOUBLYRATE { get; set; }


        public int? ENROLL { get; set; }
        public int? UNITID { get; set; }
    }
}
