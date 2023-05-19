using System.ComponentModel.DataAnnotations;

namespace Sistema.Models
{
    public class Fornecedores : Pessoas
    {
        [Display(Name = "Razão Social")]
        public string nmFornecedor { get; set; }
        [Display(Name = "Nome Fantasia")]
        public string nmFantasia { get; set; }
        [Display(Name = "CNPJ")]
        public string nrCNPJ { get; set; }
        [Display(Name = "Inscrição Estadual")]
        public string nrIE { get; set; }
    }
}
