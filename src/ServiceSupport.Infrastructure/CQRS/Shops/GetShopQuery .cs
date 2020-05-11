using ServiceSupport.Infrastructure.CQRS.Queries;
using ServiceSupport.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceSupport.Infrastructure.CQRS.Shops
{
    public class GetShopQuery : IQuery<ShopDto>
    {
        public Guid ShopId { get; }
        public GetShopQuery(Guid shopId)
        {
            ShopId = shopId;
        }
    }
}
