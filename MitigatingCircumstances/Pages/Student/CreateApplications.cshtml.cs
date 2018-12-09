using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MitigatingCircumstances.Models.Static;

namespace MitigatingCircumstances.Pages.Student
{
    [Authorize(Roles = Roles.Student)]
    public class CreateApplicationsModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public string Title { get; set; }

            public string Message { get; set; }

            public IFormFile[] Files { get; set; }
        }

        public void OnGet()
        {

        }
    }
}