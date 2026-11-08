
namespace TEALINK.Models.Generic
{
    public class CommonModelResponse
    {
        public string CODE { get; set; }
        public bool STATUS { get; set; }
        public string MESSAGE { get; set; }
        public DateTime RESPONSETIME { get; set; }
        public  List<CommonModelResponseDetails>ResponseDetails { get; set; }
    }
}
