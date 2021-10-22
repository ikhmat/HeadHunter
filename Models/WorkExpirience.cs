using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HeadHunter.Models
{
    public class WorkExpirience
    {
        public string Id { get; set; }
        public string ResumeId { get; set; }
        [Required(ErrorMessage = "Не заполнено поле компании")]
        [Display(Name = "CompanyName")]
        public string CompanyName { get; set; }
        [Required(ErrorMessage = "Не заполнено поле позиции")]
        [Display(Name = "Position")]
        public string Position { get; set; }
        public string Responsibilities { get; set; }
        [Required(ErrorMessage = "Не заполнено поле начала работы")]
        [Display(Name = "DateOfReceiving")]
        public DateTime DateOfReceiving { get; set; }
        [Required(ErrorMessage = "Не заполнено поле окончания работы")]
        [Display(Name = "DateOfEnd")]
        public DateTime DateOfEnd { get; set; }
    }
}
