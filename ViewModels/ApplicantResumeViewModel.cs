using HeadHunter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeadHunter.ViewModels
{
    public class ApplicantResumeViewModel
    {
        public Resume Resume { get; set; }
        public IEnumerable<WorkExpirience> Works { get; set; }
        public IEnumerable<EducationExpirience> Educations { get; set; }
        public IEnumerable<CoursesExpirience> Courses { get; set; }
    }
}
