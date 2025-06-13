using System.ComponentModel.DataAnnotations;

namespace Sistema.Models
{
    public class Paises
    {
        [Key]
        [Display(Name = "Código")]
        public int idPais { get; set; }

        [Display(Name = "País")]
        [Required(ErrorMessage = "Informe o país!")]
        public string nmPais { get; set; }

        [Display(Name = "DDI")]
        [Required(ErrorMessage = "Informe o DDI")]
        public string DDI { get; set; }

        [Display(Name = "Sigla")]
        [Required(ErrorMessage = "Informe a Sigla")]
        public string sigla { get; set; }

        [Display(Name = "Data de cadastro")]
        public DateTime? dtCadastro { get; set; }

        [Display(Name = "Data da últ. alteração")]
        public DateTime? dtUltAlteracao { get; set; }
    }
}
