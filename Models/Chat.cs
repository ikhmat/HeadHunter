using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeadHunter.Models
{
    public class Chat
    {
        public string Id { get; set; }
        public string FirstUserId { get; set; }
        public User FirstUser { get; set; }
        public string SecondUserId { get; set; }
        public User SecondUser { get; set; }
        public string ResumeId { get; set; }
        public Resume Resume { get; set; }
        public string VacancyId { get; set; }
        public Vacancy Vacancy { get; set; }
    }
}
