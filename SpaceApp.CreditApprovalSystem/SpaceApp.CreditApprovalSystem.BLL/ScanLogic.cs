using SCB.Surkova.Credit_approval_system.BLL;
using SpaceApp.CreditApprovalSystem.BLLContracts;
using SpaceApp.CreditApprovalSystem.DALContracts;
using SpaceApp.CreditApprovalSystem.Entities;
using SpaceApp.CreditApprovalSystem.ModelValidatorContracts;

namespace SpaceApp.CreditApprovalSystem.BLL;

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
