using System;
using ServiceSupport.Infrastructure.DTO;

namespace ServiceSupport.Infrastructure.Services.UserGroup
{
    public interface IJwtHandler
    {
         JwtDto CreateToken(Guid userId, string role);
    }
}