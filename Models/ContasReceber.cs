using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Sistema.Models
{
    public class ContasReceber
    {
        [Key]
        [Column(Order = 0)]
        [Display(Name = "Nrº Modelo")]
        public string nrModelo { get; set; }
        [Key]
        [Column(Order = 1)]
        [Display(Name = "Nrº Série")]
        public string nrSerie { get; set; }
        [Key]
        [Column(Order = 2)]
        [Display(Name = "Nrº Nota")]
<<<<<<< HEAD
        public int nrNota { get; set; }
=======
        public string nrNota { get; set; }
>>>>>>> e62caf04ce1315a201b6806efdf2275de71abc27
        [Key]
        [Column(Order = 3)]
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
        [Display(Name = "Valor Pago")]
        public decimal vlPago { get; set; }
        [Display(Name = "Data Pgto")]
        public DateTime dtPgto { get; set; }
        [Display(Name = "Situação")]
        public string flSituacao { get; set; }
        [Display(Name = "Juros")]
        public decimal vlJuros { get; set; }
        [Display(Name = "Multa")]
        public decimal vlMulta { get; set; }
        [Display(Name = "Desconto")]
        public decimal vlDesconto { get; set; }
        [Display(Name = "Taxa Desconto")]
        public decimal txDesconto { get; set; }
        [Display(Name = "Cliente")]
        public int idCliente { get; set; }
<<<<<<< HEAD
        [Display(Name = "Data de cadastro")]
        public DateTime? dtCadastro { get; set; }

        [Display(Name = "Data da últ. alteração")]
        public DateTime? dtUltAlteracao { get; set; }
    }
}
=======
    }
}
>>>>>>> e62caf04ce1315a201b6806efdf2275de71abc27
