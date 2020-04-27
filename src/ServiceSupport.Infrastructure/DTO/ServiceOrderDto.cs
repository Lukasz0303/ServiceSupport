using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceSupport.Infrastructure.DTO
{
    public class ServiceOrderDto
    {
        public IEnumerable<ServiceOrderDescriptionDto> ServiceOrderDescriptions { get; set; }
        public Guid Id { get; set; }
        public PersonDto PersonOrdering { get; set; }
        public PersonDto Serviceman { get; set; }
        public string Status { get; set; }
    }
}
