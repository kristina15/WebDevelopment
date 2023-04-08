using SCB.Surkova.Credit_approval_system.BLL;
using SCB.Surkova.CreditApprovalSystem.BLL.Interfaces;
using SCB.Surkova.CreditApprovalSystem.DAL.Interfaces;
using SCB.Surkova.CreditApprovalSystem.Entities;
using SCB.Surkova.CreditApprovalSystem.Validation.Inter;

namespace SCB.Surkova.CreditApprovalSystem.BLL
{
    public class ScanLogic : BaseLogic, IScanLogic
    {
        private readonly IScanDao _scanDao;
        private readonly IScanValidator _scanValidator;

        public ScanLogic(IScanDao scanDao, IScanValidator scanValidator)
        {
            _scanDao = scanDao;
            _scanValidator = scanValidator;
        }

        public ScanFile AddScan(ScanFile value)
        {
            var validateResult = _scanValidator.Validate(value, options => options.IncludeAllRuleSets());
            GetValidationException(validateResult);

            return _scanDao.AddScan(value);
        }

        public ScanFile GetScanById(int id)
        {
            return _scanDao.GetScanById(id);
        }

        public void UpdateScan(ScanFile value)
        {
            var validateResult = _scanValidator.Validate(value, options => options.IncludeRuleSets("Default"));
            GetValidationException(validateResult);

            _scanDao.UpdateScan(value);
        }
    }
}
