using FluentAssertions;
using Newtonsoft.Json;
using ServiceSupport.Infrastructure.Commands.Shops;
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

        [Fact]
        public async Task Given_unique_id_shop_should_be_created()
        {
            var command = new CreateShop
            {
                ResponsiblePerson =new PersonDto() { Email = "test@email.com", Name = "test", Phone = "87378543", Surname = "hivfdh" },
                Address = "Kwaitowa 3/12",
                Phone = "33567876867876",
                SID = "005"
            };
            var payload = GetPayload(command);
            var response = await Client.PostAsync("shops", payload);
            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.Created);

            string id = response.Headers.Location.ToString().Replace("shops/", "");
            var shop = await GetShopAsync(id);
            shop.Id.ToString().ShouldBeEquivalentTo(id);
        }

        private async Task<ShopDto> GetShopAsync(string id)
        {
            var response = await Client.GetAsync($"shops/{id}");
            var responseString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<ShopDto>(responseString);
        }
    }
}
