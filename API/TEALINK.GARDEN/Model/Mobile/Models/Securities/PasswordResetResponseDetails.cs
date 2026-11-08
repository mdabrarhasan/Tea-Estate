namespace TEALINK.Models.Securities
{
    public class PasswordResetResponseDetails
    {
        public string? Otp { get; set; }
        public string Message { get; set; }

        public string? MobileNumber { get; set; }
    }
}
