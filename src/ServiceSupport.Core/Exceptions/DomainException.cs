using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceSupport.Core.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException()
        {
        }

        public DomainException(string code) : base(code)
        {
        }

        public DomainException(string code, string message, params object[] args) : this(null, code, message, args)
        {
        }

        public DomainException(Exception innerException, string message, params object[] args)
            : this(innerException, string.Empty, message, args)
        {
        }

        public DomainException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
        }
    }
}
