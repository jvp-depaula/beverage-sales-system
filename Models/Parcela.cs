using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sistema.Models
{
    public class Parcela
    {
        [Key]
        [Column(Order = 0)]
        [Required(ErrorMessage = "Selecione a condição de Pgto!")]
        public int idCondicaoPgto { get; set; }

        [Key]
        [Column(Order = 1)]
        [Required(ErrorMessage = "Informe o número da parcela!")]
        public int nrParcela { get; set; }

        [Display(Name = "Dias")]
        [Required(ErrorMessage = "Informe o número de dias!")]
        public string dias { get; set; }

        [Display(Name = "Forma Pgto")]
        [Required(ErrorMessage = "Selecione a forma de pgto!")]
        public int idFormaPgto { get; set; }

        [Display(Name = "% Parcela")]
        [Required(ErrorMessage = "Informe a porcentagem da parcela!")]
        public decimal txPercentParcela { get; set; }

        [Display(Name = "Data de Cadastro")]
        public DateTime? dtCadastro { get; set; }

        [Display(Name = "Data da últ. alteração")]
        public DateTime? dtUltAlteracao { get; set; }
    }
}
