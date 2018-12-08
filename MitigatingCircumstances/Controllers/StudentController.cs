using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MitigatingCircumstances.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentController : Controller
    {
        public IActionResult Applications()
        {
            return View();
        }

        public IActionResult CreateApplication()
        {
            return View();
        }
    }

    class StudentRequestForm
    {
        public string Title { get; set; }

        public string Message { get; set; }

        public IFormFile[] Files { get; set; }
    }
}