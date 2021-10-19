using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HeadHunter.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Nickname")]
        [Remote(action: "CheckNickName", controller: "Account", ErrorMessage = "Это имя пользователя уже занято")]
        public string Nickname { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Required]
        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; }

        public IFormFile File { get; set; }

        [Required]
        [Display(Name = "Role")]
        public string Role { get; set; }

        [Display(Name = "CompanyName")]
        public string CompanyName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Password mismatch!")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm the password")]
        public string PasswordConfirm { get; set; }
    }
}
