using ApiCsharp.Api.Core.Application.ProductAgg.Contracts;
using ApiCsharp.Api.Core.Application.ProductAgg.Parsers;
using ApiCsharp.Api.Core.Domain.ProductAgg.Entities;

namespace ApiCsharp.Api.Controllers.Parsers
{
    public class ProdutoParseFactory : IProdutoParseFactory
    {
        private readonly ProdutoReportParser _produtoReportParser;

        public ProdutoParseFactory(ProdutoReportParser produtoReportParser)
        {
            _produtoReportParser = produtoReportParser;
        }
        
        public IParser<Produto, IProdutoView> GetProdutoParse()
        {
            return new ProdutoParser();
        }

        public IParser<Produto, IProdutoView> GetProdutoReportParse()
        {
            return _produtoReportParser;
        }
    }
}