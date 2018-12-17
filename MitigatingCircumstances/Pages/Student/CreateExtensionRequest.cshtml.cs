using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MitigatingCircumstances.Models;
using MitigatingCircumstances.Models.Enum;
using MitigatingCircumstances.Models.Static;
using MitigatingCircumstances.Repositories.Base;
using MitigatingCircumstances.Services.Interface;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MitigatingCircumstances.Pages.Student
{
    [Authorize(Roles = Roles.Student)]
    public class CreateExtensionRequestModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        public IEnumerable<SelectListItem> AvailableTutors { get; set; }

        public CreateExtensionRequestResult ExtensionRequestResult { get; set; }

        public class CreateExtensionRequestResult
        {
            public bool IsSuccessful { get; set; }

            public int CreatedExtensionRequestId { get; set; }
        }

        public class InputModel
        {
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

        private readonly IExtensionRequestRepository _extensionRequestRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICloudStorageService _cloudStorageService;

        public CreateExtensionRequestModel(IExtensionRequestRepository extensionRequestRepository, 
            UserManager<ApplicationUser> userManager, ICloudStorageService cloudStorageService)
        {
            _extensionRequestRepository = extensionRequestRepository;
            _cloudStorageService = cloudStorageService;
            _userManager = userManager;

            InitializeTutors().Wait();
        }

        private async Task InitializeTutors()
        {
            var tutors = await _userManager.GetUsersInRoleAsync(Roles.Tutor);
            AvailableTutors = tutors.Select(t => new SelectListItem { Value = t.Id, Text = t.Fullname });
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ExtensionRequestResult = new CreateExtensionRequestResult();

            if (ModelState.IsValid)
            {
                var student = await _userManager.GetUserAsync(User);
                var tutor = await _userManager.FindByIdAsync(Input.ChosenTutorId);

                var extensionRequest = new ExtensionRequest
                {
                    Title = Input.Title,
                    Description = Input.Description,
                    Status = ExtensionRequestStatus.Open,
                    StudentCreatedBy = student,
                    TutorAssignedTo = tutor
                };

                if (Input.UploadedFiles != null && Input.UploadedFiles.Any())
                {
                    extensionRequest.UploadedDocuments = UploadFiles(extensionRequest, Input.UploadedFiles);
                }

                _extensionRequestRepository.SaveExtensionRequest(extensionRequest);

                ExtensionRequestResult.IsSuccessful = true;
                ExtensionRequestResult.CreatedExtensionRequestId = extensionRequest.Id;
            }

            return Page();
        }

        public List<UploadedDocument> UploadFiles(ExtensionRequest extensionRequest, List<IFormFile> formFiles)
        {
            var uploadedDocuments = new List<UploadedDocument>();

            foreach (var formFile in formFiles)
            {
                if (formFile.Length > 0)
                {
                    var response = _cloudStorageService.UploadFormFile(formFile);

                    uploadedDocuments.Add(new UploadedDocument
                    {
                        CloudId = response.Id,
                        Bucket = response.Bucket,
                        MediaLink = response.MediaLink,
                        Name = formFile.FileName,
                        ExtensionRequest = extensionRequest,
                        UploadedBy = extensionRequest.StudentCreatedBy
                    });
                }
            }

            return uploadedDocuments;
        }
    }
}