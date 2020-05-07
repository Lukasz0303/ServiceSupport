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
    public class ShopsControllerTests:ControllerTestsBase
    {
        [Fact]
        public async Task Given_shops_should_be_exists()
        {
            var response = await Client.GetAsync("shops");
            var content = await response.Content.ReadAsStringAsync();
            var shops = JsonConvert.DeserializeObject<IEnumerable<ShopDto>>(content);

            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
            shops.Should().NotBeEmpty();
        }
    }
}
