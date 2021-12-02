using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using MediatR;
using ApiCsharp.Api.Core.Application.ProductAgg.Contracts;
using ApiCsharp.Api.Core.Domain.ProductAgg.Events;
using ApiCsharp.Pedido.Api.Core.Domain.Shared;

namespace ApiCsharp.Api.Core.Domain.ProductAgg.Entities
{
    public class Produto : IAggregateRoot
    {
        private ICollection<INotification> _domainEvents;

        private Produto()
        {
        }
        public Produto(string nome, long preco) : this()
        {
            ExternalId = Guid.NewGuid().ToString();
            Nome = nome;
            Preco = preco;
            Status = "Ativo";
            _domainEvents = new List<INotification>();
            RaiseDomainEvent(new ProdutoCriado(this));
        }

        public long Id { get; private set; }
        public string ExternalId { get; private set; }
        public string Nome { get; private set; }
        public string Status { get; private set; }
        public long Preco { get; private set; }

        public void Atualizar(IAtualizarProduto atualizarProduto)
        {
            Nome = atualizarProduto.Nome;
            Preco = atualizarProduto.Preco;
        }

        internal void Deletar()
        {
            Status = "Inativo";
        }

        private void RaiseDomainEvent(INotification notification)
        {
            _domainEvents.Add(notification);
        }

        public ICollection<INotification> GetDomainEvents()
        {
            return _domainEvents.ToImmutableList();
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}