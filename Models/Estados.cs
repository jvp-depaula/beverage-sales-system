using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sistema.Models
{
    public class Estados
    {
        [Key]
        [Display(Name = "Código")]
        public int idEstado { get; set; }

        [Display(Name = "Estado")]
        public string nmEstado { get; set; }

        [Display(Name = "País")]
        public int idPais { get; set; }
        public string nmPais { get; set; }

        [Display(Name = "UF")]
        public string flUF { get; set; }

        [Display(Name = "Data de cadastro")]
        public DateTime? dtCadastro { get; set; }

        [Display(Name = "Data da últ. alteração")]
        public DateTime? dtUltAlteracao { get; set; }
        public IEnumerable<SelectListItem> ListaPaises { get; set; }
    }
}
