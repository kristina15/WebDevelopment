using SCB.Surkova.CreditApprovalSystem.Entities;

namespace SCB.Surkova.CreditApprovalSystem.DAL.Interfaces
{
    public interface IScanDao
    {
        ScanFile AddScan(ScanFile value);
        ScanFile GetScanById(int id);
        void UpdateScan(ScanFile value);
    }
}
