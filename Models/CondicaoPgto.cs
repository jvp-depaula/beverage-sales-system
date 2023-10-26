using System.ComponentModel.DataAnnotations;

namespace Sistema.Models
{
    public class CondicaoPgto
    {
        [Display(Name = "Código")]
        public int idCondicaoPgto { get; set; }
        [Display(Name = "Condição de Pagamento")]
        public string dsCondicaoPgto { get; set; }
        [Display(Name = "% Multa")]
        public decimal vlMulta { get; set; }
        [Display(Name = "% Desconto")]
        public decimal vlDesconto { get; set; }
        [Display(Name = "% Juros")]
        public decimal vlJuros { get; set; }
        [Display(Name = "Data de cadastro")]
        public DateTime? dtCadastro { get; set; }

        [Display(Name = "Data da últ. alteração")]
        public DateTime? dtUltAlteracao { get; set; }
    }
}
