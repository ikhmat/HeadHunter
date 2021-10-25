using HeadHunter.Enums;
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
        public IActionResult Publications(string categoryId, SortState sortOrder = SortState.DateDesc)
        {
            PublicationsViewModel rlvm = new PublicationsViewModel()
            {
                CategoryId = categoryId
            };
            ViewBag.DateSort = sortOrder == SortState.DateAsc ? SortState.DateDesc : SortState.DateAsc;
            ViewBag.WageSort = sortOrder == SortState.WageAsc ? SortState.WageDesc : SortState.WageAsc;


            if (User.IsInRole("applicant"))
            {
                var vacancies = _context.Vacancies.Include(r => r.User).Where(r => r.Agreement == true);
                if (!String.IsNullOrEmpty(categoryId))
                {
                    vacancies = vacancies.Where(p => p.CategoryVacancyId.Contains(categoryId));
                }
                switch (sortOrder)
                {
                    case SortState.DateAsc:
                        vacancies = vacancies.OrderBy(s => s.DateOfUpdate);
                        break;
                    case SortState.WageAsc:
                        vacancies = vacancies.OrderBy(s => s.Wage);
                        break;
                    case SortState.WageDesc:
                        vacancies = vacancies.OrderByDescending(s => s.Wage);
                        break;
                    default:
                        vacancies = vacancies.OrderByDescending(s => s.DateOfUpdate);
                        break;
                }
                rlvm.Vacancies = vacancies;

            }
            else
            {
                var resumes = _context.Resumes.Include(r => r.User).Where(r => r.Published == true);
                if (!String.IsNullOrEmpty(categoryId))
                {
                    resumes = resumes.Where(p => p.CategoryId.Contains(categoryId));
                }
                switch (sortOrder)
                {
                    case SortState.DateAsc:
                        resumes = resumes.OrderBy(s => s.UpdateDate);
                        break;
                    case SortState.WageAsc:
                        resumes = resumes.OrderBy(s => s.Wage);
                        break;
                    case SortState.WageDesc:
                        resumes = resumes.OrderByDescending(s => s.Wage);
                        break;
                    default:
                        resumes = resumes.OrderByDescending(s => s.UpdateDate);
                        break;
                }

                rlvm.Resumes = resumes;

            }


            ViewBag.Categories = _context.CategoryVacancies.ToList();
            return View(rlvm);
        }
    }
}
