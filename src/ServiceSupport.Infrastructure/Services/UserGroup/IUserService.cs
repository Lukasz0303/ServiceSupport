using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ServiceSupport.Infrastructure.DTO;

namespace ServiceSupport.Infrastructure.Services.UserGroup
{
    public interface IUserService : IService
    {
        Task<UserDto> GetAsync(Guid id);
        Task<UserDto> GetAsync(string email);
        Task<IEnumerable<UserDto>> BrowseAsync();
        Task RegisterAsync(Guid userId, string email, 
            string username, string password, string role);
        Task LoginAsync(string email, string password);
    }
}