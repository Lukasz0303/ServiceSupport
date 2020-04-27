﻿using ServiceSupport.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceSupport.Infrastructure.Services.ShopGroup
{
    public interface IShopService:IService
    {
        Task<ShopDto> GetAsync(Guid id);
        Task<IEnumerable<ShopDto>> GetAllAsync();
    }
}