using System;

namespace ServiceSupport.Core.Domain
{
    public abstract class ServiceSupportException : Exception
    {
        public string Code { get; }

        protected ServiceSupportException()
        {
        }

        protected ServiceSupportException(string code)
        {
            Code = code;
        }

        protected ServiceSupportException(string message, params object[] args) : this(string.Empty, message, args)
        {
        }

        protected ServiceSupportException(string code, string message, params object[] args) : this(null, code, message, args)
        {
        }

        protected ServiceSupportException(Exception innerException, string message, params object[] args)
            : this(innerException, string.Empty, message, args)
        {
        }

        protected ServiceSupportException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }        
    }
}