using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ServiceSupport.Infrastructure.Commands;
using ServiceSupport.Infrastructure.Services.ShopGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceSupport.Api.Controllers
{
    public class ShopsController : ApiControllerBase
    {
        private readonly IShopService _shopService;

        public ShopsController(IShopService shopService,
            ICommandDispatcher commandDispatcher, IMemoryCache cache) : base(commandDispatcher)
        {
            _shopService = shopService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var shop = await _shopService.GetAsync(id);
            if (shop == null)
            {
                return NotFound();
            }

            return Json(shop);
        }

        public async Task<IActionResult> Get()
        {
            var shops = await _shopService.GetAllAsync();

            return Json(shops);
        }
    }
}
