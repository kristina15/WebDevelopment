﻿using SCB.Surkova.CreditApprovalSystem.Entities;
using System.Collections.Generic;

namespace SCB.Surkova.CreditApprovalSystem.DAL.Interfaces
{
    public interface IUserDAL
    {
        void AddUser(User value);
        User GetUserByLoginAndPassword(User value);
        User GetUserByLogin(string login);
        void UpdatePassword(User value);
        IEnumerable<User> GetUsers();
        User GetUserById(int id);
        void AddRole(User user, string role);
        void AddScan(User user, ScanFile scan);
        void UpdateUser(User value);
        IEnumerable<User> GetUserBySurname(string surname);
    }
}
