using ServiceSupport.Core.Domain;
using ServiceSupport.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceSupport.Infrastructure.Repositories.InMemory
{
    public class InMemoryShopRepository:IShopRepository
    {
        private static readonly ISet<Shop> _shops = new HashSet<Shop>();
        public async Task AddAsync(Shop shop)
        {
            _shops.Add(shop);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Shop>> GetAllAsync()
            => await Task.FromResult(_shops);

        public async Task<Shop> GetAsync(Guid id)
            => await Task.FromResult(_shops.SingleOrDefault(x => x.Id == id));

        public async Task RemoveAsync(Guid id)
        {
            var shop = await GetAsync(id);
            _shops.Remove(shop);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Shop shop)
        {
            await Task.CompletedTask;
        }

        public async Task AddShopTime(Guid id, DayOfWeek day, string startTime, string endTime)
        {
            var shop = await GetAsync(id);
            shop.AddShopTime(day, startTime, endTime);
            await Task.CompletedTask;
        }
    }
}
