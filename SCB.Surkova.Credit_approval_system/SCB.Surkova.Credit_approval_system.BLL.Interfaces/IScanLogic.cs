using SCB.Surkova.CreditApprovalSystem.Entities;

namespace SCB.Surkova.CreditApprovalSystem.BLL.Interfaces
{
    public interface IScanLogic 
    {
        ScanFile AddScan(ScanFile value);
        ScanFile GetScanById(int id);
        void UpdateScan(ScanFile value);
    }
}
