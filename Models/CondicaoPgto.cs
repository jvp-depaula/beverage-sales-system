using System.ComponentModel.DataAnnotations;

namespace Sistema.Models
{
    public class CondicaoPgto
    {
        [Display(Name = "Código")]
        public int idCondicaoPgto { get; set; }
        [Display(Name = "Condição de Pagamento")]
        public string dsCondicao { get; set; }
        [Display(Name = "Taxa de Desconto")]
        public decimal txDesconto { get; set; }
    }
}
