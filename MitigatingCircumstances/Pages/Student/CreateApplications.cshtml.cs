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

namespace MitigatingCircumstances.Pages.Student
{
    [Authorize(Roles = Roles.Student)]
    public class CreateApplicationsModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        public List<SelectListItem> AvailableTutors { get; set; }

        public class InputModel
        {
            [Required]
            public string Title { get; set; }

            [Required]
            [Display(Name = "Mitigating Circumstances")]
            public string Message { get; set; }

            [Display(Name = "Supporting Documents")]
            public List<IFormFile> Files { get; set; }

            [Required]
            [Display(Name = "Assigned Tutor")]
            public string ChosenTutor { get; set; }
        }

        private readonly ISupportTicketRepository _supportTicketRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICloudStorageService _cloudStorageService;

        public CreateApplicationsModel(ISupportTicketRepository supportTicketRepository, 
            UserManager<ApplicationUser> userManager, ICloudStorageService cloudStorageService)
        {
            _supportTicketRepository = supportTicketRepository;
            _cloudStorageService = cloudStorageService;
            _userManager = userManager;

            AvailableTutors = new List<SelectListItem>()
            {
                new SelectListItem { Value = "Gernot", Text = "Gernot Libechen" },
                new SelectListItem { Value = "Paul", Text = "Paul De Vrieze" },
                new SelectListItem { Value = "Tim", Text = "Tim Orman" }
            };
        }

        public void OnGet()
        {

        }

        public void OnPost()
        {
            if (ModelState.IsValid)
            {
                var student = _userManager.GetUserAsync(User).Result;
                var tutor = student;

                var ticket = new SupportTicket
                {
                    Title = Input.Title,
                    Message = Input.Message,
                    Status = TicketStatus.Open,
                    StudentCreatedBy = student,
                    TutorAssignedTo = tutor
                };

                if (Input.Files != null && Input.Files.Any())
                {
                    ticket.UploadedDocuments = UploadFiles(ticket, Input.Files);
                }

                _supportTicketRepository.SaveSupportTicket(ticket);
            }          
        }

        public List<UploadedDocument> UploadFiles(SupportTicket ticket, List<IFormFile> formFiles)
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
                        Ticket = ticket,
                        UploadedBy = ticket.StudentCreatedBy
                    });
                }
            }

            return uploadedDocuments;
        }
    }
}