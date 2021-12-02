using ApiCsharp.Api.Core.Application.ProductAgg.Contracts;
using ApiCsharp.Api.Core.Domain.ProductAgg.Entities;

namespace ApiCsharp.Api.Core.Application.ProductAgg.Parsers
{
    public interface IParser<TFrom, TTo>
    {
        TTo Parse(TFrom from);
    }

    /// <summary>
    /// Abstract factory
    /// </summary>
    public interface IProdutoParseFactory
    {
        IParser<Produto, IProdutoView> GetProdutoParse();
        IParser<Produto, IProdutoView> GetProdutoReportParse();
    }
}