using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeadHunter.Models
{
    public class CoursesExpirience
    {
        public string Id { get; set; }
        public string ResumeId { get; set; }
        public string CompanyName { get; set; }
        public string Speciality { get; set; }
        public DateTime DateOfReceiving { get; set; }
        public DateTime DateOfEnd { get; set; }
    }
}
