using AutoMapper;
using ServiceSupport.Core.Domain;
using ServiceSupport.Core.Repositories;
using ServiceSupport.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceSupport.Infrastructure.Services.ShopGroup
{
    public class ShopService:IShopService
    {
        private readonly IShopRepository _shopRepository;
        private readonly IMapper _mapper;

        public ShopService(IShopRepository shopRepository, IMapper mapper)
        {
            _shopRepository = shopRepository;
            _mapper = mapper;
        }
        async Task<ShopDto> IShopService.GetAsync(Guid id)
        {
            var shop = await _shopRepository.GetAsync(id);

            return _mapper.Map<Shop, ShopDto>(shop);
        }

        async Task<IEnumerable<ShopDto>> IShopService.GetAllAsync()
        {
            var shops = await _shopRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<Shop>, IEnumerable<ShopDto>>(shops);
        }
    }
}
