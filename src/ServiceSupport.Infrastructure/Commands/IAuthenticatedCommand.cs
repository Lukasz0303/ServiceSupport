using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceSupport.Infrastructure.Commands
{
    public interface IAuthenticatedCommand
    {
        Guid UserId { get; set; }
    }
}
