using SCB.Surkova.CreditApprovalSystem.BLL.Interfaces;
using SCB.Surkova.CreditApprovalSystem.Entities;
using SCB.Surkova.CreditApprovalSystem.Web.Models;
using SCB.Surkova.CreditApprovalSystem.Web.Models.ScanVMs;
using SCB.Surkova.CreditApprovalSystem.Web.Models.User;
using SCB.Surkova.CreditApprovalSystem.Web.Models.UserVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCB.Surkova.CreditApprovalSystem.Web.Controllers
{

    public class UsersController : Controller
    {
        private readonly IUserLogic _userLogic;

        public UsersController(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        [Authorize(Roles = UserRoles.User)]
        public ActionResult GetProfile()
        {
            var user = _userLogic.GetUserByLogin(User.Identity.Name);
            var viewUser = AutoMapperConfig.mapper.Map<DisplayUserVM>(user);
            return View(viewUser);
        }

        [Authorize(Roles = UserRoles.Admin)]
        public ActionResult GetUsers()
        {
            var model = AutoMapperConfig.mapper.Map<IEnumerable<DisplayUserVM>>(_userLogic.GetUsers());

            return PartialView("GetUsers", model);
        }

        [Authorize(Roles = UserRoles.Admin)]
        public ActionResult Edit(DisplayUserVM model)
        {
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public ActionResult Edit(DisplayUserVM model, string roles)
        {
            try
            {
                var user = _userLogic.GetUserById(AutoMapperConfig.mapper.Map<User>(model).Id);
                _userLogic.AddRole(user, roles);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }

            return RedirectToAction("GetUsers.cshtml");
        }

        [Authorize(Roles = UserRoles.User)]
        public ActionResult EditProfile()
        {
            var user = _userLogic.GetUserByLogin(User.Identity.Name);
            var userView = AutoMapperConfig.mapper.Map<EditUserVM>(user);
            return View(userView);
        }

        [HttpPost]
        public ActionResult EditProfile(EditUserVM value, IEnumerable<HttpPostedFileBase> passportImage, HttpPostedFileBase additionalImage, string types)
        {
            if (ModelState.IsValid)
            {
                if (passportImage.First() != null)
                {
                    value.Passport.Scans = AutoMapperConfig.mapper.Map<List<EditScanVM>>(passportImage);
                }

                if(!string.IsNullOrEmpty(types))
                {
                    value.AdditionalFile = new EditScanVM
                    {
                        Type = types
                    };
                }

                value.AdditionalFile = AutoMapperConfig.mapper.Map<EditScanVM>(additionalImage) ?? value.AdditionalFile;

                try
                {
                    _userLogic.UpdateUser(AutoMapperConfig.mapper.Map<User>(value));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(value);
                }

                return RedirectToAction("GetProfile");
            }

            return View(value);
        }

        [Authorize(Roles = UserRoles.Admin)]
        public ActionResult GetUserBySurname(string surname)
        {
            var model = _userLogic.GetUserBySurname(surname);
            return PartialView("GetUsers", AutoMapperConfig.mapper.Map<IEnumerable<DisplayUserVM>>(model));
        }
    }
}