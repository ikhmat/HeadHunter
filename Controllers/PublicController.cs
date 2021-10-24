﻿using HeadHunter.Models;
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
        public IActionResult Publications(string categoryId)
        {
            PublicationsViewModel rlvm = new PublicationsViewModel()
            {
                CategoryId = categoryId
            };
            if (User.IsInRole("applicant"))
            {
                var vacancies = _context.Vacancies.Include(r => r.User).Where(r => r.Agreement == true);
                if (!String.IsNullOrEmpty(categoryId))
                {
                    vacancies = vacancies.Where(p => p.CategoryVacancyId.Contains(categoryId));
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
                rlvm.Resumes = resumes;
            }
            ViewBag.Categories = _context.CategoryVacancies.ToList();
            return View(rlvm);
        }
    }
}
