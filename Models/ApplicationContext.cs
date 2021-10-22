using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeadHunter.Models
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<Resume> Resumes { get; set; }
        public DbSet<WorkExpirience> WorkExpiriences { get; set; }
        public DbSet<EducationExpirience> EducationExpiriences { get; set; }
        public DbSet<CoursesExpirience> CoursesExpiriences { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Vacancy> Vacancies { get; set; }
        public DbSet<CategoryVacancy> CategoryVacancies { get; set; }


        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
    }
}
