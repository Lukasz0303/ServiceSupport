using ServiceSupport.Infrastructure.Commands;
using ServiceSupport.Infrastructure.Commands.ServiceOrders;
using ServiceSupport.Infrastructure.Services;
using ServiceSupport.Infrastructure.Services.ServiceOrderGroup;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceSupport.Infrastructure.Handlers.ServiceOrders
{
    public class AddServiceOrderDescriptionHandller : ICommandHandler<AddServiceOrderDescription>
    {
        private readonly IHandler _handler;
        private readonly IServiceOrderService _serviceOrderService;

        public AddServiceOrderDescriptionHandller(IHandler handler, IServiceOrderService serviceOrderService)
        {
            _handler = handler;
            _serviceOrderService = serviceOrderService;
        }

        public async Task HandleAsync(AddServiceOrderDescription command)
        => await _handler
            .Run(async () => await _serviceOrderService.AddServiceOrderDescription(command.ServiceOrderId,
                command.Name, command.Surname, command.Email, command.Phone,command.Title,command.Content,command.IsFinished))
            .Next()
            .ExecuteAllAsync();
    }
}
