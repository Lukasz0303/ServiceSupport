using ServiceSupport.Infrastructure.Commands.Shops;
using ServiceSupport.Infrastructure.CQRS.Comands;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceSupport.Infrastructure.CQRS.Shops
{
    public class CreateShopCommand : CommandBase<Guid>
    {
        public CreateShop CreateShop;

        public CreateShopCommand(CreateShop createShop)
        {
            CreateShop = createShop;
        }
    }
}
