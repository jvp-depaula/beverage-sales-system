using System.ComponentModel.DataAnnotations;

namespace Sistema.Models
{
    public class Estados
    {
        [Key]
        [Display(Name = "Código")]
        public int idEstado { get; set; }

        [Display(Name = "Estado")]
        public string nmEstado { get; set; }

        public int idPais { get; set; }

        [Display(Name = "UF")]
        public string flUF { get; set; }

        [Display(Name = "Data de cadastro")]
        public DateTime? dtCadastro { get; set; }

        [Display(Name = "Data da últ. alteração")]
        public DateTime? dtUltAlteracao { get; set; }
    }
}
