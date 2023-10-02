using System.ComponentModel.DataAnnotations;

namespace Sistema.Models
{
    public class CondicaoPgto
    {
        [Display(Name = "Código")]
        public int idCondicaoPgto { get; set; }
        [Display(Name = "Condição de Pagamento")]
        public string nomeCondicao { get; set; }
        [Display(Name = "Forma de Pagamento")]
        public int idFormaPgto { get; set; }

        [Display(Name = "Taxa de juros (%)")]
        public decimal? txJuros { get; set; }

        [Display(Name = "Porcentagem (%)")]
        public decimal? txPercentual { get; set; }

        [Display(Name = "Dias")]
        public int? qtdDias { get; set; }

        [Display(Name = "Multa (%)")]
        public decimal? multa { get; set; }

        [Display(Name = "Desconto (%)")]
        public decimal? desconto { get; set; }
        [Display(Name = "Data de cadastro")]
        public DateTime? dtCadastro { get; set; }

        [Display(Name = "Data da últ. alteração")]
        public DateTime? dtUltAlteracao { get; set; }
    }
}
