using ServiceSupport.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ServiceSupport.Core.Domain
{
    public class Person
    {
        private static readonly Regex PhoneRegex = new Regex("^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\\s\\./0-9]*$");

        public string Name { get; protected set; }
        public string Surname { get; protected set; }
        public string FullName { get; protected set; }
        public string Email { get; protected set; }
        public string Phone { get; protected set; }
        public DateTime Created { get; protected set; }
        public DateTime Updated { get; protected set; }

        protected Person()
        {
        }

        public Person(string email)
        {
            SetEmail(email);
            Created = DateTime.UtcNow;
        }

        public Person(string name, string surname, string email,string phone)
        {
            SetName(name);
            SetSurname(surname);
            SetEmail(email);
            SetFullName(name, surname);
            SetPhone(phone);
            Created = DateTime.UtcNow;
        }
        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new DomainException(ErrorCodes.InvalidName,
                    "Name can not be empty.");
            }
            if (Name == name)
            {
                return;
            }

            Name = Name;
            Updated = DateTime.UtcNow;
        }

        public void SetSurname(string surname)
        {
            if (string.IsNullOrWhiteSpace(surname))
            {
                throw new DomainException(ErrorCodes.InvalidSurname,
                    "Surname can not be empty.");
            }
            if (Surname == surname)
            {
                return;
            }

            Surname = surname;
            Updated = DateTime.UtcNow;
        }

        public void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new DomainException(ErrorCodes.InvalidEmail,
                    "Email can not be empty.");
            }
            if (Email == email)
            {
                return;
            }

            Email = email.ToLowerInvariant();
            Updated = DateTime.UtcNow;
        }
        public void SetFullName(string name, string surname)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(surname))
            {
                throw new DomainException(ErrorCodes.InvalidFullName,
                    "Name or surname is can not be empty.");
            }

            FullName = Name + " " + Surname;
            Updated = DateTime.UtcNow;
        }

        public void SetPhone(string phone)
        {
            if (!PhoneRegex.IsMatch(phone))
            {
                throw new DomainException(ErrorCodes.InvalidPhone, "Phone is invalid.");
            }
            if (Phone == phone)
            {
                return;
            }

            this.Phone = phone;
            Updated = DateTime.UtcNow;
        }
    }
}
