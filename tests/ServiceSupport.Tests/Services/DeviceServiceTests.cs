using AutoMapper;
using Moq;
using ServiceSupport.Core.Domain;
using ServiceSupport.Core.Repositories;
using ServiceSupport.Infrastructure.Services.DeviceGroup;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ServiceSupport.Tests.Services
{
    public class DeviceServiceTests
    {
        [Fact]
        public async Task When_calling_get_async_and_devices_exists_it_should_invoke_devices_repository_get_async()
        {
            var deviceRepositoryMock = new Mock<IDeviceRepository>();
            var mapperMock = new Mock<IMapper>();

            var deviceService = new DeviceService(deviceRepositoryMock.Object, mapperMock.Object);
            await deviceService.BrowseAsync();

            var devices = new HashSet<Device>()
            {
                new Device(Guid.NewGuid(), "ab1", new DateTime(), new DateTime(),new Shop(Guid.NewGuid(),"Toruń Kwiatowa 18/4",
                    new Person("Łukasz","Zieliński","l.zzzielinski@gail.com","530230655"),
                        "001","520640330")),
                new Device(Guid.NewGuid(), "ab2", new DateTime(), new DateTime(),new Shop(Guid.NewGuid(),"Toruń Kwiatowa 18/4",
                    new Person("Łukasz","Zieliński","l.zzzielinski@gail.com","530230655"),
                        "001","520640330")),
                new Device(Guid.NewGuid(), "ab3", new DateTime(), new DateTime(),new Shop(Guid.NewGuid(),"Toruń Kwiatowa 18/4",
                    new Person("Łukasz","Zieliński","l.zzzielinski@gail.com","530230655"),
                        "001","520640330")),
            };

            deviceRepositoryMock.Setup(x => x.GetAllAsync())
                              .ReturnsAsync(devices);

            deviceRepositoryMock.Verify(x => x.GetAllAsync(), Times.Once());
        }

        [Fact]
        public async Task When_calling_get_async_and_device_exists_it_should_invoke_device_repository_get_async()
        {
            var deviceRepositoryMock = new Mock<IDeviceRepository>();
            var mapperMock = new Mock<IMapper>();

            var deviceService = new DeviceService(deviceRepositoryMock.Object, mapperMock.Object);
            await deviceService.GetAsync("ab3");

            var device = new Device(Guid.NewGuid(), "ab3", new DateTime(), new DateTime(), new Shop(Guid.NewGuid(), "Toruń Kwiatowa 18/4",
                    new Person("Łukasz", "Zieliński", "l.zzzielinski@gail.com", "530230655"),
                        "001", "520640330"));

            deviceRepositoryMock.Setup(x => x.GetAsync(It.IsAny<string>()))
                              .ReturnsAsync(device);

            deviceRepositoryMock.Verify(x => x.GetAsync(It.IsAny<string>()), Times.Once());
        }

        [Fact]
        public async Task When_calling_get_async_and_device_does_not_exist_it_should_invoke_device_repository_get_async()
        {
            var deviceRepositoryMock = new Mock<IDeviceRepository>();
            var mapperMock = new Mock<IMapper>();

            var deviceService = new DeviceService(deviceRepositoryMock.Object, mapperMock.Object);
            await deviceService.GetAsync("ab5");

            deviceRepositoryMock.Setup(x => x.GetAsync("ab5"))
                              .ReturnsAsync(() => null);

            deviceRepositoryMock.Verify(x => x.GetAsync(It.IsAny<string>()), Times.Once());
        }
    }
}
