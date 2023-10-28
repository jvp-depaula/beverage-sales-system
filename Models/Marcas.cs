using System.ComponentModel.DataAnnotations;

namespace Sistema.Models
{
    public class Marcas
    {
        [Key]
        [Display(Name = "Código")]
        public int idMarca { get; set; }

        [Display(Name = "Marca")]
        [Required(ErrorMessage = "Informe o nome da Marca!")]
        public string nmMarca { get; set; }

        [Display(Name = "Data de cadastro")]
        public DateTime? dtCadastro { get; set; }

        [Display(Name = "Data da últ. alteração")]
        public DateTime? dtUltAlteracao { get; set; }
    }
}
