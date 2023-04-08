using SCB.Surkova.Credit_approval_system.Entities;

namespace SCB.Surkova.Credit_approval_system.Valid.Interfaces
{
    public interface IPassportValidator
    {
        bool ValidatePassport(Passport passport);
    }
}
