using ServiceSupport.Core.Domain;
using ServiceSupport.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceSupport.Infrastructure.Repositories.InMemory
{
    public class InMemoryDeviceRepository : IDeviceRepository
    {
        private static readonly ISet<Device> _devices = new HashSet<Device>();

        public async Task<IEnumerable<Device>> GetAllAsync()
            => await Task.FromResult(_devices);

        public async Task<Device> GetAsync(Guid id)
            => await Task.FromResult(_devices.SingleOrDefault(x => x.Id == id));

        public async Task<Device> GetAsync(string serialNumber)
            => await Task.FromResult(_devices.SingleOrDefault(x => x.SerialNumber.ToLowerInvariant() == serialNumber.ToLowerInvariant()));

        public async Task<IEnumerable<Device>> GetAllInactiveAsync(int minutes)
            => await Task.FromResult(_devices.Where(x => x.LastSignalLife>DateTime.Now.AddMinutes(-minutes)));

        public async Task<IEnumerable<Device>> GetAllNoDataAsync(int minutes)
        { 
            return await Task.FromResult(_devices.Where(x => x.LastSentData > DateTime.Now.AddMinutes(-minutes) &&
             x.Shop.IsOpen(minutes)));
        }


        public async Task AddAsync(Device device)
        {
            _devices.Add(device);
            await Task.CompletedTask;
        }

        public async Task RemoveAsync(Guid id)
        {
            var device = await GetAsync(id);
            _devices.Remove(device);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Device device)
        {
            await Task.CompletedTask;
        }
    }
}
