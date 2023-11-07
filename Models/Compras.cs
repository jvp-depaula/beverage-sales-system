using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using NuGet.Protocol;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Sistema.Models
{
    public class Compras
    {
        [Display(Name = "Fornecedor")]
        public int idFornecedor { get; set; }
        [Display(Name = "Modelo")]
        public string nrModelo { get; set; }
        [Display(Name = "Série")]
        public string nrSerie { get; set; }
        [Display(Name = "Número")]
        public int nrNota { get; set; }

        [Display(Name = "Data de emissão")]
        public DateTime? dtEmissao { get; set; }

        [Display(Name = "Data de entrega")]
        public DateTime? dtEntrega { get; set; }
        [Display(Name = "Chave NFe")]
        public string chaveNFe { get; set; }
        public IEnumerable<SelectListItem> ListFornecedores { get; set; }
        public int? idFornecedorAux { get; set; }
        public string nmFornecedor { get; set; }
        public IEnumerable<SelectListItem> ListCondicaoPagamento { get; set; }
        public int? idCondicaoPgto { get; set; }
        public string dsCondicaoPgto { get; set; }
        public decimal? CondicaoPagamento_txJuros { get; set; }
        public decimal? CondicaoPagamento_vlMulta { get; set; }
        public decimal? CondicaoPagamento_vlDesconto { get; set; }
        public IEnumerable<SelectListItem> ListProdutos { get; set; }
        public int? idProduto { get; set; }
        public string dsProduto { get; set; }

        [Display(Name = "Situação")]
        public string flSituacao { get; set; }
        [Display(Name = "Observação")]
        public string observacao { get; set; }
        public decimal? vlTotal { get; set; }

        [Display(Name = "Valor do frete")]
        public decimal? vlFrete { get; set; }

        [Display(Name = "Valor do seguro")]
        public decimal? vlSeguro { get; set; }

        [Display(Name = "Outras despesas")]
        public decimal? vlDespesas { get; set; }

        public class ProdutosVM
        {
            public int? idProduto { get; set; }
            public string dsProduto { get; set; }
            public int? idUnidade { get; set; }
            public string dsUnidade { get; set; }
            public decimal? qtdProduto { get; set; }
            public decimal? vlVenda { get; set; }
            public decimal? vlCompra { get; set; }
            public decimal? txDesconto { get; set; }
            public decimal? vlTotal { get; set; }
        }
        public string jsProdutos { get; set; }
        public List<ProdutosVM> ProdutosCompra
        {
            get
            {
                if (string.IsNullOrEmpty(jsProdutos))
                    return new List<ProdutosVM>();
                return JsonConvert.DeserializeObject<List<ProdutosVM>>(jsProdutos);
            }
            set
            {
                jsProdutos = JsonConvert.SerializeObject(value);
            }
        }

        public string jsParcelas { get; set; }

        public List<Parcelas> ParcelasCompra
        {
            get
            {
                if (string.IsNullOrEmpty(jsParcelas))
                    return new List<Parcelas>();
                return JsonConvert.DeserializeObject<List<Parcelas>>(jsParcelas);
            }
            set
            {
                jsParcelas = JsonConvert.SerializeObject(value);
            }
        }
        public DateTime dtCadastro { get; set; }
    }
}
