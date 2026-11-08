

using TEALINK.Models.Securities;

namespace TEALINK.Repositories.Securities
{
    public interface IAPIValidationRepository
    {
        public APIValidationResponse apiValidation(APIValidationRequest aPIValidationRequest);
    }
}
