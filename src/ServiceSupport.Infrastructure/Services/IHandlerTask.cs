using System;
using System.Threading.Tasks;
using NLog;
using ServiceSupport.Core.Domain;

namespace ServiceSupport.Infrastructure.Services
{
    public interface IHandlerTask
    {
        IHandlerTask Always(Func<Task> always);
        IHandlerTask OnCustomError(Func<ServiceSupportException, Task> onCustomError,
            bool propagateException = false, bool executeOnError = false);
        IHandlerTask OnCustomError(Func<ServiceSupportException, Logger, Task> onCustomError,
            bool propagateException = false, bool executeOnError = false);
        IHandlerTask OnError(Func<Exception, Task> onError, bool propagateException = false);
        IHandlerTask OnError(Func<Exception, Logger, Task> onError, bool propagateException = false);
        IHandlerTask OnSuccess(Func<Task> onSuccess);
        IHandlerTask PropagateException();
        IHandlerTask DoNotPropagateException();
        IHandler Next();
        Task ExecuteAsync();
    }
}