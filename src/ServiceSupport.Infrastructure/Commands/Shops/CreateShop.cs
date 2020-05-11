using ServiceSupport.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceSupport.Infrastructure.Commands.Shops
{
    public class CreateShop
    {
        public string Address { get; set; }
        public PersonDto ResponsiblePerson { get; set; }
        public string SID { get; set; }
        public string Phone { get; set; }

    }
}
