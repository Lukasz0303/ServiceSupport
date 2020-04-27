using System.Threading.Tasks;
using ServiceSupport.Infrastructure.Commands;
using ServiceSupport.Infrastructure.Commands.Users;

namespace ServiceSupport.Infrastructure.Handlers.Users
{
    public class ChangeUserPasswordHandler : ICommandHandler<ChangeUserPassword>
    {
        public async Task HandleAsync(ChangeUserPassword command)
        {
            await Task.CompletedTask;
        }
    }
}