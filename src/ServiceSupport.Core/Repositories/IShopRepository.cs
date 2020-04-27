using ServiceSupport.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceSupport.Core.Repositories
{
    public interface IShopRepository: IRepository
    {
        Task<Shop> GetAsync(Guid id);
        Task<IEnumerable<Shop>> GetAllAsync();
        Task AddAsync(Shop shop);
        Task UpdateAsync(Shop shop);
        Task RemoveAsync(Guid id);
    }
}
