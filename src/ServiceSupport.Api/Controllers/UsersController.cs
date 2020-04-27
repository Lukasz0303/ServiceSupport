using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceSupport.Infrastructure.Commands;
using ServiceSupport.Infrastructure.Commands.Users;
using ServiceSupport.Infrastructure.Services;

namespace ServiceSupport.Api.Controllers
{
    public class UsersController : ApiControllerBase
    {
        private readonly IUserService _userService;
        
        public UsersController(IUserService userService, 
            ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
            _userService = userService;
        }
        
        public async Task<IActionResult> Get()
        {
            var users = await _userService.BrowseAsync();

            return Json(users);
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> Get(string email)
        {
            var user = await _userService.GetAsync(email);
            if(user == null)
            {
                return NotFound();
            }

            return Json(user);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateUser command)
        {
            await DispatchAsync(command);

            return Created($"users/{command.Email}", null);
        }
    }
}
