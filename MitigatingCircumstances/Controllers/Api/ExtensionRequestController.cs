using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using MitigatingCircumstances.Models;
using MitigatingCircumstances.Repositories.Base;
using MitigatingCircumstances.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using MitigatingCircumstances.Models.Enum;
using System.Linq;
using MitigatingCircumstances.Pages.Student;

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

            _mailService.SendTeacherCreatedNotificationEmail(tutor, student, extensionRequest);

            return Created(string.Empty, extensionRequest);
        }

        [HttpPost]
        public ActionResult<HttpResponseMessage> ChangeState()
        {


            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }
    }
}