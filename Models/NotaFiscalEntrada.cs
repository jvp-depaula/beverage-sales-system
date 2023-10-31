using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Sistema.Models
{
    public class NotaFiscalEntrada
    {
        [Key]
        [Column(Order = 0)]
        [Display(Name = "Fornecedor")]
        [Required(ErrorMessage = "Selecione o Fornecedor")]
        public int idFornecedor { get; set; }
        public string nmFornecedor { get; set; }
        public IEnumerable<SelectListItem> ListaFornecedores { get; set; }

        [Key]
        [Column(Order = 1)]
        [Display(Name = "Nrº Modelo")]
        [Required(ErrorMessage = "Informe o modelo da nota!")]
        public string nrModelo { get; set; }
        [Key]
        [Column(Order = 2)]
        [Required(ErrorMessage = "Informe a série da nota!")]
        [Display(Name = "Nrº Série")]
        public string nrSerie { get; set; }
        [Key]
        [Column(Order = 3)]
        [Required(ErrorMessage = "Informe o número da nota!")]
        [Display(Name = "Nrº Nota")]
        public int nrNota { get; set; }
        [Display(Name = "Data de Emissão")]
        [Required(ErrorMessage = "Informe a data de emissão!")]
        public DateTime dtEmissao { get; set; }
        [Display(Name = "Data de Entrada")]
        [Required(ErrorMessage = "Informe a data de entrada!")]
        public DateTime dtEntrada { get; set; }
        [Display(Name = "Chave NFe")]
        [Required(ErrorMessage = "Informe a chave da nota fiscal!")]
        public string chaveNFe { get; set; }
        [Display(Name = "Situação")]
        public string flSituacao { get; set; }
        [Display(Name = "Total Nota")]
        public decimal vlTotalNota { get; set; }        
        [Display(Name = "Frete")]
        [Required(ErrorMessage = "Informe o Frete!")]
        public decimal vlFrete { get; set; }
        [Required(ErrorMessage = "Informe o seguro!")]
        [Display(Name = "Seguro")]
        public decimal vlSeguro { get; set; }
        [Display(Name = "Outras Despesas")]
        [Required(ErrorMessage = "Informe as outras despesas!")]
        public decimal vlOutrasDespesas { get; set; }
        [Display(Name = "Total Itens")]
        public decimal vlTotalItens { get; set; }
        [Display(Name = "Desconto")]
        [Required(ErrorMessage = "Informe o desconto!")]
        public decimal vlDesconto { get; set; }
        [Display(Name = "Data de cadastro")]
        public DateTime? dtCadastro { get; set; }

        [Display(Name = "Data da últ. alteração")]
        public DateTime? dtUltAlteracao { get; set; }


        public int idProduto { get; set; }
        public string dsProduto { get; set; }
        public IEnumerable<SelectListItem> ListaProdutos { get; set; }
        public class ProdutosVM
        {
            public int idProduto { get; set; }
            public string dsProduto { get; set; }
            public string dsUnidade { get; set; }
            public decimal? qtProduto { get; set; }
            [DisplayFormat(DataFormatString = "{0:###.###,##}")]
            public decimal? vlVenda { get; set; }
            public decimal? vlCompra { get; set; }
            public decimal? txDesconto { get; set; }
            public decimal? vlTotal { get; set; }
        }
        public string jsProdutos { get; set; }
        public List<ProdutosVM> ProdutosCompra
        {
            get
            {
                if (string.IsNullOrEmpty(jsProdutos))
                    return new List<ProdutosVM>();
                return JsonConvert.DeserializeObject<List<ProdutosVM>>(jsProdutos);
            }
            set
            {
                jsProdutos = JsonConvert.SerializeObject(value);
            }
        }
    }
}
