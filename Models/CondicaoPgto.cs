using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Sistema.Models
{
    public class CondicaoPgto
    {
        [Display(Name = "Código")]
        public int idCondicaoPgto { get; set; }
        [Required(ErrorMessage = "Informe o nome da Condição de Pagamento!")]
        [Display(Name = "Condição de Pagamento")]
        public string dsCondicaoPgto { get; set; }

        [Required(ErrorMessage = "Informe a multa!")]
        [Display(Name = "% Multa")]
        public decimal vlMulta { get; set; }

        [Required(ErrorMessage = "Informe o desconto!")]
        [Display(Name = "% Desconto")]
        public decimal vlDesconto { get; set; }

        [Required(ErrorMessage = "Informe o nome juros!")]
        [Display(Name = "% Juros")]
        public decimal vlJuros { get; set; }

        [Display(Name = "Forma de Pgto.")]
        [Required(ErrorMessage = "Selecione a forma de Pgto!")]
        public int idFormaPgto { get; set; }
        public IEnumerable<SelectListItem> ListaFormaPgto { get; set; }

        [Display(Name = "Dias")]
        [Required(ErrorMessage = "Descreva os dias!")]
        public int qtDias { get; set; }

        [Display(Name = "Percentual")]
        [Required(ErrorMessage = "Informe o percentual da parcela!")]
        public int txPercentual { get; set; }

        [Display(Name = "Número da Parcela")]        
        public int nrParcela { get; set; }


        public string jsItens { get; set; }
        public List<CondicaoPgtoVM> ListCondicao
        {
            get
            {
                if (string.IsNullOrEmpty(jsItens))
                    return new List<CondicaoPgtoVM>();
                return JsonConvert.DeserializeObject<List<CondicaoPgtoVM>>(jsItens);
            }
            set
            {
                jsItens = JsonConvert.SerializeObject(value);
            }
        }

        public class CondicaoPgtoVM
        {
            public int? idCondicaoPgto { get; set; }
            public string dsCondicaoPgto { get; set; }
            public int? idFormaPgto { get; set; }
            public string dsFormaPgto { get; set; }
            public int? nrParcela { get; set; }
            public int? qtDias { get; set; }
            public decimal txPercentual { get; set; }
        }

        [Display(Name = "Data de cadastro")]
        public DateTime? dtCadastro { get; set; }

        [Display(Name = "Data da últ. alteração")]
        public DateTime? dtUltAlteracao { get; set; }
    }
}
