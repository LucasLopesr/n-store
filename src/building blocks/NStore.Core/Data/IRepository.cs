using NStore.Core.DomainObjects;
using System;

namespace NStore.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {

    }
}
