using ServiceSupport.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ServiceSupport.Core.Domain
{
    public class ShopTime
    {
        private static readonly Regex TimeRegex = new Regex("\\d{2}:\\d{2}$");
        public DayOfWeek Day { get; protected set; }
        public DateTime StartTime { get; protected set; } //DateTime.Now +"07:30"
        public DateTime EndTime { get; protected set; }//DateTime.Now +"18:00"
        protected ShopTime()
        {
        }

        public ShopTime(DayOfWeek day, string startTime, string endTime)
        {
            Day = day;
            SetStartTime(startTime);
            SetEndTime(endTime);
        }
        public void SetStartTime(string startTime)
        {
            if (String.IsNullOrEmpty(startTime))
            {
                throw new DomainException(ErrorCodes.InvalidTime,
                    "Time can not be empty.");
            }
            if (!TimeRegex.IsMatch(startTime))
            {
                throw new DomainException(ErrorCodes.InvalidTimeFormat,
                    "StartTime can not be empty.");
            }
            StartTime=new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                (int.Parse(startTime.Split(':')[0]) % 24),(int.Parse(startTime.Split(':')[1]) % 60),0);
        }
        public void SetEndTime(string endTime)
        {
            if (String.IsNullOrEmpty(endTime))
            {
                throw new DomainException(ErrorCodes.InvalidTimeFormat,
                    "Time can not be empty.");
            }
            if (!TimeRegex.IsMatch(endTime))
            {
                throw new DomainException(ErrorCodes.InvalidTimeFormat,
                    "StartTime can not be empty.");
            }
            EndTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                (int.Parse(endTime.Split(':')[0]) % 24), (int.Parse(endTime.Split(':')[1]) % 60), 0);
        }
    }
}
