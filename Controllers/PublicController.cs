using HeadHunter.Models;
using HeadHunter.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public IActionResult VacancyDetails(string vacancyId)
        {
            Vacancy vacancy = _context.Vacancies.Include(v => v.User).FirstOrDefault(v => v.Id == vacancyId);
            ViewBag.CategoryName = _context.CategoryVacancies.Find(vacancy.CategoryVacancyId).Name;
            return View(vacancy);
        }
        public IActionResult Publications(string searchString, string categoryId)
        {
            PublicationsViewModel rlvm = new PublicationsViewModel()
            {
                CategoryId = categoryId
            };
            if (User.IsInRole("applicant"))
            {
                var vacancies = _context.Vacancies.Include(r => r.User).Where(r => r.Agreement == true);
                if (!String.IsNullOrEmpty(searchString))
                {
                    searchString = searchString.ToLower();
                    vacancies = vacancies.Where(v => v.Name.ToLower().Contains(searchString));
                }
                if (!String.IsNullOrEmpty(categoryId))
                {
                    vacancies = vacancies.Where(p => p.CategoryVacancyId.Contains(categoryId));
                }
                rlvm.Vacancies = vacancies;
            }
            else
            {
                var resumes = _context.Resumes.Include(r => r.User).Where(r => r.Published == true);
                if (!String.IsNullOrEmpty(searchString))
                {
                    searchString = searchString.ToLower();
                    resumes = resumes.Where(v => v.JobTitle.ToLower().Contains(searchString));
                }
                if (!String.IsNullOrEmpty(categoryId))
                {
                    resumes = resumes.Where(p => p.CategoryId.Contains(categoryId));
                }
                rlvm.Resumes = resumes;
            }
            ViewBag.Categories = _context.CategoryVacancies.ToList();
            return View(rlvm);
        }
        public IActionResult BossesProfile(string userId)
        {
            User user = _context.Users.FirstOrDefault(u => u.Id == userId);
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
                Vacancies = _context.Vacancies.Where(v => v.UserId == user.Id).Where(v => v.Agreement == true).OrderByDescending(v => v.DateOfUpdate).ToList()
            };
            ViewBag.CurrentUserId = _userManager.GetUserId(User);
            return View(viewModel);
        }
        public IActionResult ApplicantProfile(string userId)
        {
            User user = _context.Users.FirstOrDefault(u => u.Id == userId);
            ApplicantProfileViewModel viewModel = new ApplicantProfileViewModel
            {
                UserId = user.Id,
                Email = user.Email,
                Nickname = user.UserName,
                Name = user.Name,
                Surname = user.Surname,
                PhoneNumber = user.PhoneNumber,
                LinkImg = user.LinkImg,
                Resumes = _context.Resumes.Where(v => v.UserId == user.Id).Where(r => r.Published == true).OrderByDescending(v => v.UpdateDate).ToList()
            };
            ViewBag.CurrentUserId = _userManager.GetUserId(User);
            return View(viewModel);
        }

    }
}
