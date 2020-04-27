using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceSupport.Infrastructure.DTO
{
    public class ShopTimeDto
    {
        public DayOfWeek Day { get; set; }
        public TimeSpan StartTime { get; set; } //"07:30"
        public TimeSpan EndTime { get; set; }
    }
}
