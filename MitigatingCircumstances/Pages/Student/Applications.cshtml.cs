using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MitigatingCircumstances.Models;
using MitigatingCircumstances.Models.Static;
using MitigatingCircumstances.Repositories.Interface;

namespace MitigatingCircumstances.Pages.Student
{
    [Authorize(Roles = Roles.Student)]
    public class ApplicationsModel : PageModel
    {
        private readonly IExtensionRequestRepository _extensionRequestRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public IEnumerable<ExtensionRequest> ExtensionRequests { get; private set; }

        public ApplicationsModel(IExtensionRequestRepository extensionRequestRepository,
            UserManager<ApplicationUser> userManager)
        {
            _extensionRequestRepository = extensionRequestRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var currentStudent = await _userManager.GetUserAsync(User);
            ExtensionRequests = _extensionRequestRepository
                        .GetExtensionRequestsCreatedBy(currentStudent.Id);

            return Page();
        }
    }
}