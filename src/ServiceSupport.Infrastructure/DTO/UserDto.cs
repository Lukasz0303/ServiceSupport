using System;

namespace ServiceSupport.Infrastructure.DTO
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Role { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public PersonDto Person { get; set; }
    }
}