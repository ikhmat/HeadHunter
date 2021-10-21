using HeadHunter.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
            var user = _context.Users.FirstOrDefault(u => u.Id == _userManager.GetUserId(User));
            var resumes = _context.Resumes.Where(r => r.UserId == _userManager.GetUserId(User));
            ViewBag.Resumes = resumes;
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(User formUser, IFormFile file)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == formUser.Id);
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\avatars\\" + user.LinkImg));
                    string filename = user.UserName + Path.GetExtension(file.FileName);
                    using (var stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\avatars\\" + filename), FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    user.LinkImg = filename;
                }
                user.Email = formUser.Email;
                user.UserName = formUser.UserName;
                user.Surname = formUser.Surname;
                user.Name = formUser.Name;
                user.PhoneNumber = formUser.PhoneNumber;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Profile");
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
    }
}
