using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sistema.Models
{
    public class Pessoas
    {
        [Key]
        [DisplayName("Código")]
        public int id { get; set; }

        [Display(Name = "Condição Pgto.")]
        [Required(ErrorMessage = "Selecione a condição de Pgto!")]
        public int idCondicaoPgto { get; set; }
        public string dsCondicaoPgto { get; set; }
        public IEnumerable<SelectListItem> ListaCondicoesPgto { get; set; }

        // CONTATO
        [Display(Name = "Número Celular")]
        public string nrTelefoneCelular { get; set; }
        [Display(Name = "Número Telefone")]
        public string nrTelefoneFixo { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Informe o email!")]
        public string dsEmail { get; set; }

        // TIPO
        [Display(Name = "Pessoa")]
        [Required(ErrorMessage = "Selecione o Tipo de Pessoa!")]
        public string flTipo { get; set; }

        // ENDERECO
        [Display(Name = "CEP")]
        [Required(ErrorMessage = "Informe o CEP!")]
        public string nrCEP { get; set; }
        [Display(Name = "Logradouro")]
        [Required(ErrorMessage = "Informe o logradouro!")]
        public string dsLogradouro { get; set; }
        [Display(Name = "Número")]
        [Required(ErrorMessage = "Informe o número!")]
        public string nrEndereco { get; set; }
        [Display(Name = "Bairro")]
        [Required(ErrorMessage = "Informe o bairro!")]
        public string dsBairro { get; set; }
        [Display(Name = "Complemento")]        
        public string dsComplemento { get; set; }
        [Display(Name = "Cidade")]
        [Required(ErrorMessage = "Selecione a Cidade!")]
        public int idCidade { get; set; }
        public string nmCidade { get; set; }
        public IEnumerable<SelectListItem> ListaCidades { get; set; }


        // CONTROLE
        [Display(Name = "Data de cadastro")]
        public DateTime? dtCadastro { get; set; }

        [Display(Name = "Data da últ. alteração")]
        public DateTime? dtUltAlteracao { get; set; }
                
        #region FLAGS
        #region TIPO
        public static SelectListItem[] Tipo
        {
            get
            {
                return new[]
                {
                    new SelectListItem { Value = "", Text = "Selecionar" },
                    new SelectListItem { Value = "F", Text = "FÍSICA" },
                    new SelectListItem { Value = "J", Text = "JURÍDICA" }                    
                };
            }
        }
        #endregion
        #endregion
    }
}
