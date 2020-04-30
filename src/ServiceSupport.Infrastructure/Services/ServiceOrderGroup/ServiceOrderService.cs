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

        public async Task<IEnumerable<ServiceOrderDto>> BrowseAsync()
        {
            var serviceOrders = await _serviceOrderRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<ServiceOrder>, IEnumerable<ServiceOrderDto>>(serviceOrders);
        }

        public async Task<IEnumerable<ServiceOrderDto>> GetAsyncNotassigned()
        {
            var serviceOrders = await _serviceOrderRepository.GetAsyncNotassigned();

            return _mapper.Map<IEnumerable<ServiceOrder>, IEnumerable<ServiceOrderDto>>(serviceOrders);
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

        public async Task<IEnumerable<ServiceOrderDto>> GetAsyncPersonOrdering(string emailPersonOrdering)
        {
            var serviceOrders = await _serviceOrderRepository.
                GetAsyncPersonOrdering(emailPersonOrdering);

            return _mapper.Map<IEnumerable<ServiceOrder>, IEnumerable<ServiceOrderDto>>(serviceOrders);
        }

        public async Task<IEnumerable<ServiceOrderDto>> GetAsyncServiceman(string emailServiceman)
        {
            var serviceOrders = await _serviceOrderRepository.
                GetAsyncServiceman(emailServiceman);

            return _mapper.Map<IEnumerable<ServiceOrder>, IEnumerable<ServiceOrderDto>>(serviceOrders);
        }

        public async Task SetServiceMan(string serviceOrderId, string name, string surname, string email, string phone)
        {
            var serviceOrder = await _serviceOrderRepository.GetAsync(serviceOrderId);
            serviceOrder.SetServiceman(new Person(name, surname, email, phone));
        }

        public async Task AddServiceOrderDescription(string serviceOrderId, string name, string surname, string email, string phone, string title, string content,string isfinished)
        {
            var serviceOrder = await _serviceOrderRepository.GetAsync(serviceOrderId);
            serviceOrder.AddServiceOrderDescription(title,content, new Person(name, surname, email, phone), isfinished);
        }
    }
}
