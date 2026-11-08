namespace TEALINK.Models.Securities
{
    public class PasswordResetResponse
    {
        public string CODE { get; set; }
        public bool STATUS { get; set; }
        public string MESSAGE { get; set; }
        public DateTime RESPONSETIME { get; set; }
        public virtual PasswordResetResponseDetails PasswordResetResponseDetails { get; set; }
    }
}
