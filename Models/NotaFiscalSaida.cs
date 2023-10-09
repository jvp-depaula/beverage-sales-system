using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Sistema.Models
{
    public class NotaFiscalSaida
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
        public string nrNota { get; set; }
        [Display(Name = "Data de Emissão")]
        public DateTime dtEmissao { get; set; }
        [Display(Name = "Chave NFe")]
        public string chaveNFe { get; set; }
        [Display(Name = "Situação")]
        public string flSituacao { get; set; }
        [Display(Name = "Total Nota")]
        public decimal vlTotalNota { get; set; }
        public IEnumerable<ItemNFSaida> itensNF { get; set; }
        [Display(Name = "Total Itens")]
        public decimal vlTotalItens { get; set; }
        [Display(Name = "Desconto")]
        public decimal vlDesconto { get; set; }
        [Display(Name = "Cliente")]
        public int idCliente { get; set; }
    }
}
