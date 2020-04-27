using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using ServiceSupport.Infrastructure.Commands;
using ServiceSupport.Infrastructure.Commands.Accounts;
using ServiceSupport.Infrastructure.Extensions;
using ServiceSupport.Infrastructure.Services;

namespace ServiceSupport.Infrastructure.Handlers.Accounts
{
    public class LoginHandler : ICommandHandler<Login>
    {
        private readonly IHandler _handler;
        private readonly IUserService _userService;
        private readonly IJwtHandler _jwtHandler;
        private readonly IMemoryCache _cache;

        public LoginHandler(IHandler handler, IUserService userService,
                            IJwtHandler jwtHandler, IMemoryCache cache)
        {
            _handler = handler;
            _userService = userService;
            _jwtHandler = jwtHandler;
            _cache = cache;
        }

        public async Task HandleAsync(Login command)
        => await _handler
            .Run(async () => await _userService.LoginAsync(command.Email, command.Password))
            .Next()
            .Run(async () =>
	        {
	            var user = await _userService.GetAsync(command.Email);
	            var jwt = _jwtHandler.CreateToken(user.Id, user.Role);
	            _cache.SetJwt(command.TokenId, jwt);
	        })
            .Next()
            .ExecuteAllAsync();
    }
}