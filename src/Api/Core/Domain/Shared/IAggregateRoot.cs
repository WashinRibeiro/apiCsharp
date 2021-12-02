using System.Collections.Generic;
using MediatR;

namespace ApiCsharp.Pedido.Api.Core.Domain.Shared
{
    public interface IAggregateRoot
    {
        ICollection<INotification> GetDomainEvents();
        void ClearDomainEvents();
    }
}