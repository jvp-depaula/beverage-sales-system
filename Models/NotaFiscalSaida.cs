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
        [Required(ErrorMessage = "Informe o modelo da nota!")]
        public string nrModelo { get; set; }

        [Key]
        [Column(Order = 1)]
        [Display(Name = "Nrº Série")]
        [Required(ErrorMessage = "Informe a série da nota! ")]
        public string nrSerie { get; set; }

        [Key]
        [Column(Order = 2)]
        [Display(Name = "Nrº Nota")]
        [Required(ErrorMessage = "Informe o número da nota!")]
        public int nrNota { get; set; }

        [Display(Name = "Data de Emissão")]
        [Required(ErrorMessage = "Informe a data de emissão!")]
        public DateTime dtEmissao { get; set; }

        [Display(Name = "Chave NFe")]
        [Required(ErrorMessage = "Informe a chave eletronica da nota!")]
        public string chaveNFe { get; set; }

        [Display(Name = "Situação")]
        public string flSituacao { get; set; }

        [Display(Name = "Total Nota")]
        public decimal vlTotalNota { get; set; }

        public IEnumerable<ItemNFSaida> itensNF { get; set; }

        [Display(Name = "Total Itens")]
        public decimal vlTotalItens { get; set; }

        [Display(Name = "Desconto")]
        [Required(ErrorMessage = "Informe o desconto")]
        public decimal vlDesconto { get; set; }

        [Display(Name = "Cliente")]
        [Required(ErrorMessage = "Informe o cliente!")]
        public int idCliente { get; set; }

        [Display(Name = "Data de cadastro")]
        public DateTime? dtCadastro { get; set; }

        [Display(Name = "Data da últ. alteração")]
        public DateTime? dtUltAlteracao { get; set; }
    }
}
