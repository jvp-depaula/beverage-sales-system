using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Xml.Linq;

namespace Sistema.Models
{
    public class Produtos
    {
        [Key]
        [Display(Name = "Código")]
        public int idProduto { get; set; }

        [Display(Name = "Produto")]
        [Required(ErrorMessage = "Informe o produto!")]
        public string dsProduto { get; set; }

        [Display(Name = "Fornecedor")]
        [Required(ErrorMessage = "Selecione o fornecedor!")]
        public int idFornecedor { get; set; }
        public string nmFornecedor { get; set; }
        public IEnumerable<SelectListItem> ListaFornecedores { get; set; }

        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "Selecione a categoria!")]
        public int idCategoria { get; set; }
        public string nmCategoria { get; set; }
        public IEnumerable<SelectListItem> ListaCategorias { get; set; }

        [Display(Name = "Unidade")]
        [Required(ErrorMessage = "Selecione a unidade!")]
        public int idUnidade { get; set; }
        public string dsUnidade { get; set; }
        public IEnumerable<SelectListItem> ListaUnidades { get; set; }

        [Display(Name = "Marca")]
        [Required(ErrorMessage = "Selecione a marca!")]
        public int idMarca { get; set; }
        public string nmMarca { get; set; }
        public IEnumerable<SelectListItem> ListaMarcas { get; set; }

        [Display(Name = "NCM")]
        [Required(ErrorMessage = "Informe o NCM!")]
        public string cdNCM { get; set; }

        [Display(Name = "Valor de Compra")]
        [DisplayFormat(DataFormatString = "{0:###.###,##}")]
        public decimal? vlCusto { get; set; }

        [Display(Name = "Valor de Venda")]
        [Required(ErrorMessage = "Informe o valor de venda!")]
        [DisplayFormat(DataFormatString = "{0:###.###,##}")]
        public decimal vlVenda { get; set; }

        [Display(Name = "Observação")]
        public string observacao { get; set; }

        [Display(Name = "Valor Última Compra")]
        public decimal? vlUltCompra { get; set; }
       
        [Display(Name = "Estoque")] // Estoque.
        public decimal? qtdEstoque { get; set; } // Estoque - Saldo do Produto em Estoque.

        [Display(Name = "Data de cadastro")]
        public DateTime? dtCadastro { get; set; }

        [Display(Name = "Data da últ. alteração")]
        public DateTime? dtUltAlteracao { get; set; }
    }
}
