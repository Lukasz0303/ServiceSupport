using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceSupport.Infrastructure.Commands;
using ServiceSupport.Infrastructure.Commands.ServiceOrders;
using ServiceSupport.Infrastructure.DTO;
using ServiceSupport.Infrastructure.Services;
using ServiceSupport.Infrastructure.Services.ServiceOrderGroup;

namespace ServiceSupport.Api.Controllers
{
    public class ServiceOrdersController : ApiControllerBase
    {
        private readonly IServiceOrderService _serviceOrderService;

        public ServiceOrdersController(IServiceOrderService ServiceOrderService,
            ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
            _serviceOrderService = ServiceOrderService;
        }

        public async Task<IActionResult> Get()
        {
            var serviceOrders = await _serviceOrderService.BrowseAsync();

            return Json(serviceOrders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var serviceOrder = await _serviceOrderService.GetAsync(id);
            if (serviceOrder == null)
            {
                return NotFound();
            }

            return Json(serviceOrder);
        }

        [HttpGet("personordering/{email}")]
        public async Task<IActionResult> GetServiceOrdersByPersonOrdering(string email)
        {
            var ServiceOrder = await _serviceOrderService.GetAsyncPersonOrdering(email);
            if (ServiceOrder == null)
            {
                return NotFound();
            }

            return Json(ServiceOrder);
        }

        [HttpGet("serviceman/{email}")]
        public async Task<IActionResult> GetServiceOrdersByServiceman(string email)
        {
            var ServiceOrder = await _serviceOrderService.GetAsyncServiceman(email);
            if (ServiceOrder == null)
            {
                return NotFound();
            }

            return Json(ServiceOrder);
        }

        [HttpGet("notassigned")]
        public async Task<IActionResult> GetAsyncNotassigned()
        {
            var ServiceOrder = await _serviceOrderService.GetAsyncNotassigned();
            if (ServiceOrder == null)
            {
                return NotFound();
            }

            return Json(ServiceOrder);
        }

        [HttpPost("setserviceman/")]
        public async Task<IActionResult> Post([FromBody]SetServiceMan command)
        {
            await DispatchAsync(command);
            return Created($"serviceorders/{command.ServiceOrderId}",null);
        }

        [HttpPost("adddescription/")]
        public async Task<IActionResult> Post([FromBody]AddServiceOrderDescription command)
        {
            await DispatchAsync(command);
            return Created($"serviceorders/{command.ServiceOrderId}", null);
        }
    }
}
