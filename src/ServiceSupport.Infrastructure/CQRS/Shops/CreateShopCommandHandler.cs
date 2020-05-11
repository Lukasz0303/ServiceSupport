using ServiceSupport.Infrastructure.CQRS.Comands;
using ServiceSupport.Infrastructure.Services.ShopGroup;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceSupport.Infrastructure.CQRS.Shops
{
    public class CreateShopCommandHandler : ICommandHandler<CreateShopCommand, Guid>
    {
        readonly IShopService _shopService;
        public CreateShopCommandHandler(IShopService shopService)
        {
            _shopService = shopService;
        }

        public Task<Guid> Handle(CreateShopCommand request, CancellationToken cancellationToken)
        {
            return _shopService.RegisterAsync(request.Id, request.CreateShop);
        }
    }
}
