using AutoMapper;
using ServiceSupport.Core.Domain;
using ServiceSupport.Core.Repositories;
using ServiceSupport.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceSupport.Infrastructure.Services.ServiceOrderGroup
{
    public class ServiceOrderService : IServiceOrderService
    {
        private readonly IServiceOrderRepository _serviceOrderRepository;
        private readonly IMapper _mapper;

        public ServiceOrderService(IServiceOrderRepository serviceOrderRepository, IMapper mapper)
        {
            _serviceOrderRepository = serviceOrderRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(ServiceOrderDto serviceOrder)
        {
            await _serviceOrderRepository.AddAsync(_mapper.Map < ServiceOrderDto, ServiceOrder>( serviceOrder));
        }

        public async Task<ServiceOrderDto> GetAsync(Guid id)
        {
            var serviceOrder = await _serviceOrderRepository.GetAsync(id);

            return _mapper.Map<ServiceOrder, ServiceOrderDto>(serviceOrder);
        }

        public async Task<IEnumerable<ServiceOrderDto>> GetAsyncPersonOrdering(PersonDto person)
        {
            var serviceOrders = await _serviceOrderRepository.
                GetAsyncPersonOrdering(_mapper.Map < PersonDto, Person > (person));

            return _mapper.Map<IEnumerable<ServiceOrder>, IEnumerable<ServiceOrderDto>>(serviceOrders);
        }

        public async Task<IEnumerable<ServiceOrderDto>> GetAsyncServiceman(PersonDto person)
        {
            var serviceOrders = await _serviceOrderRepository.
                GetAsyncServiceman(_mapper.Map<PersonDto, Person>(person));

            return _mapper.Map<IEnumerable<ServiceOrder>, IEnumerable<ServiceOrderDto>>(serviceOrders);
        }
    }
}
