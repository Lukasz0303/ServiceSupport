﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceSupport.Infrastructure.CQRS.Queries
{
    public interface IQueryHandler<in TQuery, TResult> :
        IRequestHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    { }
}
