using System.ComponentModel.DataAnnotations;

namespace Sistema.Models
{
    public class Fornecedores : Pessoas
    {
        [Display(Name = "Razão Social")]
        [Required(ErrorMessage = "Informe o nome!")]
        public string nmFornecedor { get; set; }
        [Display(Name = "Nome Fantasia")]
        [Required(ErrorMessage = "Informe o nome fantasia!")]
        public string nmFantasia { get; set; }
        [Display(Name = "CNPJ")]
        [Required(ErrorMessage = "Informe o CNPJ!")]
        public string nrCNPJ { get; set; }
        [Display(Name = "Inscrição Estadual")]
        [Required(ErrorMessage = "Informe a incrição estadual!")]
        public string nrIE { get; set; }        
    }
}
