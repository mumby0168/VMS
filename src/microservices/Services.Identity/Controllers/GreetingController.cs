using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Identity.Messages.Commands;
using Services.Identity.Models;
using Services.Identity.Services;

namespace Services.Identity.Controllers
{
    [Route("api/greeting")]
    public class GreetingController : Controller
    {
        private readonly IGreetingSystemService _greetingSystemService;

        public GreetingController(IGreetingSystemService greetingSystemService)
        {
            _greetingSystemService = greetingSystemService;
        }

        [HttpPost("sign-in")]
        public Task<IAuthToken> SignIn([FromBody]SignInGreetingSystem command)
        {
            return _greetingSystemService.SignIn(command.Email, command.Password, command.Code);
        }
    }
}