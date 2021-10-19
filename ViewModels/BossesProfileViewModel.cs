using HeadHunter.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeadHunter.ViewModels
{
    public class BossesProfileViewModel
    {
        public User User { get; set; }
        public IFormFile File { get; set; }
    }
}
