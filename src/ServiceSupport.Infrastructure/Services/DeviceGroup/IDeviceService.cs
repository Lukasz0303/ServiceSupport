using ServiceSupport.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceSupport.Infrastructure.Services.DeviceGroup
{
    public interface IDeviceService:IService
    {
        Task<DeviceDto> GetAsync(Guid id);
        Task<DeviceDto> GetAsync(string serialNumber);
        Task<IEnumerable<DeviceDto>> BrowseAsync();
        Task<IEnumerable<DeviceDto>> GetAllInactiveAsync(int minutes);
        Task<IEnumerable<DeviceDto>> GetAllNoDataAsync(int minutes);
    }
}
