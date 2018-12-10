using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MitigatingCircumstances.Models;
using MitigatingCircumstances.Models.Static;
using MitigatingCircumstances.Repositories.Base;

namespace MitigatingCircumstances.Pages.Student
{
    [Authorize(Roles = Roles.Student)]
    public class ApplicationsModel : PageModel
    {
        private readonly ISupportTicketRepository _supportTicketRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public IEnumerable<SupportTicket> Tickets { get; private set; }

        public ApplicationsModel(ISupportTicketRepository supportTicketRepository,
            UserManager<ApplicationUser> userManager)
        {
            _supportTicketRepository = supportTicketRepository;
            _userManager = userManager;
        }

        public void OnGet()
        {
            var currentStudent = _userManager.GetUserAsync(User).Result;
            Tickets = _supportTicketRepository.GetAllTicketsForStudent(currentStudent.Id);
        }
    }
}