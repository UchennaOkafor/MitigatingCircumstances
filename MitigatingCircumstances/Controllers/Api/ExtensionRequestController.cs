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

namespace MitigatingCircumstances.Controllers.Api
{
    [Authorize]
    [ApiController]
    //[Produces("application/json")]
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
        public async Task<ActionResult<HttpResponseMessage>> Create()
        {
            var form = Request.Form;
            var student = await _userManager.GetUserAsync(User);
            var tutor = await _userManager.FindByIdAsync(form["Input.ChosenTutorId"]);

            var extensionRequest = new ExtensionRequest
            {
                Title = form["Input.Title"],
                Description = form["Input.Description"],
                Status = ExtensionRequestStatus.Open,
                StudentCreatedBy = student,
                TutorAssignedTo = tutor
            };

            if (form.Files != null && form.Files.Any())
            {
                extensionRequest.UploadedDocuments =
                    _extensionRequestRepository.UploadFilesFor(extensionRequest, form.Files.ToList());
            }

            _extensionRequestRepository.SaveExtensionRequest(extensionRequest);

            //_mailerService.SendTeacherCreatedNotificationEmail(tutor, student, extensionRequest);

            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        [HttpPost]
        public ActionResult<HttpResponseMessage> ChangeState()
        {


            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }
    }
}