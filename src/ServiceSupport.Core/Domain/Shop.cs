using ServiceSupport.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ServiceSupport.Core.Domain
{
    public class Shop
    {
        private static readonly Regex PhoneRegex = new Regex("^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\\s\\./0-9]*$");
        
        private ISet<ShopTime> _shopTime = new HashSet<ShopTime>();
        public IEnumerable<ShopTime> ShopTime => _shopTime;
        public Guid Id { get; protected set; }
        public string Address { get; protected set; }
        public Person ResponsiblePerson { get; protected set; }
        public string SID { get; protected set; }
        public string Phone { get; protected set; }
        public DateTime Created { get; protected set; }
        public DateTime Updated { get; protected set; }
        protected Shop()
        {
        }
        public Shop(Guid shopId, string address, Person responsiblePerson, string SID, string phone)
        {
            Id = shopId;
            SetAddress(address);
            SetPerson(responsiblePerson);
            SetSID(SID);
            SetPhone(phone);
            Created = DateTime.UtcNow;
        }

        public void AddShopTime(DayOfWeek day, string startTime, string endTime)
        {
            var shopTime = ShopTime.SingleOrDefault(x => x.Day == day);
            if (shopTime != null)
            {
                throw new DomainException(ErrorCodes.InvalidAddress, $"ShopTime with day: '{day}' already exists for shop: {SID}:{Address}.");
            }
            _shopTime.Add(new ShopTime(day, startTime, endTime));
            Updated = DateTime.UtcNow;
        }

        public void SetAddress(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                throw new DomainException(ErrorCodes.InvalidAddress,
                    "Address can not be empty.");
            }
            if (Address == address)
            {
                return;
            }

            Address = address;
            Updated = DateTime.UtcNow;
        }

        public void SetPerson(Person person)
        {
            if (person == null)
            {
                throw new DomainException(ErrorCodes.InvalidPerson,
                    "Person is null.");
            }

            ResponsiblePerson = person;
            Updated = DateTime.UtcNow;
        }
        public void SetSID(string SID)
        {
            if (string.IsNullOrWhiteSpace(SID))
            {
                throw new DomainException(ErrorCodes.InvalidSID,
                    "SID can not be empty.");
            }
            if (this.SID == SID)
            {
                return;
            }

            this.SID = SID;
            Updated = DateTime.UtcNow;
        }

        public void SetPhone(string phone)
        {
            if (!PhoneRegex.IsMatch(phone))
            {
                throw new DomainException(ErrorCodes.InvalidPhone,"Phone is invalid.");
            }
            if (Phone == phone)
            {
                return;
            }

            this.Phone = phone;
            Updated = DateTime.UtcNow;
        }

        public bool IsOpen(int minutes)
        {
            var shopTime = this.ShopTime.FirstOrDefault(x => x.Day == DateTime.Now.DayOfWeek);
            if(shopTime!=null)
            {
                return DateTime.Now.AddMinutes(-minutes)> shopTime.StartTime ? true : false;
            }
            return false;
        }
    }
}
