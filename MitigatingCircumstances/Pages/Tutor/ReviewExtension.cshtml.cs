using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MitigatingCircumstances.Models;
using MitigatingCircumstances.Models.Static;
using MitigatingCircumstances.Repositories.Base;

namespace MitigatingCircumstances.Pages.Tutor
{
    [Authorize(Roles = Roles.Tutor)]
    public class ReviewExtensionModel : PageModel
    {
        private readonly IExtensionRequestRepository _extensionRequestRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public ExtensionRequest ExtensionRequest { get; set; }

        public ReviewExtensionModel(IExtensionRequestRepository extensionRequestRepository,
            UserManager<ApplicationUser> userManager)
        {
            _extensionRequestRepository = extensionRequestRepository;
            _userManager = userManager;
            ExtensionRequest = null;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var tutor = await _userManager.GetUserAsync(User);

            var extensionRequest = _extensionRequestRepository.GetExtensionRequestById(id);
            if (extensionRequest?.TutorAssignedTo?.Id == tutor?.Id)
            {
                ExtensionRequest = extensionRequest;
            }

            return Page();
        }
    }
}