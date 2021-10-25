using HeadHunter.Models;
using HeadHunter.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "applicant")]
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
        public IActionResult ApplicantProfile()
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
            ViewBag.CurrentUserId = _userManager.GetUserId(User);
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
                return RedirectToAction("ApplicantProfile");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult AddResume()
        {
            ViewBag.Categories = _context.CategoryVacancies.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult AddResume
            (Resume resume, IEnumerable<WorkExpirience> works, IEnumerable<EducationExpirience> educations,
            IEnumerable<CoursesExpirience> courses)
        {
            if (!ModelState.IsValid)
            {
                var errorModel =
                        from x in ModelState.Keys
                        where ModelState[x].Errors.Count > 0
                        select new
                        {
                            key = x.Substring(x.IndexOf(".") + 1),
                            errors = ModelState[x].Errors.Select(y => y.ErrorMessage).ToArray()
                        };
                return Json( new { success = false, data = errorModel });
            }
            resume.Id = Guid.NewGuid().ToString();
            resume.UpdateDate = DateTime.Now;
            resume.UserId = _userManager.GetUserId(User);
            resume.Published = true;
            _context.Resumes.Add(resume);
            foreach (var item in works)
            {
                WorkExpirience work = new WorkExpirience
                {
                    Id = Guid.NewGuid().ToString(),
                    ResumeId = resume.Id,
                    CompanyName = item.CompanyName,
                    Position = item.Position,
                    DateOfReceiving = item.DateOfReceiving,
                    DateOfEnd = item.DateOfEnd
                };
                _context.WorkExpiriences.Add(work);
            }
            foreach (var item in educations)
            {
                EducationExpirience education = new EducationExpirience
                {
                    Id = Guid.NewGuid().ToString(),
                    ResumeId = resume.Id,
                    InstitutionName = item.InstitutionName,
                    Speciality = item.Speciality,
                    DateOfReceiving = item.DateOfReceiving,
                    DateOfEnd = item.DateOfEnd
                };
                _context.EducationExpiriences.Add(education);
            }
            foreach (var item in courses)
            {
                CoursesExpirience course = new CoursesExpirience
                {
                    Id = Guid.NewGuid().ToString(),
                    ResumeId = resume.Id,
                    CompanyName = item.CompanyName,
                    Speciality = item.Speciality,
                    DateOfReceiving = item.DateOfReceiving,
                    DateOfEnd = item.DateOfEnd
                };
                _context.CoursesExpiriences.Add(course);
            }
            _context.SaveChanges();
            return Json(new { success = true, redirectToUrl = Url.Action("ApplicantProfile", "Applicant") });
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
            ViewBag.Categories = _context.CategoryVacancies.ToList();
            ViewBag.Works = _context.WorkExpiriences.Where(w => w.ResumeId == resumeId).ToList();
            ViewBag.Educations = _context.EducationExpiriences.Where(w => w.ResumeId == resumeId).ToList();
            ViewBag.Courses = _context.CoursesExpiriences.Where(w => w.ResumeId == resumeId).ToList();
            return View(resume);
        }
        [HttpPost]
        public IActionResult EditResume
            (Resume resume, IEnumerable<WorkExpirience> works, IEnumerable<EducationExpirience> educations,
            IEnumerable<CoursesExpirience> courses)
        {
            if (!ModelState.IsValid)
            {
                var errorModel =
                        from x in ModelState.Keys
                        where ModelState[x].Errors.Count > 0
                        select new
                        {
                            key = x.Substring(x.IndexOf(".") + 1),
                            errors = ModelState[x].Errors.Select(y => y.ErrorMessage).ToArray()
                        };
                return Json(new { success = false, data = errorModel });
            }
            resume.UpdateDate = DateTime.Now;
            _context.Resumes.Update(resume);

            // Часть редактирования модулей

            List<WorkExpirience> oldWorks = _context.WorkExpiriences.Where(e => e.ResumeId == resume.Id).ToList();
            List<EducationExpirience> oldEducations = _context.EducationExpiriences.Where(e => e.ResumeId == resume.Id).ToList();
            List<CoursesExpirience> oldCources = _context.CoursesExpiriences.Where(e => e.ResumeId == resume.Id).ToList();

            foreach (var item in works)
            {
                if (item.Id == null)
                {
                    WorkExpirience work = new WorkExpirience
                    {
                        Id = Guid.NewGuid().ToString(),
                        ResumeId = resume.Id,
                        CompanyName = item.CompanyName,
                        Position = item.Position,
                        DateOfReceiving = item.DateOfReceiving,
                        DateOfEnd = item.DateOfEnd
                    };
                    _context.WorkExpiriences.Add(work);
                }
                else
                {
                    oldWorks.Remove(oldWorks.FirstOrDefault(w => w.Id == item.Id));
                }
            }
            foreach (var item in educations)
            {
                if (item.Id == null)
                {
                    EducationExpirience education = new EducationExpirience
                    {
                        Id = Guid.NewGuid().ToString(),
                        ResumeId = resume.Id,
                        InstitutionName = item.InstitutionName,
                        Speciality = item.Speciality,
                        DateOfReceiving = item.DateOfReceiving,
                        DateOfEnd = item.DateOfEnd
                    };
                    _context.EducationExpiriences.Add(education);
                }
                else
                {
                    oldEducations.Remove(oldEducations.FirstOrDefault(e => e.Id == item.Id));
                }
            }
            foreach (var item in courses)
            {
                if (item.Id == null)
                {
                    CoursesExpirience course = new CoursesExpirience
                    {
                        Id = Guid.NewGuid().ToString(),
                        ResumeId = resume.Id,
                        CompanyName = item.CompanyName,
                        Speciality = item.Speciality,
                        DateOfReceiving = item.DateOfReceiving,
                        DateOfEnd = item.DateOfEnd
                    };
                    _context.CoursesExpiriences.Add(course);
                }
                else {
                    oldCources.Remove(oldCources.FirstOrDefault(c => c.Id == item.Id));
                }
            }
            foreach (var item in oldWorks) _context.WorkExpiriences.Remove(item);
            foreach (var item in oldEducations) _context.EducationExpiriences.Remove(item);
            foreach (var item in oldCources) _context.CoursesExpiriences.Remove(item);


            _context.SaveChanges();
            return Json(new { success = true, redirectToUrl = Url.Action("ApplicantProfile", "Applicant") });
        }
    }
}
