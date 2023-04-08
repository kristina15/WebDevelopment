using SCB.Surkova.CreditApprovalSystem.Entities;

namespace SCB.Surkova.CreditApprovalSystem.BLL.Interfaces
{
    public interface IPassportLogic
    {
        void AddPassport(Passport value);
        void AddScan(Passport value, ScanFile scan);
        Passport GetPassportBySeriesAndNumber(Passport value);
        Passport GetPassportById(int id);
        void UpdatePassport(Passport value);
    }
}
