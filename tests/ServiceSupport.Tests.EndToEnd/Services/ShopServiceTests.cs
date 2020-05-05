using AutoMapper;
using Moq;
using ServiceSupport.Core.Domain;
using ServiceSupport.Core.Repositories;
using ServiceSupport.Infrastructure.Services.ShopGroup;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ServiceSupport.Tests.Services
{
    public class ShopServiceTests
    {
        [Fact]
        public async Task When_calling_get_async_and_shop_exists_it_should_invoke_shop_repository_get_async()
        {
            var shopRepositoryMock = new Mock<IShopRepository>();
            var mapperMock = new Mock<IMapper>();

            var shopService = new ShopService(shopRepositoryMock.Object, mapperMock.Object);

            var shop = new Shop(Guid.NewGuid(), "Trzcinowa 13", new Person("test@gmail.com"), "001", "500540340");

            await shopService.GetAsync(shop.Id);


            shopRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>()))
                              .ReturnsAsync(shop);

            shopRepositoryMock.Verify(x => x.GetAsync(It.IsAny<Guid>()), Times.Once());
        }
        [Fact]
        public async Task When_calling_get_async_and_shops_exists_it_should_invoke_shops_repository_get_async()
        {
            var shopRepositoryMock = new Mock<IShopRepository>();
            var mapperMock = new Mock<IMapper>();

            var shopService = new ShopService(shopRepositoryMock.Object, mapperMock.Object);
            await shopService.GetAllAsync();

            var shops = new HashSet<Shop>()
            {
                 new Shop(Guid.NewGuid(), "Trzcinowa 13", new Person("test@gmail.com"), "001", "500540340"),
                 new Shop(Guid.NewGuid(), "Trzcinowa 14", new Person("test2@gmail.com"), "002", "500540340"),
                 new Shop(Guid.NewGuid(), "Trzcinowa 15", new Person("test3@gmail.com"), "003", "500540340")
            };

            shopRepositoryMock.Setup(x => x.GetAllAsync())
                              .ReturnsAsync(shops);

            shopRepositoryMock.Verify(x => x.GetAllAsync(), Times.Once());
        }
    }
}
