using System.ComponentModel.DataAnnotations;

namespace Sistema.Models
{
    public class Contatos
    {
        [Display(Name = "Número Celular")]
        public string nrTelefoneCelular { get; set; }
        [Display(Name = "Número Telefone")]
        public string nrTelefoneFixo { get; set; }
        [Display(Name = "Email")]
        public string dsEmail { get; set; }
    }
}
