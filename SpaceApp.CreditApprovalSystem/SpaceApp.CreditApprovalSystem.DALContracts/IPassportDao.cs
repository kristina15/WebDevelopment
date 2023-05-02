using SpaceApp.CreditApprovalSystem.Entities;

namespace SpaceApp.CreditApprovalSystem.DALContracts;

public interface IPassportDao
{
    void AddPassport(Passport value);
    void AddScan(Passport value, ScanFile scan);
    Passport GetPassportBySeriesAndNumber(Passport value);
    Passport GetPassportById(int id);
    void UpdatePassport(Passport value);
}
