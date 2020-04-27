using System;
using ServiceSupport.Infrastructure.DTO;

namespace ServiceSupport.Infrastructure.Services
{
    public interface IJwtHandler
    {
         JwtDto CreateToken(Guid userId, string role);
    }
}