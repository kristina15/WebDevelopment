using SCB.Surkova.CreditApprovalSystem.Api.Filters;
using SCB.Surkova.CreditApprovalSystem.Api.Models;
using SCB.Surkova.CreditApprovalSystem.Api.Models.LoanVMs;
using SCB.Surkova.CreditApprovalSystem.Api.Models.User;
using SCB.Surkova.CreditApprovalSystem.BLL.Interfaces;
using SCB.Surkova.CreditApprovalSystem.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using HttpDeleteAttribute = System.Web.Http.HttpDeleteAttribute;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using HttpPutAttribute = System.Web.Http.HttpPutAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;
using RoutePrefixAttribute = System.Web.Http.RoutePrefixAttribute;

namespace SCB.Surkova.CreditApprovalSystem.Api.Controllers.WebApi
{
    [RoutePrefix("api/LoanApi")]
    [BasicAuthentication]
    public class LoanApiController : ApiController
    {
        private readonly ILoanLogic _loanLogic;
        private readonly IUserLogic _userLogic;

        public LoanApiController()
        {
            _loanLogic = DependencyResolver.Current.GetService<ILoanLogic>();
            _userLogic = DependencyResolver.Current.GetService<IUserLogic>();
        }

        [HttpPost]
        [Route("CreateLoan")]
        [MyAuthorize(Roles = UserRoles.User)]
        public HttpResponseMessage CreateLoan([FromBody] CreateLoanVM newLoan)
        {
            var user = _userLogic.GetUserByLogin(HttpContext.Current.User.Identity.Name);

            newLoan.UserId = user.Id;
            newLoan.PassportId = user.Passport.Id;
            newLoan.PassportScanId = user.Passport.Scans?.First().Id;
            newLoan.AdditionalScanId = user.AdditionalFile?.Id;
            _loanLogic.AddLoan(AutoMapperConfig.mapper.Map<Loan>(newLoan));

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPut]
        [MyAuthorize(Roles = UserRoles.Underwriter)]
        public HttpResponseMessage UpdateStatus(int id, string status)
        {
            var value = new DisplayLoanVM
            {
                Id = id,
                Status = status
            };

            _loanLogic.UpdateStatus(AutoMapperConfig.mapper.Map<Loan>(value));
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpDelete]
        [MyAuthorize(Roles = UserRoles.User)]
        public HttpResponseMessage DeleteLoan(int id)
        {
            _loanLogic.DeleteLoan(id);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        [Route("GetCurrentLoans")]
        [MyAuthorize(Roles = UserRoles.Underwriter)]
        public HttpResponseMessage GetCurrentLoans()
        {
            var model = AutoMapperConfig.mapper.Map<IEnumerable<DisplayLoanVM>>(_loanLogic.GetCurrentLoans());
            return Request.CreateResponse(HttpStatusCode.OK, model);
        }

        [HttpGet]
        [Route("GetHistoryOfLoans")]
        [MyAuthorize(Roles = UserRoles.Underwriter)]
        public HttpResponseMessage GetHistoryOfLoans()
        {
            var loans = _loanLogic.GetHistoryOfLoans();

            return Request.CreateResponse(HttpStatusCode.OK, loans);
        }

        [HttpGet]
        [Route("GetLoanDetail")]
        [MyAuthorize(Roles = UserRoles.Underwriter)]
        public HttpResponseMessage GetLoanDetail(DisplayLoanVM value)
        {
            var loan = _loanLogic.GetLoanById(value.Id);
            var user = _userLogic.GetUserById(loan.UserId);
            var viewUser = AutoMapperConfig.mapper.Map<DisplayUserVM>(user);
            return Request.CreateResponse(HttpStatusCode.OK, viewUser);
        }
    }
}
