using ServiceSupport.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceSupport.Core.Repositories
{
    public interface IServiceOrderRepository : IRepository
    {
        Task<IEnumerable<ServiceOrder>> GetAllAsync();
        Task<ServiceOrder> GetAsync(Guid id);
        Task<ServiceOrder> GetAsync(string id);
        Task<IEnumerable<ServiceOrder>> GetAsyncPersonOrdering(string emailPersonOrdering);
        Task<IEnumerable<ServiceOrder>> GetAsyncServiceman(string emailServiceman);
        Task<IEnumerable<ServiceOrder>> GetAsyncNotassigned();
        Task AddAsync(ServiceOrder serviceOrder);
        Task UpdateAsync(ServiceOrder serviceOrder);
        Task RemoveAsync(Guid id);
    }
}
