﻿using AutoMapper;
using ServiceSupport.Core.Domain;
using ServiceSupport.Core.Repositories;
using ServiceSupport.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceSupport.Infrastructure.Services.DeviceGroup
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IMapper _mapper;

        public DeviceService(IDeviceRepository deviceRepository, IMapper mapper)
        {
            _deviceRepository = deviceRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<DeviceDto>> GetAllAsync()
        {
            var devices = await _deviceRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<Device>, IEnumerable<DeviceDto>>(devices);
        }

        public async Task<DeviceDto> GetAsync(string serialNumber)
        {
            var device = await _deviceRepository.GetAsync(serialNumber);

            return _mapper.Map<Device, DeviceDto>(device);
        }
    }
}
