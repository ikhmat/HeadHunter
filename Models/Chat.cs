using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeadHunter.Models
{
    public class Chat
    {
        public string Id { get; set; }
        public string ReceiverId { get; set; }
        public User Receiver { get; set; }
        public string SenderId { get; set; }
        public User Sender { get; set; }
        public string ResumeId { get; set; }
        public Resume Resume { get; set; }
        public string VacancyId { get; set; }
        public Vacancy Vacancy { get; set; }
    }
}
