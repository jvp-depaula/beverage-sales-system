using System.ComponentModel.DataAnnotations;

namespace Sistema.Models
{
    public class Funcionarios : Pessoas
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Informe o nome!")]
        public string nmFuncionario { get; set; }
        [Display(Name = "Apelido")]
        [Required(ErrorMessage = "Informe o apelido!")]
        public string dsApelido { get; set; }
        [Display(Name = "CPF")]
        [Required(ErrorMessage = "Informe o CPF!")]
        public string nrCPF { get; set; }
        [Required(ErrorMessage = "Informe o RG!")]
        [Display(Name = "RG")]
        public string nrRG { get; set; }
        [Display(Name = "Data de Nascimento")]
        [Required(ErrorMessage = "Informe a data de nascimento!")]
        public DateTime dtNasc { get; set; }
    }
}
