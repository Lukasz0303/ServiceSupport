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
    public class DevicesControllerTests: ControllerTestsBase
    {
        [Fact]
        public async Task Given_invalid_serialnumber_device_should_not_exist()
        {
            var serialNumber = "aaa321bbbcccc";
            var response = await Client.GetAsync($"devices/{serialNumber}");
            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Given_devices_should_be_exists()
        {
            var response = await Client.GetAsync("devices");
            var content = await response.Content.ReadAsStringAsync();
            var devices = JsonConvert.DeserializeObject<IEnumerable<DeviceDto>>(content);

            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
            devices.Should().NotBeEmpty();
        }
    }
}
