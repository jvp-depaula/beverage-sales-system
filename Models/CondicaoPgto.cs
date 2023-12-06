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
        public decimal txMulta { get; set; }

        [Required(ErrorMessage = "Informe o desconto!")]
        [Display(Name = "% Desconto")]
        public decimal txDesconto { get; set; }

        [Required(ErrorMessage = "Informe o nome juros!")]
        [Display(Name = "% Juros")]
        public decimal txJuros { get; set; }

        [Display(Name = "Forma de Pgto.")]
        [Required(ErrorMessage = "Selecione a forma de Pgto!")]
        public int idFormaPgto { get; set; }
        public string dsFormaPgto { get; set; }
        public IEnumerable<SelectListItem> ListaFormaPgto { get; set; }

        [Display(Name = "Dias")]
        [Required(ErrorMessage = "Descreva os dias!")]
        public int qtDias { get; set; }

        [Display(Name = "Percentual")]
        [Required(ErrorMessage = "Informe o percentual da parcela!")]
        public decimal txPercentual { get; set; }

        [Display(Name = "Número da Parcela")]        
        public int nrParcela { get; set; }
        public string jsParcelas { get; set; }
        public List<Parcelas> ListParcelas
        {
            get
            {
                if (string.IsNullOrEmpty(jsParcelas))
                    return new List<Parcelas>();
                return JsonConvert.DeserializeObject<List<Parcelas>>(jsParcelas);
            }
            set
            {
                jsParcelas = JsonConvert.SerializeObject(value);
            }
        }       

        [Display(Name = "Data de cadastro")]
        public DateTime? dtCadastro { get; set; }

        [Display(Name = "Data da últ. alteração")]
        public DateTime? dtUltAlteracao { get; set; }
    }
}
