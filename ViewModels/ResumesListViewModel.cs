﻿using HeadHunter.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeadHunter.ViewModels
{
    public class ResumesListViewModel
    {
        public IEnumerable<Resume> Resumes { get; set; }
        public string CategoryId { get; set; }
    }
}
