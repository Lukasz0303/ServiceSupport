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
    public class GetShopsQueryHandler : IQueryHandler<GetShopsQuery, IEnumerable<ShopDto>>
    {
        IShopService _shopService;
        public GetShopsQueryHandler(IShopService shopService)
        {
            _shopService = shopService;
        }
        public async Task<IEnumerable<ShopDto>> Handle(GetShopsQuery request, CancellationToken cancellationToken)
        {
            return await _shopService.GetAllAsync();
        }
    }
}
