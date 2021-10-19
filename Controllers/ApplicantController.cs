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
                    _context.SaveChanges();
                }
            }
            return View(user);
        }
    }
}
