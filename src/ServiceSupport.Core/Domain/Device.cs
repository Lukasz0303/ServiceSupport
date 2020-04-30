using ServiceSupport.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ServiceSupport.Core.Domain
{
    public class Device
    {
        private static readonly Regex SerialNumberRegex = new Regex("^[0-9a-f]+$");

        private ISet<ServiceOrder> _serviceOrders = new HashSet<ServiceOrder>();
        public IEnumerable<ServiceOrder> ServiceOrders => _serviceOrders;
        public Guid Id { get; protected set; }
        public string SerialNumber { get; protected set; } // szesnastkowy
        public DateTime LastSentData { get; protected set; } //W godzinach pracy sklepu, urządzenie powinno wysłać dane conajmij raz na godzinę
        public DateTime LastSignalLife { get; protected set; } //Urządznie powinno się komunikować raz na 5 min
        public DateTime Created { get; protected set; }
        public DateTime Updated { get; protected set; } 
        public Shop Shop { get; protected set; }

        protected Device()
        {
        }

        public Device(Guid deviceId, string serialNumber, DateTime lastSentData, DateTime lastSignalLife,
            Shop shop)
        {
            Id = deviceId;
            SetSerialNumber(serialNumber);
            LastSentData = lastSentData;
            LastSignalLife = lastSignalLife;
            SetShop(shop);
            Created = DateTime.UtcNow;
        }

        public void SetSerialNumber(string serialNumber)
        {
            if (!SerialNumberRegex.IsMatch(serialNumber))
            {
                throw new DomainException(ErrorCodes.InvalidSerialNumber,
                    "Serial number is invalid.");
            }

            if (String.IsNullOrEmpty(serialNumber))
            {
                throw new DomainException(ErrorCodes.InvalidSerialNumber,
                    "Serial number is invalid.");
            }

            SerialNumber = serialNumber.ToUpperInvariant();
            Updated = DateTime.UtcNow;
        }

        public void SetShop(Shop shop)
        {
            if (shop==null)
            {
                throw new DomainException(ErrorCodes.InvalidSerialNumber,
                    "Shop is null.");
            }

            Shop = shop;
            Updated = DateTime.UtcNow;
        }
        public void AddServiceOrder(Guid serviceOrderId, Person personOrdering, Person serviceman)
        {
            var serviceOrders = ServiceOrders.SingleOrDefault(x => x.Id == serviceOrderId);
            if (serviceOrders != null)
            {
                throw new Exception($"ServiceOrder with name: '{serviceOrderId}' already exists for device: {SerialNumber}.");
            }
            _serviceOrders.Add(new ServiceOrder(serviceOrderId, personOrdering, serviceman));
            Updated = DateTime.UtcNow;
        }

        public void AddServiceOrder(ServiceOrder serviceOrder)
        {
            _serviceOrders.Add(serviceOrder);
        }
    }
}