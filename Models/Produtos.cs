using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Sistema.Models
{
    public class Produtos
    {
        [Key]
        [Display(Name = "Código")]
        public int idProduto { get; set; }

        [Display(Name = "Produto")]
        public string dsProduto { get; set; }

        [Display(Name = "Fornecedor")]
        public int idFornecedor { get; set; }

        [Display(Name = "Categoria")]
        public int idCategoria { get; set; }
        [Display(Name = "Unidade")]
        public int idUnidade { get; set; }
        [Display(Name = "Marca")]
        public int idMarca { get; set; }

        [Display(Name = "NCM")]
        public string cdNCM { get; set; }

        [Display(Name = "Valor de venda")]
        public decimal? vlVenda { get; set; }
        [Display(Name = "Custo")]
        public decimal? vlCusto { get; set; }

        [Display(Name = "Valor Custo Ult. Compra")]
        public decimal? vlCustoUltCompra { get; set; }
        [Display(Name = "Estoque")]
        public int qtdEstoque { get; set; }

        [Display(Name = "Data de cadastro")]
        public DateTime? dtCadastro { get; set; }

        [Display(Name = "Data da últ. alteração")]
        public DateTime? dtUltAlteracao { get; set; }
    }
}
