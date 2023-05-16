using System.ComponentModel.DataAnnotations;

namespace Sistema.Models
{
    public class Funcionarios : Pessoas
    {
        [Display(Name = "Nome")]
        public string nmFuncionario { get; set; }
        [Display(Name = "Apelido")]
        public string dsApelido { get; set; }
        public string nrCPF { get; set; }
        public string nrRG { get; set; }
        [Display(Name = "Data de Nascimento")]
        public DateTime dtNasc { get; set; }
    }
}
