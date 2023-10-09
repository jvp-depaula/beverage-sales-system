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
        public string nmFornecedor { get; set; }
        public IEnumerable<SelectListItem> ListaFornecedores { get; set; }

        [Display(Name = "Categoria")]
        public int idCategoria { get; set; }
        public string nmCategoria { get; set; }
        public IEnumerable<SelectListItem> ListaCategorias { get; set; }
        [Display(Name = "Unidade")]
        public int idUnidade { get; set; }
        public string dsUnidade { get; set; }
        public IEnumerable<SelectListItem> ListaUnidades { get; set; }
        [Display(Name = "Marca")]
        public int idMarca { get; set; }
        public string nmMarca { get; set; }
        public IEnumerable<SelectListItem> ListaMarcas { get; set; }

        [Display(Name = "NCM")]
        public string cdNCM { get; set; }

        [Display(Name = "Valor de venda")]
        public decimal vlVenda { get; set; }

        [Display(Name = "Observação")]
        public string observacao { get; set; }

        [Display(Name = "Custo Médio")]
        public decimal vlCustoMedio { get; set; }
        [Display(Name = "Saldo")] // Estoque.
        public int vlSaldo { get; set; } // Estoque - Saldo do Produto em Estoque.

        [Display(Name = "Data de cadastro")]
        public DateTime? dtCadastro { get; set; }

        [Display(Name = "Data da últ. alteração")]
        public DateTime? dtUltAlteracao { get; set; }
    }
}
