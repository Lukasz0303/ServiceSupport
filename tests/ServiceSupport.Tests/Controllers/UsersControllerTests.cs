using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using ServiceSupport.Api;
using ServiceSupport.Infrastructure.Commands.Users;
using ServiceSupport.Infrastructure.DTO;
using Xunit;

namespace ServiceSupport.Tests.EndToEnd.Controllers
{
    public class UsersControllerTests : ControllerTestsBase
    {
        [Fact]
        public async Task Given_invalid_email_user_should_not_exist()
        {
            var email = "user1000@email.com";
            var response = await Client.GetAsync($"users/{email}");
            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Given_unique_email_user_should_be_created()
        {
            var command = new CreateUser 
            {
                Email = "test@email.com",
                Username = "test",
                Password = "secret",
                Role = "user"
            };
            var payload = GetPayload(command);
            var response = await Client.PostAsync("users", payload);
            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.Created);
            response.Headers.Location.ToString().ShouldBeEquivalentTo($"users/{command.Email}");

            var user = await GetUserAsync(command.Email);
            user.Person.Email.ShouldBeEquivalentTo(command.Email);
        }

        [Fact]
        public async Task Given_users_should_be_exists()
        {
            var response = await Client.GetAsync("users");
            var content = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<IEnumerable<UserDto>>(content);

            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
            users.Should().NotBeEmpty();
        }

        private async Task<UserDto> GetUserAsync(string email)
        {
            var response = await Client.GetAsync($"users/{email}");
            var responseString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<UserDto>(responseString);
        }
    }
}