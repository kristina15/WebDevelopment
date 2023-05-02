using SCB.Surkova.Credit_approval_system.BLL;
using SpaceApp.CreditApprovalSystem.BLLContracts;
using SpaceApp.CreditApprovalSystem.DALContracts;
using SpaceApp.CreditApprovalSystem.Entities;
using SpaceApp.CreditApprovalSystem.ModelValidatorContracts;

namespace SpaceApp.CreditApprovalSystem.BLL;

public class PassportLogic : BaseLogic, IPassportLogic
{
    private readonly IPassportDao _passportDao;
    private readonly IPassportValidator _passportValidator;
    private readonly IScanValidator _scanValidator;

    public PassportLogic(IPassportDao passportDao, IPassportValidator passportValidator, IScanValidator scanValidator)
    {
        _passportDao = passportDao;
        _passportValidator = passportValidator;
        _scanValidator = scanValidator;
    }

    public void AddPassport(Passport value)
    {
        var validateResult = _passportValidator.Validate(value, options => options.IncludeRuleSets("Default", "Series and number"));
        GetValidationException(validateResult);

        _passportDao.AddPassport(value);
    }

    public void AddScan(Passport value, ScanFile scan)
    {
        var validateResult = _passportValidator.Validate(value, options => options.IncludeRuleSets("Default"));
        GetValidationException(validateResult);

        validateResult = _scanValidator.Validate(scan, options => options.IncludeRuleSets("Default"));
        GetValidationException(validateResult);

        _passportDao.AddScan(value, scan);
    }

    public Passport GetPassportBySeriesAndNumber(Passport value)
    {
        var validateResult = _passportValidator.Validate(value, options => options.IncludeRuleSets("Series and number"));
        GetValidationException(validateResult);

        return _passportDao.GetPassportBySeriesAndNumber(value);
    }

    public Passport GetPassportById(int id)
    {
        return _passportDao.GetPassportById(id);
    }

    public void UpdatePassport(Passport value)
    {
        var validateResult = _passportValidator.Validate(value, options => options.IncludeRuleSets("Default"));
        GetValidationException(validateResult);

        _passportDao.UpdatePassport(value);
    }
}
