using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;
using SCB.Surkova.CreditApprovalSystem.BLL.Interfaces;
using SCB.Surkova.CreditApprovalSystem.Entities;
using SCB.Surkova.CreditApprovalSystem.Web.Models;
using SCB.Surkova.CreditApprovalSystem.Web.Models.User;
using Roles = System.Web.Security.Roles;

namespace SCB.Surkova.CreditApprovalSystem.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserLogic _userLogic;
        private readonly IPassportLogic _passportLogic;

        public AccountController(IUserLogic userLogic, IPassportLogic passportLogic)
        {
            _userLogic = userLogic;
            _passportLogic = passportLogic;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginVM value, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = AutoMapperConfig.mapper.Map<User>(value);
                var receivedUser = _userLogic.GetUserByLogin(user.Login);
                if (receivedUser != null)
                {
                    receivedUser = _userLogic.GetUserByLoginAndPassword(user);
                    if (receivedUser != null)
                    {
                        FormsAuthentication.SetAuthCookie(value.Login, createPersistentCookie: true);

                        if (returnUrl != null)
                        {
                            return Redirect(returnUrl);
                        }

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Bad password");
                        return View(value);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Bad login");
                    return View(value);
                }
            }
            
            return View(value);
        }

        public ActionResult Register(string returnUrl)
        {
            ViewBag.ReturlUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var user = _userLogic.GetUserByLogin(model.Login);
                var passport = _passportLogic.GetPassportBySeriesAndNumber(new Passport { Series = model.Passport.Series, Number = model.Passport.Number });
                if (user == null && passport == null)
                {
                    try
                    {
                        _userLogic.AddUser(AutoMapperConfig.mapper.Map<User>(model));
                        
                        FormsAuthentication.SetAuthCookie(model.Login, createPersistentCookie: true);

                        user = _userLogic.GetUserByLogin(model.Login);
                        if (Roles.IsUserInRole(UserRoles.Admin))
                        {
                            _userLogic.AddRole(user, UserRoles.Admin);
                        }
                        else
                        {
                            _userLogic.AddRole(user, UserRoles.User);
                        }

                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", ex.Message);
                    }
                }
                else if (passport != null)
                {
                    ModelState.AddModelError("", "Such passport already exists");
                }
                else
                {
                    ModelState.AddModelError("", "Such user already exists");
                }
            }

            return View(model);
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }

        [ChildActionOnly]
        public ActionResult LoginBlock()
        {
            if (User.Identity.IsAuthenticated)
            {
                return PartialView("_LogoutPartial");
            }
            else
            {
                return PartialView("_LoginPartial");
            }
        }

        [ChildActionOnly]
        public ActionResult ToolBox()
        {
            if(Roles.IsUserInRole(UserRoles.Admin))
            {
                ViewBag.TemporaryUsers = AutoMapperConfig.mapper.Map<IEnumerable<DisplayUserVM>>(_userLogic.GetUsers());
                return PartialView("RolePartial/_IndexToolBoxAdminPartial");
            }

            if(Roles.IsUserInRole(UserRoles.Underwriter))
            {
                return PartialView("RolePartial/_IndexToolBoxUndewriterPartial");
            }

            if(Roles.IsUserInRole(UserRoles.User))
            {
                return PartialView("RolePartial/_IndexToolBoxUserPartial");
            }

            return null;
        }

        public ActionResult PasswordRecovery()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PasswordRecovery(PasswordRecoveryVM value)
        {
            if (ModelState.IsValid)
            { 
                var user = _userLogic.GetUserByLogin(value.Login);
                if (user != null)
                {
                    if (user.Passport.Series == value.Passport.Series && user.Passport.Number == value.Passport.Number)
                    {
                        value.Id = user.Id;
                        _userLogic.UpdatePassword(AutoMapperConfig.mapper.Map<User>(value));

                        return RedirectToAction("Login");
                    }
                    else
                    {
                        ModelState.AddModelError("","Invalid passport");
                    }
                }
                else
                {
                    ModelState.AddModelError("","Invalid login");
                }
            }

            return View(value);
        }
    }
}