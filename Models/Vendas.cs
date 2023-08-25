using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Sistema.Models
{
    public class Vendas
    {
        [Display(Name = "Código")]
        public int idVenda { get; set; }
        [Display(Name = "Data da venda")]
        public DateTime? dtVenda { get; set; }

        [Display(Name = "Situação")]
        public string situacao { get; set; }

        public string finalizar { get; set; }
        public decimal? vlTotal { get; set; }
        public string modelo { get; set; }

        public int idFuncionario { get; set; }
        public int idCliente { get; set; }
        public int idProduto { get; set; }
        public int idCondicaoPagamento { get; set; }
    }
}
