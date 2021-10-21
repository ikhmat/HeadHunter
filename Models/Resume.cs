using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HeadHunter.Models
{
    public class Resume
    {
        public string Id { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Surname")]
        public string Surname { get; set; }
        [Required]
        [Display(Name = "Date of birth")]
        public DateTime BirthDate { get; set; }
        [Display(Name = "Category")]
        public string CategoryId { get; set; }
        [Required]
        [Display(Name = "City of residence")]
        public string CityName { get; set; }
        public string Telegram { get; set; }
        [Required]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Facebook link")]
        public string FacebookLink { get; set; }
        [Display(Name = "Linkedin link")]
        public string LinkedInLink { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        [Required]
        [Display(Name = "Desired wage")]
        public int Wage { get; set; }
        [Required]
        [Display(Name = "Job title")]
        public string JobTitle { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
