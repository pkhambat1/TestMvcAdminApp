using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TestMvcAdminApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestMvcAdminApp.Repositories;
using Microsoft.Extensions.Logging;

namespace MvcCoreAdminApp.Controllers {

    [AllowAnonymous]
    public class AccountController : Controller {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager) {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }


        #region Register
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null) {
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Register(RegisterUser model, string returnUrl = null) {
        //    if (ModelState.IsValid) {

        //        // Create UserDetails object 
        //        var userDetails = new UserDetails { FirstName = model.FirstName, LastName = model.LastName, Mobile = model.Mobile, CompanyName = model.CompanyName };
        //        // Add UserDetails entry to UserDetails db table
        //        var userID = AdminRepository.CreateUserDetails(userDetails);
        //        // Create Users object with UserID foreign key from UserDetails table
        //        var user = new ApplicationUser { UserID = userID, Email = model.Email, UserName = model.UserName };
        //        // Add AspNetUsers entry to AspNetUsers Db
        //        var result = await _userManager.CreateAsync(user, model.Password);

        //        if (result.Succeeded) {
        //            // Sign in user
        //            await _signInManager.SignInAsync(user, false);
        //            return RedirectToAction("Index", "Home");

        //        } else {
        //            foreach (var error in result.Errors) {
        //                ModelState.AddModelError("", error.Description);
        //            }
        //        }
        //    }

        //    return View(model);
        //}
        #endregion

        #region Login
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null) {
            var loginUser = new LoginUser { ReturnUrl = returnUrl };
            return View(loginUser);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUser model) {
            if (ModelState.IsValid) {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);
                if (result.Succeeded) {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl)) {
                        return Redirect(model.ReturnUrl);
                    } else {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            ModelState.AddModelError("", "Invalid login attempt");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout() {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
        #endregion
    }
}