using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceSupport.Infrastructure.DTO
{
    public class ServiceOrderDescriptionDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public PersonDto Person { get; set; }
    }
}
