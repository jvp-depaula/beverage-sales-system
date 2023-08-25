using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Sistema.Models
{
    public class FormaPgto
    {
        [Display(Name = "Código")]
        public int idFormaPgto { get; set; }
        [Display(Name = "Forma de pagamento")]
        public string nomeForma { get; set; }

        [Display(Name = "Situação")]
        public string situacao { get; set; }
    }
}
