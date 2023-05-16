using System.ComponentModel.DataAnnotations;

namespace Sistema.Models
{
    public class Categorias
    {
        [Key]
        [Display(Name = "Código")]
        public int idCategoria { get; set; }

        [Display(Name = "Categoria")]
        public string nmCategoria { get; set; }

        [Display(Name = "Data de cadastro")]
        public DateTime? dtCadastro { get; set; }

        [Display(Name = "Data da últ. alteração")]
        public DateTime? dtUltAlteracao { get; set; }
    }
}
