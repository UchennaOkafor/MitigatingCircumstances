using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MitigatingCircumstances.Models.Static;

namespace MitigatingCircumstances.Pages.Student
{
    [Authorize(Roles = Roles.Student)]
    public class ApplicationsModel : PageModel
    {
        public void OnGet()
        {

        }
    }
}