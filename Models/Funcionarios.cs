using System.ComponentModel.DataAnnotations;

namespace Sistema.Models
{
    public class Funcionarios : Pessoas
    {
        [Display(Name = "Nome")]
        public string nmFuncionario { get; set; }
        [Display(Name = "Apelido")]
        public string dsApelido { get; set; }
        [Display(Name = "CPF")]
        public string nrCPF { get; set; }
        [Display(Name = "CPF")]
        public string nrRG { get; set; }
        [Display(Name = "Data de Nascimento")]
        public DateTime dtNasc { get; set; }
    }
}
