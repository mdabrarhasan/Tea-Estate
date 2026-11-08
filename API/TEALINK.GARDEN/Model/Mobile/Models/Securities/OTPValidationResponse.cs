namespace TEALINK.Models.Securities
{
    public class OTPValidationResponse
    {
        public string CODE { get; set; }
        public bool STATUS { get; set; }
        public string MESSAGE { get; set; }
        public virtual OTPValidationResponseDetails OTPResponseDetails { get; set; }
    }
}
