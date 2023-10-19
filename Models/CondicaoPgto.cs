using System.ComponentModel.DataAnnotations;

namespace Sistema.Models
{
    public class CondicaoPgto
    {
        [Display(Name = "Código")]
        public int idCondicaoPgto { get; set; }
        [Display(Name = "Condição de Pagamento")]
        public string dsCondicaoPgto { get; set; }
        [Display(Name = "Forma de Pgto")]
        public int idFormaPgto { get; set; }
        [Display(Name = "(%) Juros")]
        public decimal txJuros { get; set; }
        [Display(Name = "Taxa Percentual")]
        public decimal txPercentual { get; set; }
        [Display(Name = "Dias")]
        public int qtdDias { get; set; }
        [Display(Name = "Multa")]
        public decimal vlMulta { get; set; }
        [Display(Name = "Desconto")]
        public decimal vlDesconto { get; set; }
        [Display(Name = "Data de cadastro")]
        public DateTime? dtCadastro { get; set; }

        [Display(Name = "Data da últ. alteração")]
        public DateTime? dtUltAlteracao { get; set; }
    }
}
