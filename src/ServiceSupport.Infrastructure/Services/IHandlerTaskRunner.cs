using System;
using System.Threading.Tasks;

namespace ServiceSupport.Infrastructure.Services
{
    public interface IHandlerTaskRunner
    {
        IHandlerTask Run(Func<Task> runAsync);         
    }
}