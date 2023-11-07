using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using static Sistema.Models.Compras;

namespace Sistema.Models
{
    public class Vendas
    {
        public int idVenda { get; set; }
        [Display(Name = "Data da venda")]
        public DateTime? dtVenda { get; set; }
                
        [Display(Name = "Funcionário")]
        public int idFuncionario { get; set; }
        public string nmFuncionario { get; set; }
        public IEnumerable<SelectListItem> ListFuncionarios { get; set; }

        [Display(Name = "Cliente")]
        public int idCliente { get; set; }
        public string nmCliente { get; set; }
        public IEnumerable<SelectListItem> ListClientes { get; set; }

        [Display(Name = "Produto")]
        public int idProduto { get; set; }
        public string dsProduto { get; set; }
        public IEnumerable<SelectListItem> ListProdutos { get; set; }

        [Display(Name = "Condição Pgto")]
        public int idCondicaoPgto { get; set; }
        public string dsCondicaoPgto { get; set; }
        public decimal? txJuros { get; set; }
        public decimal? vlMulta { get; set; }
        public decimal? vlDesconto { get; set; }
        public IEnumerable<SelectListItem> ListCondicaoPgto { get; set; }

        [Display(Name = "Situação")]
        public string flSituacao { get; set; }
        public decimal? vlTotal { get; set; }
        public string nrModelo { get; set; }
        public class ProdutosVM
        {
            public int? idProduto { get; set; }
            public string dsProduto { get; set; }
            public int idUnidade { get; set; }
            public decimal qtdProduto { get; set; }
            public decimal vlVenda { get; set; }
            public decimal? txDesconto { get; set; }
            public decimal vlTotal { get; set; }
        }

        public string jsProdutos { get; set; }
        public string jsParcelas { get; set; }

        public List<ProdutosVM> ProdutosVenda
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

        public List<Parcelas> ParcelasVenda
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

        public static SelectListItem[] Situacao
        {
            get
            {
                return new[]
                {
                    new SelectListItem { Value = "N", Text = "NORMAL"},
                    new SelectListItem { Value = "C", Text = "CANCELADA"}
                };
            }
        }
    }
}
