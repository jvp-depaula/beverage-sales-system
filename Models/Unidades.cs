using System.ComponentModel.DataAnnotations;

namespace Sistema.Models
{
    public class Unidades
    {
        [Key]
        [Display(Name = "Código")]
        public int idUnidade { get; set; }

        [Display(Name = "Unidade")]
        public string dsUnidade { get; set; }

        [Display(Name = "Sigla")]
        public string sigla { get; set; }

        [Display(Name = "Data de cadastro")]
        public DateTime? dtCadastro { get; set; }

        [Display(Name = "Data da últ. alteração")]
        public DateTime? dtUltAlteracao { get; set; }
    }
}