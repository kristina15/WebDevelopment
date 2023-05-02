using SpaceApp.CreditApprovalSystem.Entities;

namespace SpaceApp.CreditApprovalSystem.BLLContracts;

public interface IScanLogic
{
    ScanFile AddScan(ScanFile value);
    ScanFile GetScanById(int id);
    void UpdateScan(ScanFile value);
}
