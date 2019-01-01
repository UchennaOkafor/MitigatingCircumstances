using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using MitigatingCircumstances.Models;
using MitigatingCircumstances.Models.Enum;
using MitigatingCircumstances.Models.Static;
using MitigatingCircumstances.Repositories.Base;
using MitigatingCircumstances.Services.Interface;

namespace MitigatingCircumstances.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]/[action]")]   
    public class MailController : ControllerBase
    {
        private readonly IMailService _mailService;
        private readonly IExtensionRequestRepository _extensionRequestRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public MailController(IMailService mailService, ICloudStorageService storageService, 
            IExtensionRequestRepository extensionRequestRepository, UserManager<ApplicationUser> userManager)
        {
            _mailService = mailService;         
            _userManager = userManager;
            _extensionRequestRepository = extensionRequestRepository;
        }

        [HttpPost]
        public async Task<ActionResult<HttpResponseMessage>> Parse()
        {
            var email = ParseInboundEmail();
            var student = await _userManager.FindByEmailAsync(email.Envelope.From);
            var teachers = await _userManager.GetUsersInRoleAsync(Roles.Tutor);
            var selectedTeacher = teachers.OrderBy(g => Guid.NewGuid()).ElementAt(0);

            if (student == null)
            {
                _mailService.SendInboundEmailDoesntExistEmail(email.Envelope.From, email.SendersName, email.Subject);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                var extensionRequest = new ExtensionRequest
                {
                    Description = email.Text,
                    Title = email.Subject,
                    Status = ExtensionRequestStatus.Open,
                    StudentCreatedBy = student,
                    TutorAssignedTo = selectedTeacher,
                };

                if (Request.Form?.Files?.Count > 0)
                {
                    extensionRequest.UploadedDocuments =
                        _extensionRequestRepository.UploadFilesFor(extensionRequest, Request.Form.Files.ToList());
                }

                _extensionRequestRepository.SaveExtensionRequest(extensionRequest);

                _mailService.SendInboundEmailExtensionCreated(email.Envelope.From, email.SendersName, extensionRequest);

                _mailService.SendTeacherCreatedNotificationEmail(extensionRequest.TutorAssignedTo, 
                    extensionRequest.StudentCreatedBy, extensionRequest);

                return new HttpResponseMessage(HttpStatusCode.Created);
            }
        }

        private InboundEmail ParseInboundEmail()
        {
            return new InboundEmail
            {
                Dkim = Request.Form["dkim"].FirstOrDefault(),
                To = Request.Form["to"].FirstOrDefault(),
                Html = Request.Form["html"].FirstOrDefault(),
                From = Request.Form["from"].FirstOrDefault(),
                Text = Request.Form["text"].FirstOrDefault(),
                SenderIp = Request.Form["sender_ip"].FirstOrDefault(),
                Envelope = JsonConvert.DeserializeObject<Envelope>(Request.Form["envelope"].FirstOrDefault()),
                Attachments = int.Parse(Request.Form["attachments"].FirstOrDefault()),
                Subject = Request.Form["subject"].FirstOrDefault(),
                Charsets = Request.Form["charsets"].FirstOrDefault(),
                Spf = Request.Form["spf"].FirstOrDefault()
            };
        }
    }
}