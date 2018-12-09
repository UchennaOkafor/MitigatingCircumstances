using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MitigatingCircumstances.Controllers
{
    public class TutorController : Controller
    {
        [Authorize(Roles = "Tutor")]
        public IActionResult Applications()
        {
            return View();
        }
    }
}