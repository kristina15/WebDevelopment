using SCB.Surkova.CreditApprovalSystem.BLL;
using SCB.Surkova.CreditApprovalSystem.BLL.Interfaces;
using SCB.Surkova.CreditApprovalSystem.DAL;
using SCB.Surkova.CreditApprovalSystem.DAL.Interfaces;
using SCB.Surkova.CreditApprovalSystem.Hash.Interfaces;
using SCB.Surkova.CreditApprovalSystem.HashGenerator;
using SCB.Surkova.CreditApprovalSystem.Validation;
using SCB.Surkova.CreditApprovalSystem.Validation.Inter;

namespace SCB.Surkova.CreditApprovalSystem.Common
{
    public static class DependencyResolver
    {
        //private static IUserDAL _userDao;
        //private static IUserLogic _userLogic;
        //private static IPassportDao _passportDao;
        //private static IPassportLogic _passportLogic;
        //private static ILoanDao _loanDao;
        //private static ILoanLogic _loanLogic;
        //private static IScanDao _scanDao;
        //private static IScanLogic _scanLogic;
        //private static IUserValidator _userValidator;
        //private static IPassportValidator _passportValidator;
        //private static ILoanValidator _loanValidator;
        //private static IScanValidator _scanValidator;

        //public static IUserDAL UserData => _userDao ?? (_userDao = new UserDAL());
        //public static IUserLogic UserLogic => _userLogic ?? (_userLogic = new UserLogic(UserData, UserValidator, PassportLogic, PassportValidator, ScanValidator));
        //public static IPassportDao PassportData => _passportDao ?? (_passportDao = new PassportDao());
        //public static IPassportLogic PassportLogic => _passportLogic ?? (_passportLogic = new PassportLogic(PassportData, PassportValidator, ScanValidator));
        //public static ILoanDao LoanData => _loanDao ?? (_loanDao = new LoanDao());
        //public static ILoanLogic LoanLogic => _loanLogic ?? (_loanLogic = new LoanLogic(LoanData, LoanValidator, UserValidator));
        //public static IScanDao ScanData => _scanDao ?? (_scanDao = new ScanDao());
        //public static IScanLogic ScanLogic => _scanLogic ?? (_scanLogic = new ScanLogic(ScanData, ScanValidator));
        //public static IUserValidator UserValidator => _userValidator ?? (_userValidator = new UserValidator());
        //public static IPassportValidator PassportValidator => _passportValidator ?? (_passportValidator = new PassportValidator());
        //public static ILoanValidator LoanValidator => _loanValidator ?? (_loanValidator = new LoanValidator(PassportLogic));
        //public static IScanValidator ScanValidator => _scanValidator ?? (_scanValidator = new ScanValidator());
    }
}
