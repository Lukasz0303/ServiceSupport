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
        public async Task<ShopDto> GetAsync(Guid id)
        {
            var shop = await _shopRepository.GetAsync(id);

            return _mapper.Map<Shop, ShopDto>(shop);
        }

        public async Task AddShopTime(Guid id, DayOfWeek day, string startTime, string endTime)
        {
            await _shopRepository.AddShopTime(id, day, startTime, endTime);
        }

        public async Task<IEnumerable<ShopDto>> GetAllAsync()
        {
            var shops = await _shopRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<Shop>, IEnumerable<ShopDto>>(shops);
        }
    }
}
