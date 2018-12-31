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
            [BindProperty(Name = "Title")]
            [Required]
            [Display(Prompt = "The title of your extraneous circumstance")]
            public string Title { get; set; }

            [Required]
            [Display(Prompt = "A concise description of your extraneous circumstance")]
            public string Description { get; set; }

            [Display(Name = "Supporting Documents")]
            public List<IFormFile> UploadedFiles { get; set; }

            [Required]
            [Display(Name = "Assigned Tutor")]
            public string ChosenTutorId { get; set; }
        }

        private readonly UserManager<ApplicationUser> _userManager;

        public CreateExtensionRequestModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async void OnGetAsync()
        {
            await InitializeTutors();
        }

        private async Task InitializeTutors()
        {
            var tutors = await _userManager.GetUsersInRoleAsync(Roles.Tutor);
            AvailableTutors = tutors.Select(t => new SelectListItem { Value = t.Id, Text = t.Fullname });
        }
    }
}