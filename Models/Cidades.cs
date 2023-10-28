using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Sistema.Models
{
    public class Cidades
    {
        [Key]
        [Display(Name = "Código")]
        public int idCidade { get; set; }

        [Required(ErrorMessage = "Informe a Cidade!")]
        [Display(Name = "Cidade")]
        public string nmCidade { get; set; }

        [Required(ErrorMessage = "Selecione o Estado!")]
        [Display(Name = "Estado")]
        public int idEstado { get; set; }
        public string nmEstado { get; set; }
        public string nmPais { get; set; }

        [Required(ErrorMessage = "Informe o DDD!")]
        [Display(Name = "DDD")]
        public string DDD { get; set; }

        [Display(Name = "Data de cadastro")]
        public DateTime? dtCadastro { get; set; }

        [Display(Name = "Data da últ. alteração")]
        public DateTime? dtUltAlteracao { get; set; }
        public IEnumerable<SelectListItem> ListaEstados { get; set; }
    }
}
