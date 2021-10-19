using HeadHunter.Models;
using HeadHunter.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeadHunter.Controllers
{
    public class BossesController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private ApplicationContext _db;

        public BossesController(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Profile()
        {
            BossesProfileViewModel viewModel = new BossesProfileViewModel
            {
                User = _userManager.Users.FirstOrDefault(u => u.Id == _userManager.GetUserId(User))
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(BossesProfileViewModel viewmodel)
        {
            User user = _db.Users.FirstOrDefault(u => u.UserName == viewmodel.User.UserName);
            return RedirectToAction("Profile");
        }

    }
}
