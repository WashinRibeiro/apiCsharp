using ApiCsharp.Api.Core.Application.ProductAgg.Contracts;

namespace ApiCsharp.Api.Controllers.Contracts
{
    public class AtualizarProdutoDto : IAtualizarProduto
    {
        public string Nome { get; set; }
        public long Preco { get; set; }
    }
}