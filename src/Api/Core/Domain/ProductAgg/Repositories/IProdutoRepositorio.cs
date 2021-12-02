using System.Collections.Generic;
using ApiCsharp.Api.Core.Domain.ProductAgg.Entities;

namespace ApiCsharp.Api.Core.Domain.ProductAgg.Repositories
{
    public interface IProdutoRepositorio
    {
        void Adicionar(Produto produto);
        ICollection<Produto> Buscar(string nome);
        Produto ObterPeloId(string id);
    }
}