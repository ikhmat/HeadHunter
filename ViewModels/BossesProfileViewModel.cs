using HeadHunter.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HeadHunter.ViewModels
{
    public class BossesProfileViewModel
    {
        [Required(ErrorMessage = "Не заполнено поле почты")]
        [Display(Name = "Email")]
        [Remote(action: "CheckEmailAuthorize", controller: "Account", ErrorMessage = "Эта почта уже занята")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Не заполнено поле никнейма")]
        [Display(Name = "Nickname")]
        [Remote(action: "CheckNickNameAuthorize", controller: "Account", ErrorMessage = "Это имя пользователя уже занято")]
        public string Nickname { get; set; }
        [Required(ErrorMessage = "Не заполнено поле имени")]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Не заполнено поле фамилии")]
        [Display(Name = "Surname")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Не заполнено поле компании")]
        [Display(Name = "CompanyName")]
        public string CompanyName { get; set; }
        [Required(ErrorMessage = "Не заполнено поле номера телефона")]
        [Display(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }

        public string UserId { get; set; }
        public string LinkImg { get; set; }
        
        public IFormFile File { get; set; }
        public List<Vacancy> Vacancies { get; set; }
    }
}
