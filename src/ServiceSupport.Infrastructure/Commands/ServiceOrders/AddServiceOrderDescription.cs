using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceSupport.Infrastructure.Commands.ServiceOrders
{
    public class AddServiceOrderDescription:ICommand
    {
        public string ServiceOrderId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string IsFinished { get; set; }
    }
}
