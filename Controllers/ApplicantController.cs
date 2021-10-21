using HeadHunter.Models;
using HeadHunter.ViewModels;
using Microsoft.AspNetCore.Http;
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
    public class ApplicantController : Controller
    {
        private readonly UserManager<User> _userManager;
        private ApplicationContext _context;

        public ApplicantController(UserManager<User> userManager, ApplicationContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Profile()
        {
            User user = _context.Users.FirstOrDefault(u => u.Id == _userManager.GetUserId(User));
            ApplicantProfileViewModel viewModel = new ApplicantProfileViewModel
            {
                UserId = user.Id,
                Email = user.Email,
                Nickname = user.UserName,
                Name = user.Name,
                Surname = user.Surname,
                PhoneNumber = user.PhoneNumber,
                LinkImg = user.LinkImg,
                Resumes = _context.Resumes.Where(v => v.UserId == user.Id).OrderByDescending(v => v.UpdateDate).ToList()
            };
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ApplicantProfileViewModel model)
        {
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Id == model.UserId);
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
                user.PhoneNumber = model.PhoneNumber;
                await _context.SaveChangesAsync();
                return RedirectToAction("Profile");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult AddResume()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddResume(Resume formResume)
        {
            if (ModelState.IsValid)
            {
                formResume.Id = Guid.NewGuid().ToString();
                formResume.UpdateDate = DateTime.Now;
                formResume.UserId = _userManager.GetUserId(User);
                _context.Resumes.Add(formResume);
                await _context.SaveChangesAsync();
                return RedirectToAction("Profile");
            }
            return View(formResume);
        }
        public async Task<IActionResult> UpdateResume(string id)
        {
            Resume resume = _context.Resumes.Find(id);
            resume.UpdateDate = DateTime.Now;
            _context.Resumes.Update(resume);
            await _context.SaveChangesAsync();
            return Json(resume);
        }

        public async Task<IActionResult> Disagreement(string id)
        {
            Resume resume = _context.Resumes.Find(id);
            resume.Published = !resume.Published;
            _context.Resumes.Update(resume);
            await _context.SaveChangesAsync();
            return Json(resume);
        }
        [HttpGet]
        public IActionResult EditResume(string resumeId)
        {
            var resume = _context.Resumes.FirstOrDefault(r => r.Id == resumeId);
            return View(resume);
        }
        [HttpPost]
        public async Task<IActionResult> EditResume(Resume formResume)
        {
            if (ModelState.IsValid)
            {
                formResume.UpdateDate = DateTime.Now;
                _context.Update(formResume);
                await _context.SaveChangesAsync();
                return RedirectToAction("Profile");
            }
            return View(formResume);
        }
    }
}
