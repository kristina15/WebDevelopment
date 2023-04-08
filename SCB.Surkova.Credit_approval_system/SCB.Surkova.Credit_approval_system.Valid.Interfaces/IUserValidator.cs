using SCB.Surkova.Credit_approval_system.Entities;

namespace SCB.Surkova.Credit_approval_system.Valid.Interfaces
{
    public interface IUserValidator
    {
        bool NameValidate(string name);
    }
}
