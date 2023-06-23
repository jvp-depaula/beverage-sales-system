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
        public string flUnidade { get; set; }

        [Display(Name = "NCM")]
        public string cdNCM { get; set; }

        [Display(Name = "Valor de venda")]
        public decimal? vlVenda { get; set; }

        [Display(Name = "Observação")]
        public string observacao { get; set; }

        [Display(Name = "Data de cadastro")]
        public DateTime? dtCadastro { get; set; }

        [Display(Name = "Data da últ. alteração")]
        public DateTime? dtUltAlteracao { get; set; }

        #region FLAGS

        #region UNIDADE
        public static SelectListItem[] Unidade
        {
            get
            {
                return new[]
                {
                    new SelectListItem { Value = "U", Text = "UNIDADE" },
                    // verificar L, ML
                };
            }
        }
        #endregion
        #endregion
    }
}
