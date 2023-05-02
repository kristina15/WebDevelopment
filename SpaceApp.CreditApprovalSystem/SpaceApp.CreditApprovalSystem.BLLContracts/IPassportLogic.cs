using SpaceApp.CreditApprovalSystem.Entities;

namespace SpaceApp.CreditApprovalSystem.BLLContracts;

public interface IPassportLogic
{
    void AddPassport(Passport value);
    void AddScan(Passport value, ScanFile scan);
    Passport GetPassportBySeriesAndNumber(Passport value);
    Passport GetPassportById(int id);
    void UpdatePassport(Passport value);
}
