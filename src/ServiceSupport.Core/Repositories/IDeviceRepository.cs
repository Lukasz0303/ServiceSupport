using ServiceSupport.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceSupport.Core.Repositories
{
    public interface IDeviceRepository : IRepository
    {
        Task<Device> GetAsync(Guid id);
        Task<Device> GetAsync(string serialNumber);
        Task<IEnumerable<Device>> GetAllAsync();
        Task<IEnumerable<Device>> GetAllInactiveAsync(int minutes);
        Task<IEnumerable<Device>> GetAllNoDataAsync(int minutes);
        Task AddAsync(Device device);
        Task UpdateAsync(Device device);
        Task RemoveAsync(Guid id);
    }
}
