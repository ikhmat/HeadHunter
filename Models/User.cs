using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeadHunter.Models
{
    public class User :  IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string LinkImg { get; set; }
        public string CompanyName { get; set; }
    }
}
