using ApiCsharp.Api.Core.Domain.EstoqueAgg.Entities;

namespace ApiCsharp.Api.Core.Domain.EstoqueAgg.Repositories
{
    public interface IEstoqueRepository
    {
        void Adicionar(EstoqueItem estoqueItem);
        Estoque Carregar();
    }
}