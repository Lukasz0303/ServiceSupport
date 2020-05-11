using AutoMapper;
using ServiceSupport.Core.Domain;
using ServiceSupport.Core.Repositories;
using ServiceSupport.Infrastructure.Commands.Shops;
using ServiceSupport.Infrastructure.DTO;
using ServiceSupport.Infrastructure.Exceptions;
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

        public async Task<Guid> RegisterAsync(Guid shopId, CreateShop createShop)
        {
            var shop = await _shopRepository.GetAsync(shopId);
            if (shop != null)
            {
                throw new ServiceException(ErrorCodes.ShopExist,
                    $"Shop with id: '{shop.Id}' already exists.");
            }

            var responsiblePerson = _mapper.Map<PersonDto,Person>(createShop.ResponsiblePerson);
            shop = new Shop(shopId, createShop.Address, responsiblePerson, createShop.SID, createShop.Phone);
            await _shopRepository.AddAsync(shop);
            return _mapper.Map<Guid, Guid>(shop.Id);
        }
    }
}
