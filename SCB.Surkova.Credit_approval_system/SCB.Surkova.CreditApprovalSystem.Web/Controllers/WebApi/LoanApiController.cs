using SCB.Surkova.CreditApprovalSystem.BLL.Interfaces;
using SCB.Surkova.CreditApprovalSystem.Entities;
using SCB.Surkova.CreditApprovalSystem.Web.Models;
using SCB.Surkova.CreditApprovalSystem.Web.Models.LoanVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SCB.Surkova.CreditApprovalSystem.Web.Controllers.WebApi
{
    public class LoanApiController : Controller
    {
        private readonly IUserLogic _userLogic;
        private readonly ILoanLogic _loanLogic;

        public LoanApiController(IUserLogic userLogic, ILoanLogic loanLogic)
        {
            _userLogic = userLogic;
            _loanLogic = loanLogic;
        }

        [Authorize(Roles = UserRoles.User)]
        public ActionResult CreateLoan()
        {
            var user = _userLogic.GetUserByLogin(User.Identity.Name);
            var loans = _loanLogic.GetLoansOfUser(user);
            var viewLoans = AutoMapperConfig.mapper.Map<IEnumerable<DisplayLoanVM>>(loans);
            ViewBag.TemporaryLoans = AutoMapperConfig.mapper.Map<IEnumerable<DisplayLoanVM>>(GetLoans());
            return View(viewLoans);
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.User)]
        public ActionResult CreateLoan(long sum)
        {
            CreateLoanVM loan = new CreateLoanVM
            {
                Sum = sum
            };

            var newLoan = AutoMapperConfig.mapper.Map<DisplayLoanVM>(loan);
            GetLoans().Add(AutoMapperConfig.mapper.Map<CreateLoanVM>(newLoan));

            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Views/Shared/LoanPartial/_UserLoanPartial.cshtml", newLoan);
            }

            return View();
        }

        private List<CreateLoanVM> GetLoans()
        {
            var loans = Session["newLoans"] as List<CreateLoanVM>;
            if (loans == null)
            {
                loans = new List<CreateLoanVM>();
                Session["newLoans"] = loans;
            }

            return loans;
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.User)]
        public JsonResult SaveLoans()
        {
            var user = _userLogic.GetUserByLogin(User.Identity.Name);
            var loans = GetLoans();
            foreach (var item in loans)
            {
                item.UserId = user.Id;
                item.PassportId = user.Passport.Id;
                item.PassportScanId = user.Passport.Scans?.First().Id;
                item.AdditionalScanId = user.AdditionalFile?.Id;
                try
                {
                    _loanLogic.AddLoan(AutoMapperConfig.mapper.Map<Loan>(item));
                }
                catch
                {
                    return Json(false);
                }
            }

            loans.Clear();

            return Json(true);
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.User)]
        public JsonResult DeleteLoan(int id)
        {
            _loanLogic.DeleteLoan(id);

            return Json(true);
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.User)]
        public JsonResult DeleteNewLoan(int sum)
        {
            var loan = GetLoans().First(l => l.Sum == sum);
            GetLoans().Remove(loan);

            return Json(true);
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Underwriter)]
        public JsonResult UpdateStatus(int id, string status)
        {
            var value = new DisplayLoanVM
            {
                Id = id,
                Status = status
            };

            _loanLogic.UpdateStatus(AutoMapperConfig.mapper.Map<Loan>(value));
            return Json(true);
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public JsonResult AddRole(string login, string role)
        {
            try
            {
                var user = _userLogic.GetUserByLogin(login);
                _userLogic.AddRole(user, role);

                return Json(role);
            }
            catch(Exception ex)
            {
                return Json(ex);
            }
        }
    }
}