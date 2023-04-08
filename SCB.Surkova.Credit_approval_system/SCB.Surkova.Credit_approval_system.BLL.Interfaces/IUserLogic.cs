using SCB.Surkova.CreditApprovalSystem.Entities;
using System.Collections.Generic;

namespace SCB.Surkova.CreditApprovalSystem.BLL.Interfaces
{
    public interface IUserLogic
    {
        void AddUser(User value);
        User GetUserByLoginAndPassword(User value);
        User GetUserByLogin(string login);
        void UpdatePassword(User value);
        User GetUserById(int id);
        void AddRole(User value, string role);
        void AddScan(User value, ScanFile scan);
        IEnumerable<User> GetUsers();
        void UpdateUser(User value);
        IEnumerable<User> GetUserBySurname(string surname);
    }
}
