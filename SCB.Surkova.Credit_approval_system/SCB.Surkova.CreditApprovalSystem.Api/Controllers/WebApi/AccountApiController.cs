using SCB.Surkova.CreditApprovalSystem.Api.Models;
using SCB.Surkova.CreditApprovalSystem.Api.Models.User;
using SCB.Surkova.CreditApprovalSystem.BLL.Interfaces;
using SCB.Surkova.CreditApprovalSystem.Entities;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Security;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;
using RoutePrefixAttribute = System.Web.Http.RoutePrefixAttribute;

namespace SCB.Surkova.CreditApprovalSystem.Api.Controllers.WebApi
{
    [RoutePrefix("api/AccountApi")]
    public class AccountApiController : ApiController
    {
        private readonly IUserLogic _userLogic;
        private readonly IPassportLogic _passportLogic;

        public AccountApiController()
        {
            _userLogic = DependencyResolver.Current.GetService<IUserLogic>();
            _passportLogic = DependencyResolver.Current.GetService<IPassportLogic>();
        }

        [HttpPost]
        [Route("Login")]
        public DisplayUserVM Login(LoginVM value)
        {
            var user = AutoMapperConfig.mapper.Map<User>(value);
            var receivedUser = AutoMapperConfig.mapper.Map<DisplayUserVM>(_userLogic.GetUserByLoginAndPassword(user));
            return receivedUser;
        }

        [HttpPost]
        [Route("Register")]
        public void Register([FromBody] RegisterVM model)
        {
            var user = _userLogic.GetUserByLogin(model.Login);
            var passport = _passportLogic.GetPassportBySeriesAndNumber(new Passport { Series = model.Passport.Series, Number = model.Passport.Number });
            if (user == null && passport == null)
            {
                _userLogic.AddUser(AutoMapperConfig.mapper.Map<User>(model));

                user = _userLogic.GetUserByLogin(model.Login);
                if (Roles.IsUserInRole(UserRoles.Admin))
                {
                    _userLogic.AddRole(user, UserRoles.Admin);
                }
                else
                {
                    _userLogic.AddRole(user, UserRoles.User);
                }

                FormsAuthentication.SetAuthCookie(user.Login, createPersistentCookie: true);
            }
        }

        [HttpPost]
        [Route("PasswordRecovery")]
        public void PasswordRecovery([FromBody] PasswordRecoveryVM value)
        {
            var user = _userLogic.GetUserByLogin(value.Login);
            if (user != null)
            {
                if (user.Passport.Series == value.Passport.Series && user.Passport.Number == value.Passport.Number)
                {
                    value.Id = user.Id;
                    _userLogic.UpdatePassword(AutoMapperConfig.mapper.Map<User>(value));
                }
            }
        }
    }
}
