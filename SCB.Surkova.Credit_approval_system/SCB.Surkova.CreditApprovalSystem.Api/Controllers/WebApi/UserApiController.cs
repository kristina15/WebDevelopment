using SCB.Surkova.CreditApprovalSystem.Api.Filters;
using SCB.Surkova.CreditApprovalSystem.Api.Models;
using SCB.Surkova.CreditApprovalSystem.Api.Models.ScanVMs;
using SCB.Surkova.CreditApprovalSystem.Api.Models.User;
using SCB.Surkova.CreditApprovalSystem.Api.Models.UserVMs;
using SCB.Surkova.CreditApprovalSystem.BLL.Interfaces;
using SCB.Surkova.CreditApprovalSystem.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using HttpPutAttribute = System.Web.Http.HttpPutAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;
using RoutePrefixAttribute = System.Web.Http.RoutePrefixAttribute;

namespace SCB.Surkova.CreditApprovalSystem.Api.Controllers.WebApi
{
    [RoutePrefix("api/UserApi")]
    [BasicAuthentication]
    public class UserApiController : ApiController
    {
        private readonly IUserLogic _userLogic;

        public UserApiController()
        {
            _userLogic = DependencyResolver.Current.GetService<IUserLogic>();
        }

        [HttpGet]
        [Route("GetProfile")]
        [MyAuthorize(Roles = UserRoles.User)]
        public DisplayUserVM GetProfile()
        {
            var user = _userLogic.GetUserByLogin(HttpContext.Current.User.Identity.Name);
            var viewUser = AutoMapperConfig.mapper.Map<DisplayUserVM>(user);

            return viewUser;
        }

        [HttpGet]
        [Route("GetUsers")]
        [MyAuthorize(Roles = UserRoles.Admin)]
        public IEnumerable<DisplayUserVM> GetUsers()
        {
            var model = AutoMapperConfig.mapper.Map<IEnumerable<DisplayUserVM>>(_userLogic.GetUsers());

            return model;
        }

        [HttpPut]
        [Route("EditRole")]
        [MyAuthorize(Roles = UserRoles.Admin)]
        public void EditRole([FromBody] DisplayUserVM model, string roles)
        {
            var user = _userLogic.GetUserById(AutoMapperConfig.mapper.Map<User>(model).Id);
            _userLogic.AddRole(user, roles);
        }

        [HttpPut]
        [Route("EditProfile")]
        [MyAuthorize(Roles = UserRoles.Admin)]
        public void EditProfile([FromBody] EditUserVM value)
        {
            _userLogic.UpdateUser(AutoMapperConfig.mapper.Map<User>(value));
        }

        [HttpGet]
        [Route("GetUserBySurname")]
        [MyAuthorize(Roles = UserRoles.Admin)]
        public DisplayUserVM GetUserBySurname(string surname)
        {
            var model = _userLogic.GetUserBySurname(surname);
            var viewModel = AutoMapperConfig.mapper.Map<DisplayUserVM>(model);
            return viewModel;
        }
    }
}
