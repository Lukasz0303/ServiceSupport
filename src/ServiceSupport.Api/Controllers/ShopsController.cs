using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ServiceSupport.Infrastructure.Commands;
using ServiceSupport.Infrastructure.CQRS.Shops;
using ServiceSupport.Infrastructure.Services.ShopGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceSupport.Api.Controllers
{
    public class ShopsController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IShopService _shopService;

        public ShopsController(IShopService shopService,
            ICommandDispatcher commandDispatcher,IMediator mediator) : base(commandDispatcher)
        {
            _shopService = shopService;
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var shop = await _mediator.Send(new GetShopQuery(id));
            return Ok(shop);
        }

        public async Task<IActionResult> Get()
        {
            var shops = await _mediator.Send(new GetShopsQuery());
            return Ok(shops);
        }
    }
}
