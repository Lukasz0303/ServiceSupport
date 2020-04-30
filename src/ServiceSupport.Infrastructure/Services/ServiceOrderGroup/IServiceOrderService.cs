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

        Task<IEnumerable<ServiceOrderDto>> BrowseAsync();
        Task<IEnumerable<ServiceOrderDto>> GetAsyncPersonOrdering(string personordering);
        Task<IEnumerable<ServiceOrderDto>> GetAsyncServiceman(string emailServiceman);
        Task<IEnumerable<ServiceOrderDto>> GetAsyncNotassigned();
        Task AddAsync(ServiceOrderDto serviceOrder);

        Task SetServiceMan(string serviceOrderId, string name, string surname, string email, string phone);
        Task AddServiceOrderDescription(string serviceOrderId, string name, string surname, string email, string phone, string title, string content, string isfinished);
    }
}
