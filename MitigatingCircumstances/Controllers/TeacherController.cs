using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MitigatingCircumstances.Controllers
{
    public class TeacherController : Controller
    {
        [Authorize(Roles = "Teacher")]
        public IActionResult Applications()
        {
            return View();
        }
    }
}