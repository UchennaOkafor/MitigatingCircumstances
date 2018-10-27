using Google.Cloud.Datastore.V1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MitigatingCircumstances.Repositories;

namespace MitigatingCircumstances.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger _logger;
        private readonly ITicketRepository _requestRepository;

        public HomeController(ILogger<HomeController> logger, ITicketRepository requestRepository)
        {
            _logger = logger;
            _requestRepository = requestRepository;
        }

        public IActionResult Index()
        {
            // Sends a message to configured loggers, including the Stackdriver logger.
            // The Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker logger will log all controller actions with
            // log level information. This log is for additional information.
            _logger.LogInformation("Home page hit!");

            if (true)
            {
                var r = true;
            }

            _requestRepository.CreateMitigatingRequest();
            //var entity = _requestRepository.GetStudentRequest();
            //var log = _requestRepository.GetLog(entity["student"].KeyValue);

            return View();
        }
    }
}
