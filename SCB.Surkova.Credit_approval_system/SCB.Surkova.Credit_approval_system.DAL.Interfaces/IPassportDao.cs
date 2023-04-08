using SCB.Surkova.CreditApprovalSystem.Entities;

namespace SCB.Surkova.CreditApprovalSystem.DAL.Interfaces
{
    public interface IPassportDao
    {
        void AddPassport(Passport value);
        void AddScan(Passport value, ScanFile scan);
        Passport GetPassportBySeriesAndNumber(Passport value);
        Passport GetPassportById(int id);
        void UpdatePassport(Passport value);
    }
}
