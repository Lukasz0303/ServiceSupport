using ServiceSupport.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using ServiceSupport.Infrastructure.Services.ShopGroup;
using ServiceSupport.Infrastructure.CQRS.Queries;

namespace ServiceSupport.Infrastructure.CQRS.Shops
{
    public class GetShopQueryHandler : IQueryHandler<GetShopQuery, ShopDto>
    {
        IShopService _shopService;
        public GetShopQueryHandler(IShopService shopService)
        {
            _shopService = shopService;
        }

        public Task<ShopDto> Handle(GetShopQuery request, CancellationToken cancellationToken)
        {
            return _shopService.GetAsync(request.ShopId);
        }
    }
}
