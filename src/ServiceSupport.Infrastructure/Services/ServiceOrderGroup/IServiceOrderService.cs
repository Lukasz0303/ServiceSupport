using ServiceSupport.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceSupport.Infrastructure.Services.ServiceOrderGroup
{
    public interface IServiceOrderService:IService
    {
        Task<ServiceOrderDto> GetAsync(Guid id);
        Task<IEnumerable<ServiceOrderDto>> GetAsyncPersonOrdering(PersonDto person);
        Task<IEnumerable<ServiceOrderDto>> GetAsyncServiceman(PersonDto person);
        Task AddAsync(ServiceOrderDto serviceOrder);
    }
}
