using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Sistema.Models
{
    public class NotaFiscalEntrada
    {
        [Display(Name = "Fornecedor")]
        [Required(ErrorMessage = "Selecione o Fornecedor")]
        public int idFornecedor { get; set; }

        [Display(Name = "Nrº Modelo")]
        [Required(ErrorMessage = "Informe o modelo da nota!")]
        public string nrModelo { get; set; }
        
        [Required(ErrorMessage = "Informe a série da nota!")]
        [Display(Name = "Nrº Série")]
        public string nrSerie { get; set; }
        
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
    }
}
