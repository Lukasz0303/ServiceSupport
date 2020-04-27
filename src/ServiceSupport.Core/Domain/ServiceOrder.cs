using ServiceSupport.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceSupport.Core.Domain
{
    public class ServiceOrder
    {
        private ISet<ServiceOrderDescription> _serviceOrderDescriptions = new HashSet<ServiceOrderDescription>();
        public IEnumerable<ServiceOrderDescription> ServiceOrderDescriptions => _serviceOrderDescriptions;
        public Guid Id { get; protected set; }
        public Person PersonOrdering  { get; protected set; }
        public Person Serviceman { get; protected set; }
        public string Status { get; protected set; }
        public DateTime Created { get; protected set; }
        public DateTime Updated { get; protected set; }
        protected ServiceOrder()
        {
        }
        public ServiceOrder(Guid serviceOrderId, Person personOrdering, Person serviceman)
        {
            Id = serviceOrderId;
            SetPersonOrdering(personOrdering);
            SetServiceman(serviceman);
            Status = ServiceOrderStatuses.Started;
            Created = DateTime.UtcNow;
        }
        public void SetPersonOrdering(Person person)
        {
            if (person == null)
            {
                throw new DomainException(ErrorCodes.InvalidPerson,
                    "Person is null.");
            }

            PersonOrdering = person;
            Updated = DateTime.UtcNow;
        }
        public void SetServiceman(Person person)
        {
            if (person == null)
            {
                throw new DomainException(ErrorCodes.InvalidPerson,
                    "Person is null.");
            }

            Serviceman = person;
            Updated = DateTime.UtcNow;
        }
        public void AddServiceOrderDescription(string content, string title, Person person)
        {
            _serviceOrderDescriptions.Add(new ServiceOrderDescription(content, title, person));
            Updated = DateTime.UtcNow;
        }
    }
}
