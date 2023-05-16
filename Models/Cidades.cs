using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Sistema.Models
{
    public class Cidades
    {
        [Key]
        [Display(Name = "Código")]
        public int idCidade { get; set; }

        [Display(Name = "Cidade")]
        public string nmCidade { get; set; }

        [Display(Name = "Estado")]
        public int idEstado { get; set; }

        [Display(Name = "DDD")]
        public string DDD { get; set; }

        [Display(Name = "Data de cadastro")]
        public DateTime? dtCadastro { get; set; }

        [Display(Name = "Data da últ. alteração")]
        public DateTime? dtUltAlteracao { get; set; }
    }
}
