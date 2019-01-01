using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MitigatingCircumstances.Models;
using MitigatingCircumstances.Models.Static;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MitigatingCircumstances.Pages.Student
{
    [Authorize(Roles = Roles.Student)]
    public class CreateExtensionRequestModel : PageModel
    {
        public IEnumerable<SelectListItem> AvailableTutors { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [BindProperty(Name = "Input.Title")]
            public string Title { get; set; }

            [Required]
            [BindProperty(Name = "Input.Description")]
            public string Description { get; set; }

            [BindProperty(Name = "Input.UploadedFiles")]
            [Display(Name = "Supporting Documents")]
            public List<IFormFile> UploadedFiles { get; set; }

            [Required]
            [BindProperty(Name = "Input.ChosenTutorId")]
            [Display(Name = "Assigned Tutor")]
            public string ChosenTutorId { get; set; }
        }

        private readonly UserManager<ApplicationUser> _userManager;

        public CreateExtensionRequestModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            InitializeTutors().Wait();
        }

        private async Task InitializeTutors()
        {
            var tutors = await _userManager.GetUsersInRoleAsync(Roles.Tutor);
            AvailableTutors = tutors.Select(t => new SelectListItem { Value = t.Id, Text = t.Fullname });
        }
    }
}