using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using MitigatingCircumstances.Models;
using MitigatingCircumstances.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using MitigatingCircumstances.Models.Enum;
using System.Linq;
using MitigatingCircumstances.Pages.Student;
using System.ComponentModel.DataAnnotations;
using MitigatingCircumstances.Models.Static;
using MitigatingCircumstances.Repositories.Interface;

namespace MitigatingCircumstances.Controllers.Api
{
    [Authorize]
    [ApiController]
    [Route("api/extension_request/[action]")] 
    public class ExtensionRequestController : ControllerBase
    {
        private readonly IMailService _mailService;
        private readonly IExtensionRequestRepository _extensionRequestRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public ExtensionRequestController(IMailService mailService, ICloudStorageService storageService, 
            IExtensionRequestRepository extensionRequestRepository, UserManager<ApplicationUser> userManager)
        {
            _mailService = mailService;         
            _userManager = userManager;
            _extensionRequestRepository = extensionRequestRepository;
        }

        [HttpPost]
        [Authorize(Roles = Roles.Student)]
        public async Task<IActionResult> Create([FromForm] CreateExtensionRequestModel.InputModel form)
        {
            var student = await _userManager.GetUserAsync(User);
            var tutor = await _userManager.FindByIdAsync(form.ChosenTutorId);

            var extensionRequest = new ExtensionRequest
            {
                Title = form.Title,
                Description = form.Description,
                Status = ExtensionRequestStatus.Open,
                StudentCreatedBy = student,
                TutorAssignedTo = tutor
            };

            if (form.UploadedFiles != null && form.UploadedFiles.Any())
            {
                extensionRequest.UploadedDocuments =
                    _extensionRequestRepository.UploadFilesFor(extensionRequest, form.UploadedFiles);
            }

            _extensionRequestRepository.SaveExtensionRequest(extensionRequest);

            _mailService.SendExtensionCreatedEmailToTeacher(tutor, student, extensionRequest);

            return Created(string.Empty, extensionRequest);
        }

        [HttpPost]
        [Authorize(Roles = Roles.Tutor)]
        public async Task<IActionResult> EditStatus([FromForm] EditStatusRequest input)
        {
            var tutor = await _userManager.GetUserAsync(User);

            var extensionRequest = 
                _extensionRequestRepository.GetExtensionRequestById(input.ExtensionRequestId);

            if (extensionRequest?.TutorAssignedTo?.Id == tutor?.Id)
            {
                _extensionRequestRepository.ChangeExtensionRequestState(extensionRequest, input.NewStatus);
                _mailService.SendExtensionRequestChangeStateEmail(extensionRequest);
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost]
        [Authorize(Roles = Roles.Tutor)]
        public async Task<IActionResult> RequestMoreInformation([FromForm] RequestExtraInfo input)
        {
            var tutor = await _userManager.GetUserAsync(User);

            var extensionRequest =
                _extensionRequestRepository.GetExtensionRequestById(input.ExtensionRequestId);

            if (extensionRequest?.TutorAssignedTo?.Id == tutor?.Id)
            {
                _mailService.SendTeacherRequestMoreInfoEmail(tutor,
                    extensionRequest.StudentCreatedBy, extensionRequest, input.Message);

                return Ok();
            }

            return BadRequest();
        }

        public class EditStatusRequest
        {
            [Required]
            public int ExtensionRequestId { get; set; }

            [Required]
            [Range(1, 3)]
            public ExtensionRequestStatus NewStatus { get; set; }
        }

        public class RequestExtraInfo
        {
            [Required]
            public int ExtensionRequestId { get; set; }

            [Required]
            public string Message { get; set; }
        }
    }
}