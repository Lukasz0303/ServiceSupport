using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceSupport.Infrastructure.CQRS.Queries
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
}
