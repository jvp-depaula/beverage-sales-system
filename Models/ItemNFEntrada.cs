using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Sistema.Models
{
    public class ItemNFEntrada
    {
        [Key]
        [Column(Order = 0)]
        [Display(Name = "Produto")]
        public int idProduto { get; set; }
        [Key]
        [Column(Order = 1)]
        [Display(Name = "Fornecedor")]
        public int idFornecedor { get; set; }
        [Key]
        [Column(Order = 2)]
        [Display(Name = "Nrº Modelo")]
        public string nrModelo { get; set; }
        [Key]
        [Column(Order = 3)]
        [Display(Name = "Nrº Série")]
        public string nrSerie { get; set; }
        [Key]
        [Column(Order = 4)]
        [Display(Name = "Nrº Nota")]
        public string nrNota { get; set; }
        [Display(Name = "Quantidade")]
        public decimal qtdItem { get; set; }
        [Display(Name = "Frete")]
        public decimal vlFrete { get; set; }
        [Display(Name = "Desconto")]
        public decimal vlDesconto { get; set; }
        [Display(Name = "Seguro")]
        public decimal vlSeguro { get; set; }
        [Display(Name = "Outras Despesas")]
        public decimal vlOutrasDespesas { get; set; }
        [Display(Name = "Custo")]
        public decimal vlCusto { get; set; }
        [Display(Name = "Data de cadastro")]
        public DateTime? dtCadastro { get; set; }

        [Display(Name = "Data da últ. alteração")]
        public DateTime? dtUltAlteracao { get; set; }
    }
}
