using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Recipe.Models;
using Recipe.ViewModels;

namespace Recipe.Controllers
{
    public class AccountController : Controller
    {
        public UserManager<UserModel> _userManager { get; set; }
        public SignInManager<UserModel>_signInManager { get;set ;}
        public AccountController(UserManager<UserModel> userManager , SignInManager<UserModel> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = new UserModel {Avatar = model.Email , Email = model.Email};
                var result = await _userManager.CreateAsync(user , model.Password);
                if(result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent:false);
                    return RedirectToAction("Index" , "Home");
                }
                foreach(var err in result.Errors)
                {
                    ModelState.AddModelError(string.Empty  , err.Description);
                }
            }
            return View(model);
        }
        public IActionResult Login(string returnUrl =null)
        {
            ViewData["returnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Login(LoginViewModel model , string returnUrl =null)
        {
            ViewData["returnUrl"] = returnUrl;
            if(ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email , model.Password , model.RememberMe , lockoutOnFailure:false);
                if(result.Succeeded)
                {
                    return RedirectToLocal(returnUrl);
                }
                ModelState.AddModelError(string.Empty,"Invalid Login error !!!!!! " );

            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index" , "Home");
        }


        private IActionResult RedirectToLocal(string returnUrl)
        {
            if(Url.IsLocalUrl(returnUrl))
            {
                return RedirectToAction(returnUrl);
            }
            else{
                return RedirectToAction(nameof(HomeController.Index) , "Home");
            }
        }
        

    }
}