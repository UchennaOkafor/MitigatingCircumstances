using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MitigatingCircumstances.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public ActionResult<UserApi> GetAll()
        {
            return new UserApi();
        }

        [HttpGet]
        public ActionResult<UserApi> GetMe()
        {
            return new UserApi();
        }

    }

    public class UserApi
    {
        public string Username { get; set; } = "Jack";

        public string Lastnames { get; set; } = "Mayfair";
    }
}
