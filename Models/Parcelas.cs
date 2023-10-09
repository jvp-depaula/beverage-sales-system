using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Sistema.Models
{
    public class Parcelas
    {
        [Key]
        [Column(Order = 0)]
        public int idCondicaoPgto { get; set; }
        [Key]
        [Column(Order = 1)]
        public int idFormaPgto { get; set; }
        [Display(Name = "Dias")]
        public string dias { get; set; }
        [Display(Name = "% Parcela")]
        public decimal txPercentParcela { get; set; }
        [Display(Name = "% Juros")]
        public decimal txPercentJuros { get; set; }
        [Display(Name = "% Multa")]
        public decimal txPercentMulta { get; set; }
        [Display(Name = "% Desconto")]
        public decimal txPercentDesconto { get; set; }
    }
}
