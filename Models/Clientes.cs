using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Sistema.Models
{
    public class Clientes : Pessoas
    {
        [Required(ErrorMessage = "Informe o nome!")]
        [Display(Name = "Nome")]
        public string nmCliente { get; set; }
        [Required(ErrorMessage = "Informe o apelido!")]
        public string nmFantasia { get; set; }
        [Required(ErrorMessage = "Informe o CPF/CNPJ!")]
        public string nrCPFCNPJ{ get; set; }
        [Required(ErrorMessage = "Informe o RG/IE!")]
        public string nrRG_IE { get; set; }
        [Required(ErrorMessage = "Campo obrigatório!")]
        public DateTime dtNasc { get; set; }        
    }
}
