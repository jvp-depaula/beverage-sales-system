using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Sistema.Models
{
    public class FormaPgto
    {
        [Display(Name = "Código")]
        public int idFormaPgto { get; set; }
        [Display(Name = "Forma de pagamento")]
        [Required(ErrorMessage = "Informe a descrição da Forma de Pgto")]
        public string dsFormaPgto { get; set; }

        [Display(Name = "Data de cadastro")]
        public DateTime? dtCadastro { get; set; }

        [Display(Name = "Data da últ. alteração")]
        public DateTime? dtUltAlteracao { get; set; }
    }
}
