using FluentAssertions;
using Newtonsoft.Json;
using ServiceSupport.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ServiceSupport.Tests.EndToEnd.Controllers
{
    public class ServiceOrdersControllerTests:ControllerTestsBase
    {
        [Fact]
        public async Task Given_serviceorders_should_be_exists()
        {
            var response = await Client.GetAsync("serviceorders");
            var content = await response.Content.ReadAsStringAsync();
            var serviceorders = JsonConvert.DeserializeObject<IEnumerable<ServiceOrderDto>>(content);

            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
            serviceorders.Should().NotBeEmpty();
        }

        [Fact]
        public async Task Given_invalid_email_serviceorder_personordering_should_not_exist()
        {
            var email = "user1000@email.com";
            var response = await Client.GetAsync($"serviceorders/personordering/{email}");
            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Given_invalid_email_serviceorder_serviceman_should_not_exist()
        {
            var email = "user1000@email.com";
            var response = await Client.GetAsync($"serviceorders/serviceman/{email}");
            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.NotFound);
        }

    }
}
