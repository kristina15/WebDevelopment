using SCB.Surkova.Credit_approval_system.BLL;
using SpaceApp.CreditApprovalSystem.BLLContracts;
using SpaceApp.CreditApprovalSystem.DALContracts;
using SpaceApp.CreditApprovalSystem.Entities;
using SpaceApp.CreditApprovalSystem.ModelValidatorContracts;

namespace SpaceApp.CreditApprovalSystem.BLL;

public class LoanLogic : BaseLogic, ILoanLogic
{
    private readonly ILoanDao _loanDao;
    private readonly ILoanValidator _loanValidator;
    private readonly IUserValidator _userValidator;

    public LoanLogic(ILoanDao applicationDao, ILoanValidator loanValidator, IUserValidator userValidator)
    {
        _loanDao = applicationDao;
        _loanValidator = loanValidator;
        _userValidator = userValidator;
    }

    public void AddLoan(Loan value)
    {
        var validateResult = _loanValidator.Validate(value, options => options.IncludeRuleSets("Default", "Correct loan"));
        GetValidationException(validateResult);

        _loanDao.AddLoan(value);
    }

    public IEnumerable<Loan> GetLoansOfUser(User value)
    {
        var validateResult = _userValidator.Validate(value, options => options.IncludeRuleSets("Default"));
        GetValidationException(validateResult);

        return _loanDao.GetLoansOfUser(value);
    }

    public Loan GetLoanById(int id)
    {
        return _loanDao.GetLoanById(id);
    }

    public IEnumerable<Loan> GetCurrentLoans()
    {
        return _loanDao.GetCurrentLoans();
    }

    public IEnumerable<Loan> GetHistoryOfLoans()
    {
        return _loanDao.GetHistoryOfLoans();
    }

    public void UpdateStatus(Loan value)
    {
        var validateResult = _loanValidator.Validate(value, options => options.IncludeRuleSets("Default"));
        GetValidationException(validateResult);

        _loanDao.UpdateStatus(value);
    }

    public void DeleteLoan(int id)
    {
        _loanDao.DeleteLoan(id);
    }
}