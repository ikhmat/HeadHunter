using HeadHunter.Models;
using HeadHunter.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "boss")]
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
        [HttpGet]
        public IActionResult Profile()
        {
            User user = _db.Users.FirstOrDefault(u => u.Id == _userManager.GetUserId(User));
            BossesProfileViewModel viewModel = new BossesProfileViewModel
            {
                UserId = user.Id,
                Email = user.Email,
                Nickname = user.UserName,
                Name = user.Name,
                Surname = user.Surname,
                PhoneNumber = user.PhoneNumber,
                CompanyName = user.CompanyName,
                LinkImg = user.LinkImg,
                Vacancies = _db.Vacancies.Where(v => v.UserId == user.Id).OrderByDescending(v => v.DateOfUpdate).ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BossesProfileViewModel model)
        {
            User user = await _db.Users.FirstOrDefaultAsync(u => u.Id == model.UserId);
            if (ModelState.IsValid)
            {
                string filename;
                if (model.File != null)
                {
                    filename = model.Nickname + Path.GetExtension(model.File.FileName);
                    System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\avatars\\" + user.LinkImg));
                    user.LinkImg = filename;
                    using (var stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\avatars\\" + filename), FileMode.Create))
                    {
                        await model.File.CopyToAsync(stream);
                    }
                }
                user.Email = model.Email;
                user.UserName = model.Nickname;
                user.Surname = model.Surname;
                user.Name = model.Name;
                user.CompanyName = model.CompanyName;
                user.PhoneNumber = model.PhoneNumber;
                await _db.SaveChangesAsync();
            return RedirectToAction("Profile");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult AddVacancy()
        {
            ViewBag.Categories = _db.CategoryVacancies.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddVacancy(Vacancy vacancy)
        {
            if (vacancy.ExperienceFrom > vacancy.ExperienceTo && vacancy.ExperienceTo != 0)
            {
                ModelState.AddModelError("ExperienceTo", "Опыт работы ДО не может быть меньше ОТ");
            }
            if (ModelState.IsValid)
            {
                vacancy.Id = Guid.NewGuid().ToString();
                vacancy.DateOfUpdate = DateTime.Now;
                vacancy.UserId = _userManager.GetUserId(User);
                vacancy.Agreement = true;
                _db.Vacancies.Add(vacancy);
                await _db.SaveChangesAsync();
                return RedirectToAction("Profile");
            }
            ViewBag.Categories = _db.CategoryVacancies.ToList();
            return View(vacancy);
            
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(string text)
        {
            CategoryVacancy category = new CategoryVacancy { Id = Guid.NewGuid().ToString(), Name = text };
            _db.CategoryVacancies.Add(category);
            await _db.SaveChangesAsync();
            return Json(category);
        }

        public async Task<IActionResult> UpdateVacancy(string id)
        {
            Vacancy vacancy = _db.Vacancies.Find(id);
            vacancy.DateOfUpdate = DateTime.Now;
            _db.Vacancies.Update(vacancy);
            await _db.SaveChangesAsync();
            return Json(vacancy);
        }

        public async Task<IActionResult> Disagreement(string id)
        {
            Vacancy vacancy = _db.Vacancies.Find(id);
            vacancy.Agreement = !vacancy.Agreement;
            _db.Vacancies.Update(vacancy);
            await _db.SaveChangesAsync();
            return Json(vacancy);
        }

        public IActionResult EditVacancy(string id)
        {
            Vacancy vacancy = _db.Vacancies.Find(id);
            ViewBag.Categories = _db.CategoryVacancies.ToList();
            return View(vacancy);
        }

        [HttpPost]
        public async Task<IActionResult> EditVacancy(Vacancy vacancy)
        {
            if (vacancy.ExperienceFrom > vacancy.ExperienceTo && vacancy.ExperienceTo != 0)
            {
                ModelState.AddModelError("ExperienceTo", "Опыт работы ДО не может быть меньше ОТ");
            }
            if (ModelState.IsValid)
            {
                vacancy.UserId = _userManager.GetUserId(User);
                vacancy.DateOfUpdate = DateTime.Now;
                _db.Update(vacancy);
                await _db.SaveChangesAsync();
                return RedirectToAction("Profile");
            }
            ViewBag.Categories = _db.CategoryVacancies.ToList();
            return View(vacancy);
        }

    }
}
