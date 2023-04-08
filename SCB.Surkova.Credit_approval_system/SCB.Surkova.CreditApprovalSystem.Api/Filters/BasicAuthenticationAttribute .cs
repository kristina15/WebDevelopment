using SCB.Surkova.CreditApprovalSystem.Api.Models;
using SCB.Surkova.CreditApprovalSystem.BLL.Interfaces;
using SCB.Surkova.CreditApprovalSystem.Entities;
using System;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Mvc;

namespace SCB.Surkova.CreditApprovalSystem.Api.Filters
{
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        private readonly IUserLogic _userLogic;

        public BasicAuthenticationAttribute()
        {
            _userLogic = DependencyResolver.Current.GetService<IUserLogic>();
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization != null)
            {
                var authToken = actionContext.Request.Headers
                    .Authorization.Parameter;
                var decodeauthToken = System.Text.Encoding.UTF8.GetString(
                    Convert.FromBase64String(authToken));
                var arrUserNameandPassword = decodeauthToken.Split(':');
                string username = arrUserNameandPassword[0];
                string password = arrUserNameandPassword[1];

                var user = _userLogic.GetUserByLoginAndPassword(AutoMapperConfig.mapper.Map<User>(new LoginVM { Login = username, Password = password }));
                if (user!=null)
                {
                    var identity = new GenericIdentity(username);
                    IPrincipal principal = new GenericPrincipal(identity, user.Roles.ToArray());
                    if (HttpContext.Current != null)
                    {
                        HttpContext.Current.User = principal;
                    }
                }
                else
                {
                    actionContext.Response = actionContext.Request
                        .CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
            else
            {
                actionContext.Response = actionContext.Request
                    .CreateResponse(HttpStatusCode.Unauthorized);
            }
        }
    }
}