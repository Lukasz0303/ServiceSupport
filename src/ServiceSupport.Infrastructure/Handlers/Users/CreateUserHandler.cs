using System;
using System.Threading.Tasks;
using ServiceSupport.Infrastructure.Commands;
using ServiceSupport.Infrastructure.Commands.Users;
using ServiceSupport.Infrastructure.Services;
using ServiceSupport.Infrastructure.Services.UserGroup;

namespace ServiceSupport.Infrastructure.Handlers.Users
{
    public class CreateUserHandler : ICommandHandler<CreateUser>
    {
        private readonly IUserService _userService;
        
        public CreateUserHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task HandleAsync(CreateUser command)
        {
            await _userService.RegisterAsync(Guid.NewGuid(), command.Email, 
                command.Username, command.Password, command.Role);
        }
    }
}