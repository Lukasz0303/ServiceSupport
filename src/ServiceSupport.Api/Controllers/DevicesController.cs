using Microsoft.AspNetCore.Mvc;
using ServiceSupport.Infrastructure.Commands;
using ServiceSupport.Infrastructure.Services.DeviceGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceSupport.Api.Controllers
{
    public class DevicesController:ApiControllerBase
    {
        private readonly IDeviceService _deviceService;

        public DevicesController(IDeviceService deviceService,
            ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
            _deviceService = deviceService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var device = await _deviceService.GetAsync(id);
            if (device== null)
            {
                return NotFound();
            }

            return Json(device);
        }

        public async Task<IActionResult> Get()
        {
            var devices = await _deviceService.BrowseAsync();

            return Json(devices);
        }

        [HttpGet("{serialNumber}")]
        public async Task<IActionResult> Get(string serialNumber)
        {
            var device = await _deviceService.GetAsync(serialNumber);
            if (device == null)
            {
                return NotFound();
            }

            return Json(device);
        }

        [HttpGet("inactive/{minutes}")]
        public async Task<IActionResult> GetAllInactiveAsync(int minutes)
        {
            var device = await _deviceService.GetAllInactiveAsync(minutes);

            return Json(device);
        }

        [HttpGet("nodata/{minutes}")]
        public async Task<IActionResult> GetAllNoDataAsync(int minutes)
        {
            var device = await _deviceService.GetAllNoDataAsync(minutes);

            return Json(device);
        }
    }
}
