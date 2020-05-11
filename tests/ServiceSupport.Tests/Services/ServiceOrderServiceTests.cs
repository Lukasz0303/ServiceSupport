using AutoMapper;
using Moq;
using ServiceSupport.Core.Domain;
using ServiceSupport.Core.Repositories;
using ServiceSupport.Infrastructure.Services.ServiceOrderGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ServiceSupport.Tests.Services
{
    public class ServiceOrderServiceTests
    {
        [Fact]
        public async Task When_calling_get_async_and_orderservice_exists_it_should_invoke_orderservice_repository_get_async()
        {
            var OrderServiceRepositoryMock = new Mock<IServiceOrderRepository>();
            var mapperMock = new Mock<IMapper>();

            var OrderServiceService = new ServiceOrderService(OrderServiceRepositoryMock.Object, mapperMock.Object);

            var OrderService = new ServiceOrder(Guid.NewGuid(), new Person("test@gmail.com"), new Person("test1@gmail.com"));

            await OrderServiceService.GetAsync(OrderService.Id);


            OrderServiceRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>()))
                              .ReturnsAsync(OrderService);

            OrderServiceRepositoryMock.Verify(x => x.GetAsync(It.IsAny<Guid>()), Times.Once());
        }

        [Fact]
        public async Task When_calling_get_async_and_orderservice_exists_it_should_invoke_orderservice_personordering_repository_get_async()
        {
            var OrderServiceRepositoryMock = new Mock<IServiceOrderRepository>();
            var mapperMock = new Mock<IMapper>();

            var OrderServiceService = new ServiceOrderService(OrderServiceRepositoryMock.Object, mapperMock.Object);

            var OrderService = new ServiceOrder(Guid.NewGuid(), new Person("test@gmail.com"), new Person("test1@gmail.com"));

            var OrderServices = new HashSet<ServiceOrder>()
            {
                  new ServiceOrder(Guid.NewGuid(), new Person("test1@gmail.com"), new Person("test2@gmail.com")),
                  new ServiceOrder(Guid.NewGuid(), new Person("test1@gmail.com"), new Person("test2@gmail.com")),
                  new ServiceOrder(Guid.NewGuid(), new Person("test1@gmail.com"), new Person("test2@gmail.com")),
            };

            await OrderServiceService.GetAsyncPersonOrdering("test1@gmail.com");


            OrderServiceRepositoryMock.Setup(x => x.GetAsyncPersonOrdering(It.IsAny<string>()))
                              .ReturnsAsync(OrderServices);

            OrderServiceRepositoryMock.Verify(x => x.GetAsyncPersonOrdering(It.IsAny<string>()), Times.Once());
        }

        [Fact]
        public async Task When_calling_get_async_and_orderservice_exists_it_should_invoke_orderservice_serviceman_repository_get_async()
        {
            var OrderServiceRepositoryMock = new Mock<IServiceOrderRepository>();
            var mapperMock = new Mock<IMapper>();

            var OrderServiceService = new ServiceOrderService(OrderServiceRepositoryMock.Object, mapperMock.Object);
            var OrderServices = new HashSet<ServiceOrder>()
            {
                  new ServiceOrder(Guid.NewGuid(), new Person("test1@gmail.com"), new Person("test2@gmail.com")),
                  new ServiceOrder(Guid.NewGuid(), new Person("test1@gmail.com"), new Person("test2@gmail.com")),
                  new ServiceOrder(Guid.NewGuid(), new Person("test1@gmail.com"), new Person("test2@gmail.com")),
            };

            await OrderServiceService.GetAsyncServiceman("test2@gmail.com");


            OrderServiceRepositoryMock.Setup(x => x.GetAsyncServiceman(It.IsAny<string>()))
                              .ReturnsAsync(OrderServices);

            OrderServiceRepositoryMock.Verify(x => x.GetAsyncServiceman(It.IsAny<string>()), Times.Once());
        }

        [Fact]
        public async Task When_calling_get_async_and_OrderServices_exists_it_should_invoke_OrderServices_repository_get_async()
        {
            var OrderServiceRepositoryMock = new Mock<IServiceOrderRepository>();
            var mapperMock = new Mock<IMapper>();

            var OrderServiceService = new ServiceOrderService(OrderServiceRepositoryMock.Object, mapperMock.Object);
            await OrderServiceService.BrowseAsync();

            var OrderServices = new HashSet<ServiceOrder>()
            {
                  new ServiceOrder(Guid.NewGuid(), new Person("test1@gmail.com"), new Person("test3@gmail.com")),
                  new ServiceOrder(Guid.NewGuid(), new Person("test2@gmail.com"), new Person("test4@gmail.com")),
                  new ServiceOrder(Guid.NewGuid(), new Person("test3@gmail.com"), new Person("test5@gmail.com")),
            };

            OrderServiceRepositoryMock.Setup(x => x.GetAllAsync())
                              .ReturnsAsync(OrderServices);

            OrderServiceRepositoryMock.Verify(x => x.GetAllAsync(), Times.Once());
        }

        [Fact]
        public async Task When_calling_get_async_and_OrderServices_exists_it_should_invoke_OrderServices_repository_getnotassigned_async()
        {
            var OrderServiceRepositoryMock = new Mock<IServiceOrderRepository>();
            var mapperMock = new Mock<IMapper>();

            var OrderServiceService = new ServiceOrderService(OrderServiceRepositoryMock.Object, mapperMock.Object);
            await OrderServiceService.GetAsyncNotassigned();

            var OrderServices = new HashSet<ServiceOrder>()
            {
                  new ServiceOrder(Guid.NewGuid(), new Person("test1@gmail.com"), new Person("test3@gmail.com")),
                  new ServiceOrder(Guid.NewGuid(), new Person("test2@gmail.com")),
                  new ServiceOrder(Guid.NewGuid(), new Person("test3@gmail.com")),
            };

            OrderServiceRepositoryMock.Setup(x => x.GetAsyncNotassigned())
                              .ReturnsAsync(OrderServices);

            OrderServiceRepositoryMock.Verify(x => x.GetAsyncNotassigned(), Times.Once());
        }
    }
}
