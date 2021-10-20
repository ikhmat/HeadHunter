using HeadHunter.Models;
using HeadHunter.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
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

        public IActionResult Profile()
        {
            BossesProfileViewModel viewModel = new BossesProfileViewModel
            {
                User = _db.Users.FirstOrDefault(u => u.Id == _userManager.GetUserId(User))
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BossesProfileViewModel model)
        {
            User user = await _db.Users.FirstOrDefaultAsync(u => u.Id == model.User.Id);
            if (ModelState.IsValid)
            {
                string filename;
                if (model.File != null)
                {
                    filename = model.User.UserName + Path.GetExtension(model.File.FileName);
                    System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\avatars\\" + user.LinkImg));
                    user.LinkImg = filename;
                    using (var stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\avatars\\" + filename), FileMode.Create))
                    {
                        await model.File.CopyToAsync(stream);
                    }
                }
                user.Email = model.User.Email;
                user.UserName = model.User.UserName;
                user.Surname = model.User.Surname;
                user.Name = model.User.Name;
                user.CompanyName = model.User.CompanyName;
                await _db.SaveChangesAsync();
            }
            return RedirectToAction("Profile");
        }

        public IActionResult AddVacancy()
        {
            ViewBag.Categories = _db.CategoryVacancies.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult AddVacancy(Vacancy vacancy)
        {
            return RedirectToAction("Profile");
        }

        public async Task<IActionResult> AddCategory(string text)
        {
            CategoryVacancy category = new CategoryVacancy { Id = Guid.NewGuid().ToString(), Name = text };
            _db.CategoryVacancies.Add(category);
            await _db.SaveChangesAsync();
            return Json(category);
        }
    }
}
