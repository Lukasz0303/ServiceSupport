using ServiceSupport.Infrastructure.CQRS.Queries;
using ServiceSupport.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceSupport.Infrastructure.CQRS.Shops
{
    public class GetShopsQuery : IQuery<IEnumerable<ShopDto>>
    {
    }
}
