using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceSupport.Infrastructure.DTO
{
    public class DeviceDto
    {
        public IEnumerable<ServiceOrderDto> ServiceOrders { get; set; }
        public Guid Id { get; set; }
        public string SerialNumber { get; set; } // szesnastkowy
        public DateTime LastSentData { get; set; } //W godzinach pracy sklepu, urządzenie powinno wysłać dane conajmij raz na godzinę
        public DateTime LastSignalLife { get; set; } //Urządznie powinno się komunikować raz na 5 min
        public ShopDto Shop { get; set; }
    }
}
