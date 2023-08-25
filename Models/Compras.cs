using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Sistema.Models
{
    public class Compras
    {
        [Display(Name = "Código")]
        public int idCompra { get; set; }
        [Display(Name = "Modelo")]
        public string modelo { get; set; }
        public string modeloAux { get; set; }

        [Display(Name = "Situação")]
        public string situacao { get; set; }

        [Display(Name = "Série")]
        public string serie { get; set; }
        public string serieAux { get; set; }

        [Display(Name = "Número")]
        public int? nrNota { get; set; }
        public int? nrNotaAux { get; set; }

        [Display(Name = "Data de emissão")]
        public DateTime? dtEmissao { get; set; }
        public string dtEmissaoAux { get; set; }

        [Display(Name = "Data de entrega")]
        public DateTime? dtEntrega { get; set; }
        public string dtEntregaAux { get; set; }

        [Display(Name = "Observação")]
        public string observacao { get; set; }
        public decimal? vlTotal { get; set; }

        [Display(Name = "Valor da frete")]
        public decimal? vlFrete { get; set; }

        [Display(Name = "Valor do seguro")]
        public decimal? vlSeguro { get; set; }

        [Display(Name = "Outras despesas")]
        public decimal? vlDespesas { get; set; }
        public int idFornecedor { get; set; }
        public int idProduto { get; set; }
        public int idCondicaoPagamento { get; set; }
    }
}
