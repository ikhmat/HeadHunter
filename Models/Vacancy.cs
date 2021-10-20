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
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Wage")]
        public int Wage { get; set; }
        public string Description { get; set; }
        public int ExperienceFrom { get; set; }
        public int ExperienceTo { get; set; }
        [Required]
        [Display(Name = "Category")]
        public string CategoryVacancyId { get; set; }
        public DateTime DateOfUpdate { get; set; }
    }
}
