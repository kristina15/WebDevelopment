using SCB.Surkova.CreditApprovalSystem.BLL.Interfaces;
using SCB.Surkova.CreditApprovalSystem.Web.Models;
using SCB.Surkova.CreditApprovalSystem.Web.Models.LoanVMs;
using SCB.Surkova.CreditApprovalSystem.Web.Models.User;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SCB.Surkova.CreditApprovalSystem.Web.Controllers
{

    public class LoanController : Controller
    {
        private readonly ILoanLogic _loanLogic;
        private readonly IUserLogic _userLogic;

        public LoanController(IUserLogic userLogic, ILoanLogic loanLogic)
        {
            _loanLogic = loanLogic;
            _userLogic = userLogic;
        }

        [Authorize(Roles = UserRoles.Underwriter)]
        public ActionResult GetCurrentLoans()
        {
            var model = AutoMapperConfig.mapper.Map<IEnumerable<DisplayLoanVM>>(_loanLogic.GetCurrentLoans());
            return View(model);
        }

        [Authorize(Roles = UserRoles.Underwriter)]
        public ActionResult GetHistoryOfLoans()
        {
            var loans = _loanLogic.GetHistoryOfLoans();
            var model = new List<Tuple<DisplayLoanVM, DisplayUserVM>>();
            foreach (var item in loans)
            {
                var loan = AutoMapperConfig.mapper.Map<DisplayLoanVM>(item);
                var user = _userLogic.GetUserById(item.UserId);
                var viewUser = AutoMapperConfig.mapper.Map<DisplayUserVM>(user);
                model.Add(Tuple.Create(loan, viewUser));
            }

            return View(model);
        }

        [Authorize(Roles = UserRoles.Underwriter)]
        public ActionResult LoanDetail(DisplayLoanVM value)
        {
            var loan = _loanLogic.GetLoanById(value.Id);
            var user = _userLogic.GetUserById(loan.UserId);
            var viewUser = AutoMapperConfig.mapper.Map<DisplayUserVM>(user);
            return View(Tuple.Create(value, viewUser));
        }
    }
}