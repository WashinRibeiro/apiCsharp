namespace ApiCsharp.Api.Core.Application.ProductAgg.Contracts
{
    public interface IProdutoView
    {
        string Id { get; }
        string Nome { get; }
        string Preco { get; }
        int QuantidadeDisponivel { get; }
        string Status { get; }
    }
}