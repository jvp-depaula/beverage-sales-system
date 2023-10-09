using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sistema.Models
{
    public class ItemNFSaida
    {
        [Key]
        [Column(Order = 0)]
        [Display(Name = "Produto")]
        public int idProduto { get; set; }
        [Key]
        [Column(Order = 1)]
        [Display(Name = "Cliente")]
        public int idCliente { get; set; }
        [Key]
        [Column(Order = 2)]
        [Display(Name = "Nrº Modelo")]
        public string nrModelo { get; set; }
        [Key]
        [Column(Order = 3)]
        [Display(Name = "Nrº Série")]
        public string nrSerie { get; set; }
        [Key]
        [Column(Order = 4)]
        [Display(Name = "Nrº Nota")]
        public string nrNota { get; set; }
        [Display(Name = "Quantidade")]
        public decimal qtdItem { get; set; }
        [Display(Name = "Valor da Venda")]
        public decimal vlVenda { get; set;}
    }
}
