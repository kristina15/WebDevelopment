using SCB.Surkova.CreditApprovalSystem.BLL.Interfaces;
using System;
using System.Web.Mvc;
using System.Web.Security;

namespace SCB.Surkova.CreditApprovalSystem.Web.Models
{
    public class MyRoleProvider : RoleProvider
    {
        private readonly IUserLogic _userLogic;

        public MyRoleProvider()
        {
            _userLogic = DependencyResolver.Current.GetService<IUserLogic>();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            return _userLogic.GetUserByLogin(username).Roles.Contains(roleName);
        }

        public override string[] GetRolesForUser(string username)
        {
            if (username.StartsWith("Admin"))
            {
                return new[] { "Admin" };
            }

            var user = _userLogic.GetUserByLogin(username);
            if (user != null && user.Roles.Contains(UserRoles.Underwriter))
            {
                return new[] { "Underwriter" };
            }

            return new[] { "User" };
        }

        #region NotEmplemented
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }


        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}