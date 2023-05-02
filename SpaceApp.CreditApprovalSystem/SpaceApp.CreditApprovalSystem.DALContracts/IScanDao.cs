using SpaceApp.CreditApprovalSystem.Entities;

namespace SpaceApp.CreditApprovalSystem.DALContracts;

public interface IScanDao
{
    ScanFile AddScan(ScanFile value);
    ScanFile GetScanById(int id);
    void UpdateScan(ScanFile value);
}
