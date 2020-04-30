using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceSupport.Infrastructure.DTO
{
    public class ShopTimeDto
    {
        public DayOfWeek Day { get; set; }
        public DateTime StartTime { get; set; } //"07:30"
        public DateTime EndTime { get; set; }
    }
}
