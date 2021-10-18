using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HeadHunter.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "EmailOrNickname")]
        public string EmailOrNickname { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Rememember?")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}
