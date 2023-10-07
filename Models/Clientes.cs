using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Sistema.Models
{
    public class Clientes : Pessoas
    {
        [Display(Name = "Nome")]
        public string nmCliente { get; set; }
        public string nmFantasia { get; set; }
        public string nrCPFCNPJ{ get; set; }
        public string nrRG_IE { get; set; }
        public DateTime dtNasc { get; set; }        
    }
}
