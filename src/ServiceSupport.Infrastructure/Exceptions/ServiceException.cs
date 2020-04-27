using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceSupport.Infrastructure.Exceptions
{
    public class ServiceException : Exception
    {
        public ServiceException()
        {
        }

        public ServiceException(string code) : base(code)
        {
        }

        public ServiceException(string code, string message, params object[] args) : this(null, code, message, args)
        {
        }

        public ServiceException(Exception innerException, string message, params object[] args)
            : this(innerException, string.Empty, message, args)
        {
        }

        public ServiceException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
        }
    }

}
