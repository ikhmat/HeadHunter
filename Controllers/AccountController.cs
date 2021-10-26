using HeadHunter.Models;
using HeadHunter.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HeadHunter.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (model.Role == "boss") {
                if (string.IsNullOrEmpty(model.CompanyName))
                {
                    ModelState.AddModelError("CompanyName", "Не заполнено поле компании");
                }
            }
            if (ModelState.IsValid)
            {
                string filename = model.File == null ? "no-avatar.png" : model.Nickname + Path.GetExtension(model.File.FileName);

                User user = new User
                {
                    Email = model.Email,
                    UserName = model.Nickname,
                    Surname = model.Surname,
                    Name = model.Name,
                    LinkImg = filename,
                    PhoneNumber = model.PhoneNumber,
                    CompanyName = model.CompanyName
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    if (filename != "no-avatar.png")
                    {
                        using (var stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\avatars\\" + filename), FileMode.Create))
                        {
                            await model.File.CopyToAsync(stream);
                        }
                    }
                    await _userManager.AddToRoleAsync(user, model.Role);
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByEmailAsync(model.EmailOrNickname);
                user = user == null ? await _userManager.FindByNameAsync(model.EmailOrNickname) : user;

                var result =
                    await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {   
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public bool CheckNickName(string nickName)
        {
            return !_userManager.Users.Any(b => b.UserName == nickName);
        }
        public bool CheckEmail(string email)
        {
            return !_userManager.Users.Any(b => b.Email == email);
        }
        public bool CheckNickNameAuthorize(string nickName)
        {
            User user = _userManager.Users.FirstOrDefault(u => u.Id == _userManager.GetUserId(User));
            if (user.UserName == nickName) return true;
            return !_userManager.Users.Any(b => b.UserName == nickName);
        }
        public bool CheckEmailAuthorize(string email)
        {
            User user = _userManager.Users.FirstOrDefault(u => u.Id == _userManager.GetUserId(User));
            if (user.Email == email) return true;
            return !_userManager.Users.Any(b => b.Email == email);
        }
    }
}
