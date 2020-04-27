using ServiceSupport.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceSupport.Core.Domain
{
    public class ServiceOrderDescription
    {
        public string Title { get; protected set; }
        public string Content { get; protected set; }
        public Person Person { get; protected set; }
        public DateTime Created { get; protected set; }
        protected ServiceOrderDescription()
        {
        }
        public ServiceOrderDescription( string content, Person person)
        {
            SetContent(content);
            SetPerson(person);
            Created = DateTime.UtcNow;
        }

        public ServiceOrderDescription(string content,string title, Person person)
        {
            SetContent(content);
            SetTitle(title);
            SetPerson(person);
            Created = DateTime.UtcNow;
        }

        private void SetContent(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new DomainException(ErrorCodes.InvalidContent,
                    "Content can not be empty.");
            }
            if (Content == content)
            {
                return;
            }

            Content = content;
        }
        private void SetPerson(Person person)
        {
            if (person == null)
            {
                throw new DomainException(ErrorCodes.InvalidPerson,
                    "Person is null.");
            }

            Person = person;
        }

        private void SetTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new DomainException(ErrorCodes.InvalidTitle,
                    "Title can not be empty.");
            }
            if (Title == title)
            {
                return;
            }

            Title = title;
        }

    }
}
