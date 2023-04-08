using SCB.Surkova.Credit_approval_system.BLL;
using SCB.Surkova.CreditApprovalSystem.BLL.Interfaces;
using SCB.Surkova.CreditApprovalSystem.DAL.Interfaces;
using SCB.Surkova.CreditApprovalSystem.Entities;
using SCB.Surkova.CreditApprovalSystem.Validation.Inter;
using System;
using System.Collections.Generic;

namespace SCB.Surkova.CreditApprovalSystem.BLL
{
    public class UserLogic : BaseLogic, IUserLogic
    {
        private readonly IUserDAL _userDao;
        private readonly IPassportLogic _passportLogic;
        private readonly IScanLogic _scanLogic;
        private readonly IUserValidator _userValidation;
        private readonly IScanValidator _scanValidator;

        public UserLogic(IUserDAL userDao, IPassportLogic passportLogic, IScanLogic scanLogic, IUserValidator userValidation, IScanValidator scanValidator)
        {
            _userDao = userDao;
            _passportLogic = passportLogic;
            _scanLogic = scanLogic;
            _userValidation = userValidation;
            _scanValidator = scanValidator;
        }

        public void AddUser(User value)
        {
           var validateResult = _userValidation.Validate(value, options => options.IncludeRuleSets("Default", "FIO"));
            GetValidationException(validateResult);

            _passportLogic.AddPassport(value.Passport);
            var passport = _passportLogic.GetPassportBySeriesAndNumber(value.Passport);
            value.Passport = passport;

            _userDao.AddUser(value);
        }

        public IEnumerable<User> GetUsers()
        {
            return _userDao.GetUsers();
        }

        public User GetUserByLoginAndPassword(User value)
        {
            var validateResult = _userValidation.Validate(value, options => options.IncludeRuleSets("Default", "Login", "Password"));
            GetValidationException(validateResult);

            return _userDao.GetUserByLoginAndPassword(value) ?? throw new ArgumentNullException();
        }

        public User GetUserByLogin(string login)
        {
            User userValue = new User { Login = login };
            var res = _userValidation.Validate(userValue, options => options.IncludeRuleSets("Login"));
            GetValidationException(res);

            return _userDao.GetUserByLogin(login);
        }

        public void UpdatePassword(User value)
        {
            var validateResult = _userValidation.Validate(value, options => options.IncludeRuleSets("Password"));
            GetValidationException(validateResult);

            _userDao.UpdatePassword(value);
        }

        public User GetUserById(int id)
        {
            return _userDao.GetUserById(id);
        }

        public void AddScan(User value, ScanFile scan)
        {
            var validateResult = _userValidation.Validate(value, options => options.IncludeRuleSets("Default"));
            GetValidationException(validateResult);

            validateResult = _scanValidator.Validate(scan, options => options.IncludeRuleSets("Default"));
            GetValidationException(validateResult);

            _userDao.AddScan(value, scan);
        }

        public void UpdateUser(User value)
        {
            var validateResult = _userValidation.Validate(value, options => options.IncludeRuleSets("Default", "FIO"));
            GetValidationException(validateResult);

            var user = GetUserById(value.Id);
            value.Passport.Id = user.Passport.Id;
            _passportLogic.UpdatePassport(value.Passport);

            if (user.Passport.Scans!=null)
            {
                if(user.Passport.Scans.Count==value.Passport.Scans?.Count)
                {
                    int i = 0;
                    foreach (var item in value.Passport.Scans)
                    {
                        user.Passport.Scans[i].Link = item.Link;
                        _scanLogic.UpdateScan(user.Passport.Scans[i]);
                        i++;
                    }
                }
            }
            else
            {
                foreach (var item in value.Passport.Scans)
                {
                    item.Title = TypeTitles.Passport;
                    var scan = _scanLogic.AddScan(item);

                    _passportLogic.AddScan(user.Passport, scan);
                }
            }

            if (value.AdditionalFile != null)
            {

                if (user.AdditionalFile != null)
                {
                    user.AdditionalFile.Link = value.AdditionalFile.Link;
                    _scanLogic.UpdateScan(user.AdditionalFile);
                }
                else
                {
                    var scan = _scanLogic.AddScan(value.AdditionalFile);
                    AddScan(user, scan);
                }
            }

            _userDao.UpdateUser(value);
        }

        public void AddRole(User value, string role)
        {
            _userValidation.ValidateNewRole(value, role);

            _userDao.AddRole(value, role);
        }

        public IEnumerable<User> GetUserBySurname(string surname)
        {
            if(string.IsNullOrWhiteSpace(surname))
            {
                return _userDao.GetUsers();
            }
            else
            {
                return _userDao.GetUserBySurname(surname);
            }
        }
    }
}
