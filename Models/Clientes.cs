using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Sistema.Models
{
    public class Clientes : Pessoas
    {
        [Display(Name = "Nome")]
        public string nmCliente { get; set; }
        [Display(Name = "Apelido")]
        public string dsApelido { get; set; }
        [Display(Name = "CPF/CNPJ")]
        public string nrCPFCNPJ { get; set; }
        [Display(Name = "RG/IE")]
        public string nrRG_IE { get; set; }
        [Display(Name = "Nasc. / Fundação")]
        public DateTime dtNasc { get; set; }
    }
}
