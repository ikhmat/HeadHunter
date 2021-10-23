using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HeadHunter.Models
{
    public class Vacancy
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public bool Agreement { get; set; }
        [Required(ErrorMessage = "Не заполнено поле названия")]
        [Display(Name = "Name")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Длина имени должна быть от 5 до 100 символов")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Не заполнено поле заработной платы")]
        [Display(Name = "Wage")]
        [Range(1, 100000)]
        public int Wage { get; set; }
        public string Description { get; set; }
        [Range(0, 50)]
        public int ExperienceFrom { get; set; }
        [Range(0, 50)]
        public int ExperienceTo { get; set; }
        [Required(ErrorMessage = "Не выбрана категория")]
        [Display(Name = "Category")]
        public string CategoryVacancyId { get; set; }
        public DateTime DateOfUpdate { get; set; }
    }
}
