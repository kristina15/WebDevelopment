using FluentValidation.Results;

namespace SCB.Surkova.Credit_approval_system.BLL.Interfaces
{
    public interface IBaseLogic
    {
        void GetValidationException(ValidationResult result);
    }
}
