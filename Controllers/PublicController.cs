using HeadHunter.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeadHunter.Controllers
{
    [Authorize]
    public class PublicController : Controller
    {
        private readonly UserManager<User> _userManager;
        private ApplicationContext _context;

        public PublicController(UserManager<User> userManager, ApplicationContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ResumeDetails(string resumeId)
        {
            var resume = _context.Resumes.Find(resumeId);
            ViewBag.Category = _context.CategoryVacancies.Find(resume.CategoryId);
            ViewBag.Work = _context.WorkExpiriences.Where(e => e.ResumeId == resumeId).ToList();
            ViewBag.Education = _context.EducationExpiriences.Where(e => e.ResumeId == resumeId).ToList();
            ViewBag.Courses = _context.CoursesExpiriences.Where(e => e.ResumeId == resumeId).ToList();
            return View(resume);
        }
    }
}
