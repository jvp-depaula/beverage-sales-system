using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sistema.Models
{
    public class ContasPagar
    {
        [Key]
        [Column(Order = 0)]
        [Display(Name = "Fornecedor")]
        public int idFornecedor { get; set; }
        public string nmFornecedor { get; set; }
        [Key]
        [Column(Order = 1)]
        [Display(Name = "Nrº Modelo")]
        public string nrModelo { get; set; }
        [Key]
        [Column(Order = 2)]
        [Display(Name = "Nrº Série")]
        public string nrSerie { get; set; }
        [Key]
        [Column(Order = 3)]
        [Display(Name = "Nrº Nota")]
        public int nrNota { get; set; }
        [Key]
        [Column(Order = 4)]
        [Display(Name = "Nrº Parcela")]
        public int nrParcela { get; set; }
        [Display(Name = "Data de Emissão")]
        public DateTime dtEmissao { get; set; }
        [Display(Name = "Data de Vencimento")]
        public DateTime dtVencimento { get; set; }
        [Display(Name = "Valor Parcela")]
        public decimal vlParcela { get; set; }
        [Display(Name = "Forma Pgto")]
        public int idFormaPgto { get; set; }
        public string dsFormaPgto { get; set; }
        [Display(Name = "Valor Pago")]
        public decimal vlPago { get; set; }
        [Display(Name = "Data Pgto")]
        public DateTime dtPgto { get; set; }
        [Display(Name = "Situação")]
        public string flSituacao { get; set; }
        [Display(Name = "Juros")]
        public decimal txJuros { get; set; }
        [Display(Name = "Multa")]
        public decimal txMulta { get; set; }
        [Display(Name = "Taxa Desconto")]
        public decimal txDesconto { get; set; }
        [Display(Name = "Data de cadastro")]
        public DateTime? dtCadastro { get; set; }

        [Display(Name = "Data da últ. alteração")]
        public DateTime? dtUltAlteracao { get; set; }
    }
}