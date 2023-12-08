using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Sistema.Models
{
    public class ContasReceber
    {
        public int idContasReceber { get; set; }
        [Display(Name = "Nrº Parcela")]
        public int nrParcela { get; set; }
        [Display(Name = "Data de Vencimento")]
        public DateTime dtVencimento { get; set; }
        [Display(Name = "Valor Parcela")]
        public decimal vlParcela { get; set; }
        [Display(Name = "Forma Pgto")]
        public int idFormaPgto { get; set; }
        public string dsFormaPgto { get; set; }
        [Display(Name = "Venda")]
        public int idVenda { get; set; }
        [Display(Name = "Data Pgto")]
        public DateTime? dtPgto { get; set; }
        [Display(Name = "Situação")]
        public string flSituacao { get; set; }
        [Display(Name = "Juros")]
        public decimal txJuros { get; set; }
        [Display(Name = "Multa")]
        public decimal txMulta { get; set; }
        [Display(Name = "Taxa Desconto")]
        public decimal txDesconto { get; set; }
        [Display(Name = "Cliente")]
        public int idCliente { get; set; }
        public string nmCliente { get; set; }
        [Display(Name = "Data de cadastro")]
        public DateTime? dtCadastro { get; set; }

        [Display(Name = "Data da últ. alteração")]
        public DateTime? dtUltAlteracao { get; set; }
    }
}