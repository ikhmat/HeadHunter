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
        [Display(Name = "Имя")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Фамилия")]
        public string Surname { get; set; }
        [Required]
        [Display(Name = "Дата рождения")]
        public DateTime BirthDate { get; set; }
        [Display(Name = "Категория")]
        public string CategoryId { get; set; }
        [Required]
        [Display(Name = "Город проживания")]
        public string CityName { get; set; }
        public string Telegram { get; set; }
        [Required]
        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Ссылка на Facebook")]
        public string FacebookLink { get; set; }
        [Display(Name = "Ссылка на Linkedin")]
        public string LinkedInLink { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        [Required]
        [Display(Name = "Заработная плата в долларах США")]
        public int Wage { get; set; }
        [Required]
        [Display(Name = "Название должности")]
        public string JobTitle { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool Published { get; set; }
    }
}
