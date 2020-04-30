using ServiceSupport.Core.Domain;
using ServiceSupport.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceSupport.Infrastructure.Repositories.InMemory
{
    public class InMemoryServiceOrderRepository : IServiceOrderRepository
    {
        private static readonly ISet<ServiceOrder> _serviceOrder = new HashSet<ServiceOrder>();

        public async Task<IEnumerable<ServiceOrder>> GetAllAsync()
            => await Task.FromResult(_serviceOrder);

        public async Task<IEnumerable<ServiceOrder>> GetAsyncNotassigned()
            => await Task.FromResult(_serviceOrder.Where(x=>x.Status== ServiceOrderStatuses.AutomaticGenerated));

        public async Task<ServiceOrder> GetAsync(Guid id)
            => await Task.FromResult(_serviceOrder.SingleOrDefault(x => x.Id == id));

        public async Task<ServiceOrder> GetAsync(string id)
            => await Task.FromResult(_serviceOrder.SingleOrDefault(x => x.Id+"" == id));

        public async Task<IEnumerable<ServiceOrder>> GetAsyncPersonOrdering(string emailPersonOrdering)
            => await Task.FromResult(_serviceOrder.Where(x => x.PersonOrdering.Email.ToLowerInvariant() == emailPersonOrdering.ToLowerInvariant()));

        public async Task<IEnumerable<ServiceOrder>> GetAsyncServiceman(string emailServiceman)
            => await Task.FromResult(_serviceOrder.Where(x=> x.Serviceman!=null).Where(x => x.Serviceman.Email.ToLowerInvariant() == emailServiceman.ToLowerInvariant()));

        public async Task RemoveAsync(Guid id)
        {
            var device = await GetAsync(id);
            _serviceOrder.Remove(device);
            await Task.CompletedTask;
        }

        public async Task AddAsync(ServiceOrder serviceOrder)
        {
            _serviceOrder.Add(serviceOrder);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(ServiceOrder serviceOrder)
        {
            await Task.CompletedTask;
        }
    }
}
