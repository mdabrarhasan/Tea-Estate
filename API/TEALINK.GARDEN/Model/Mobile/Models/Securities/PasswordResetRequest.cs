namespace TEALINK.Models.Securities
{
    public class PasswordResetRequest
    {
        public string? TYPE { get; set; }
        public string? USERID { get; set; }
        public string? PASSWORDOLD { get; set; }
        public string? PASSWORDNEW { get; set; }

        public string? CHANNEL { get; set; }
        public string? DEVICEINFO { get; set; }
    }
}
