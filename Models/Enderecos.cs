using System.ComponentModel.DataAnnotations;

namespace Sistema.Models
{
    public class Enderecos
    {
        [Display(Name = "CEP")]
        public string nrCEP { get; set; }
        [Display(Name = "Logradouro")]
        public string dsLogradouro { get; set; }
        [Display(Name = "Número")]
        public string nrEndereco { get; set; }
        [Display(Name = "Bairro")]
        public string dsBairro { get; set; }
        [Display(Name = "Complemento")]
        public string dsComplemento { get; set; }
        [Display(Name = "Cidade")]
        public int idCidade { get; set; }
    }
}
