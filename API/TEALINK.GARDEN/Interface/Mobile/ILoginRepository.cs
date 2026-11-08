
using TEALINK.Models.Securities;

namespace TEALINK.GARDEN.Interface.Mobile
{
    public interface ILoginRepository
    {
        //Task<Login> getLogin();
        public LoginResponseDetails GetUserLogin(LoginRequest loginRequest);
        public PasswordResetResponseDetails PasswordReset(PasswordResetRequest PasswordReset);
        public OTPValidationResponseDetails OTPValidation(OTPValidationRequest oTPRequest);

    }
}
