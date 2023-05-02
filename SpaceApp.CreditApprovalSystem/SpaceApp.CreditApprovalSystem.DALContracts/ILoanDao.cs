using SpaceApp.CreditApprovalSystem.Entities;

namespace SpaceApp.CreditApprovalSystem.DALContracts;

public interface ILoanDao
{
    void AddLoan(Loan value);
    IEnumerable<Loan> GetLoansOfUser(User value);
    Loan GetLoanById(int id);
    IEnumerable<Loan> GetCurrentLoans();
    IEnumerable<Loan> GetHistoryOfLoans();
    void UpdateStatus(Loan value);
    void DeleteLoan(int id);
}
