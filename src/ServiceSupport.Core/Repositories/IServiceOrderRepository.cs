using ServiceSupport.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceSupport.Core.Repositories
{
    public interface IServiceOrderRepository : IRepository
    {
        Task<ServiceOrder> GetAsync(Guid id);
        Task<IEnumerable<ServiceOrder>> GetAsyncPersonOrdering(Person person);
        Task<IEnumerable<ServiceOrder>> GetAsyncServiceman(Person person);
        Task AddAsync(ServiceOrder serviceOrder);
        Task UpdateAsync(ServiceOrder serviceOrder);
        Task RemoveAsync(Guid id);
    }
}
