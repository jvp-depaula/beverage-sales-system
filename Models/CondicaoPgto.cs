using System.ComponentModel.DataAnnotations;

namespace Sistema.Models
{
    public class CondicaoPgto
    {
        [Display(Name = "Código")]
        public int idCondicaoPgto { get; set; }
        [Display(Name = "Condição de pagamento")]
        public string nomeCondicao { get; set; }

        [Display(Name = "Taxa de juros (%)")]
        public decimal? txJuros { get; set; }

        [Display(Name = "Porcentagem (%)")]
        public decimal? txPercentual { get; set; }

        [Display(Name = "Dias")]
        public short? qtdDias { get; set; }

        [Display(Name = "Multa (%)")]
        public decimal? multa { get; set; }

        [Display(Name = "Desconto (%)")]
        public decimal? desconto { get; set; }

        [Display(Name = "Total (%)")]
        public decimal? txPercentualTotal { get; set; }
        public decimal? txPercentualTotalAux { get; set; }

    }
}
