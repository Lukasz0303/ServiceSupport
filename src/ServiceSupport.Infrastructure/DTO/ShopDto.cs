using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceSupport.Infrastructure.DTO
{
    public class ShopDto
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public PersonDto ResponsiblePerson { get; set; }
        public string SID { get; set; }
        public string Phone { get; set; }

        public IEnumerable<ShopTimeDto> ShopTime { get; set; }
    }
}
